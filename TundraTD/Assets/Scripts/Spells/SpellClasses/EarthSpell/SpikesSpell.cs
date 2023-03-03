using System.Collections;
using System.Collections.Generic;
using City.Building.ElementPools;
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

        private float MaxDrawTime { get; set; } = 2f;

        private float SpikeDisappearCooldown { get; set; } = .1f;

        [IncreasableProperty(BasicElement.Lightning, 10f)]
        public float MaxLength { get; set; } = 30f;

        [MultiplictableProperty(BasicElement.Earth, 1.12f)]
        private float CollisionDamage { get; set; } = 30f;

        [MultiplictableProperty(BasicElement.Earth, 1.12f)]
        private float FallDamage { get; set; } = 50f;

        [IncreasableProperty(BasicElement.Fire, 0.3f)]
        private float StunTime { get; set; } = 0.7f;

        [IncreasableProperty(BasicElement.Air, -0.7f)]
        public float SlownessTime { get; set; } = 2f;

        [IncreasableProperty(BasicElement.Water, -.05f)]
        private float SlownessValue { get; set; } = 0.7f;

        private int Lifetime { get; set; } = 4;
        
        public override void ExecuteSpell()
        {
            _mainCamera = Camera.main;
            spikesSlownessCollider.SendSlownessValues(Lifetime, SlownessValue);
            spikesGroupObject.StunTicks = 4;
            spikesSlownessCollider.SpikesEnterDamage = CollisionDamage;
            spikesSlownessCollider.TermitesDamage = termitesDamage;
            _touchRegisterTime = 0f;
            _touchRegisterMaxTime = MaxDrawTime;
            StartCoroutine(RegisterUserInputs());
        }

        private IEnumerator RegisterUserInputs()
        {
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out var hitInfo1, float.PositiveInfinity, PlaceableLayer))
                yield break;
            
            var position1 = hitInfo1.point;
            var position2 = position1;

            while (_touchRegisterTime < _touchRegisterMaxTime)
            {
                var rayEnd = _mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(rayEnd, out var hitInfo2, float.PositiveInfinity, PlaceableLayer))
                    position2 = hitInfo2.point;

                _touchRegisterTime += Time.deltaTime;
                yield return null;
            }
            
            if (position1 == position2)
                yield break;

            StartCoroutine(InstantiateSpikes(position1, position2, true));

            if (!EarthPool.HasAdditionalWalls) yield break;
            
            StartCoroutine(InstantiateSpikes(position1 + Vector3.left * 3, position2 + Vector3.left * 3, false));
            StartCoroutine(InstantiateSpikes(position1 + Vector3.right * 3, position2 + Vector3.right * 3, false));

        }

        private IEnumerator InstantiateSpikes(Vector3 start, Vector3 finish, bool isMainWall)
        {
            var spikes = new List<SpikesGroup>();
            var direction = finish - start;
            var step = direction / direction.magnitude;
            var count = direction.magnitude / step.magnitude / spikesOffset;
            var currentPosition = start;
            
            if (count > 10)
                count = 10;
            
            spikesSlownessCollider.BoxCollider.isTrigger = !EarthPool.HasSolidWalls;
            spikesAreaAround.gameObject.SetActive(EarthPool.HasDustCloud & isMainWall);

            var sizeCoefficient = isMainWall ? 1f : .6f;
            while (count > 0)
            {
                spikesSlownessCollider.InitializeTermites(EarthPool.HasTermites);
                var group = Instantiate(spikesGroupObject, currentPosition, Quaternion.identity, spikesObjectParent.transform);
                
                if (EarthPool.HasDustCloud)
                    group.PlayCloudAnimation();
                
                group.transform.localScale *= sizeCoefficient;
                group.ApplyStunOverlappedOnMobs(FallDamage, (int)(StunTime*10));
                
                if (EarthPool.HasExplosivePebbles & isMainWall)
                        group.ExecutePebblesExplosion(pebbleDamage, pebbleStunTicks);
                
                spikes.Add(group);
                spikesSlownessCollider.SetColliderParameters(spikes, finish);
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
            yield return HandleSpikesSpellEnding(spikes);
        }

        private IEnumerator HandleSpikesSpellEnding(List<SpikesGroup> spikes)
        {
            spikesAreaAround.gameObject.SetActive(false);
            spikesSlownessCollider.gameObject.SetActive(false);
            
            foreach (var spike in spikes)
            {
                Destroy(spike.gameObject);
                yield return new WaitForSeconds(SpikeDisappearCooldown);
            }
        }
    }
}