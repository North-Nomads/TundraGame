using System.Collections;
using System.Collections.Generic;
using City.Building.ElementPools;
using Level;
using UnityEngine;

namespace Spells.SpellClasses.EarthSpell
{
    [Spell(BasicElement.Earth, "Spikes", "Creates a spikes in front of you to penetrate your enemies.")]
    public class SpikesSpell : MagicSpell
    {
        private const int PlaceableLayer = 1 << 11 | 1 << 10;

        [SerializeField] private float spikesOffset;
        [Tooltip("The object to which spikes will be assigned as children")] [SerializeField] private Transform spikesObjectParent;
        [SerializeField] private SpikesSlownessCollider spikesSlownessCollider;
        [SerializeField] private SpikesAreaAround spikesAreaAround;
        [SerializeField] private SpikesGroup spikesGroupObject;
        [Header("Pebble spell")]
        [SerializeField] private float pebbleDamage;
        [SerializeField] private int pebbleStunTicks;
        [Header("Termites spell")] 
        [SerializeField] private float termitesDamage;
        [Header("Floating stone-hammers links")]
        [SerializeField] private SpikesHammerStone[] hammerStones;
        
        private float _touchRegisterMaxTime;
        private float _touchRegisterTime;
        private Camera _mainCamera;
        private List<SpikesGroup> _spikes;

        private float MaxDrawTime { get; set; } = 2f;

        private float SpikeDisappearCooldown { get; set; } = .1f;

        [IncreasableProperty(BasicElement.Lightning, 10f)]
        public float MaxLength { get; set; } = 30f;

        [MultiplictableProperty(BasicElement.Earth, 1.12f)]
        private float CollisionDamage { get; set; } = 30f;

        [MultiplictableProperty(BasicElement.Earth, 1.12f)]
        private float FallDamage { get; set; } = 50f;

        [IncreasableProperty(BasicElement.Fire, 1f)]
        private float StunTime { get; set; } = 2f;

        [IncreasableProperty(BasicElement.Air, -0.7f)]
        public float SlownessTime { get; set; } = 2f;

        [IncreasableProperty(BasicElement.Water, -.05f)]
        private float SlownessValue { get; set; } = 0.7f;

        private int Lifetime { get; set; } = 4;
        
        public override void ExecuteSpell(RaycastHit castPosition)
        {
            _mainCamera = Camera.main;
            _spikes = new List<SpikesGroup>();
            spikesSlownessCollider.SendSlownessValues(Lifetime, SlownessValue);
            spikesGroupObject.StunTicks = 4;
            spikesSlownessCollider.SpikesEnterDamage = CollisionDamage;
            spikesSlownessCollider.TermitesDamage = termitesDamage;
            _touchRegisterTime = 0f;
            _touchRegisterMaxTime = MaxDrawTime;
            StartCoroutine(RegisterUserInputs(castPosition.point));
        }

        private IEnumerator RegisterUserInputs(Vector3 startPosition)
        {
            var group = Instantiate(spikesGroupObject, startPosition, Quaternion.identity, spikesObjectParent.transform);
            _spikes.Add(group);
            yield return new WaitForEndOfFrame();
            while (_touchRegisterTime < _touchRegisterMaxTime)
            {
                if (Input.touchCount == 1)
                {
                    Touch endTouch = Input.GetTouch(0);
                    var rayEnd = _mainCamera.ScreenPointToRay(endTouch.position);
                    Vector3 endPosition = endTouch.position;
                    
                    if (Physics.Raycast(rayEnd, out var hitInfo, float.PositiveInfinity, PlaceableLayer))
                        endPosition = hitInfo.point;
                    
                    if (Mathf.Abs(startPosition.magnitude - endPosition.magnitude) >= 1)
                    {
                        Debug.Log("Started coroutine");
                        StartCoroutine(InstantiateSpikesPath(startPosition, endPosition, true));
                        if (!EarthPool.HasAdditionalWalls) yield break;
                        StartCoroutine(InstantiateSpikesPath(startPosition + Vector3.left * 3, endPosition + Vector3.left * 3, false));
                        StartCoroutine(InstantiateSpikesPath(startPosition + Vector3.right * 3, endPosition + Vector3.right * 3, false));
                    }
                }
                _touchRegisterTime += Time.deltaTime;
                yield return null;
            }
        }
        
        private IEnumerator InstantiateSpikesPath(Vector3 start, Vector3 finish, bool isMainWall)
        {
            var direction = finish - start;
            var step = direction / direction.magnitude;
            var count = direction.magnitude / step.magnitude / spikesOffset - 1;
            var currentPosition = start;
            if (isMainWall)
                currentPosition += step;
            
            if (count > 10)
                count = 10;
            
            // Handling tower modifications
            spikesSlownessCollider.BoxCollider.isTrigger = !EarthPool.HasSolidWalls;
            spikesAreaAround.gameObject.SetActive(EarthPool.HasDustCloud & isMainWall);

            var sizeCoefficient = isMainWall ? 1f : .6f;
            // Place SpikesGroup from start position to finish position but max quantity is 10 
            while (count > 0)
            {
                spikesSlownessCollider.InitializeTermites(EarthPool.HasTermites);
                var group = Instantiate(spikesGroupObject, currentPosition, Quaternion.identity, spikesObjectParent.transform);
                
                if (EarthPool.HasDustCloud)
                    group.PlayCloudAnimation();
                
                group.transform.localScale *= sizeCoefficient;
                group.ApplyStunOnOverlappedMobs(FallDamage, StunTime.SecondsToTicks()); // Stun mobs on spawn position
                
                // Check pebbles modification
                if (EarthPool.HasExplosivePebbles & isMainWall)
                        group.ExecutePebblesExplosion(pebbleDamage, pebbleStunTicks);
                
                _spikes.Add(group);
                // Expand slowness collider size to fit current amount of spikes
                spikesSlownessCollider.SetColliderParameters(_spikes, finish);
                currentPosition += step * spikesOffset;
                count--;
                yield return new WaitForSeconds(.02f);
            }

            if (EarthPool.HasFloatingStones)
            {
                for (int i = 0; i < hammerStones.Length; i++)
                {
                    var stone = hammerStones[i];
                    stone.transform.position = (start + finish) / 2 + Mathf.Pow(-1, i + 1) * 5 * Vector3.left;
                    stone.gameObject.SetActive(true);
                    stone.BeginHammeringCoroutine();   
                }
            }

            yield return new WaitForSeconds(Lifetime);
            yield return HandleSpikesSpellEnding();
        }

        private IEnumerator HandleSpikesSpellEnding()
        {
            spikesAreaAround.gameObject.SetActive(false);
            spikesSlownessCollider.gameObject.SetActive(false);
            
            foreach (var spike in _spikes)
            {
                StartCoroutine(spike.InitializeSpikesShrinking());
                yield return new WaitForSeconds(SpikeDisappearCooldown);
            }
            Destroy(gameObject);
        }
    }
}