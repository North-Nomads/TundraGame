using System.Collections;
using System.Collections.Generic;
using City.Building.ElementPools;
using UnityEngine;

namespace Spells.SpellClasses.EarthSpell
{
    [Spell(BasicElement.Earth, "Spikes", "Creates a spikes in front of you to penetrate your enemies.")]
    public class SpikesSpell : MagicSpell
    {
        private const int CastableLayer = 1 << 11 | 1 << 10;

        [SerializeField] private Transform spikesHolder;
        [SerializeField] private SpikesSlownessCollider spikesSlownessCollider;
        [SerializeField] private SpikesAreaAround spikesAreaAround;
        [SerializeField] private SpikesGroup spikesGroupObject;
        [Header("Pebble spell")]
        [SerializeField] private float pebbleDamage;
        [SerializeField] private int pebbleStunTicks;
        [Header("Termites spell")] 
        [SerializeField] private float termitesDamage;
        
        private float _touchRegisterMaxTime;
        private float _touchRegisterTime;
        private Camera _mainCamera;
        
        public float ApproachDelay { get; } = 0.2f;
        
        public float MaxDrawTime { get; set; } = 2f;
        
        public float SpikeDisappearCooldown { get; set; } = .1f;

        [IncreasableProperty(BasicElement.Lightning, 10f)]
        public float MaxLength { get; set; } = 30f;

        [MultiplictableProperty(BasicElement.Earth, 1.12f)]
        public float CollisionDamage { get; set; } = 30f;

        [MultiplictableProperty(BasicElement.Earth, 1.12f)]
        public float FallDamage { get; set; } = 50f;

        [IncreasableProperty(BasicElement.Fire, 0.3f)]
        public float StunTime { get; set; } = 0.7f;

        [IncreasableProperty(BasicElement.Air, -0.7f)]
        public float SlownessTime { get; set; } = 2f;

        [IncreasableProperty(BasicElement.Water, -.05f)]
        public float SlownessValue { get; set; } = 0.7f;

        public int Lifetime { get; set; } = 4;

        private void Start()
        {
            _mainCamera = Camera.main;
            spikesSlownessCollider.SendSlownessValues(Lifetime, SlownessValue);
            spikesGroupObject.StunTicks = 4;
            spikesSlownessCollider.SpikesEnterDamage = CollisionDamage;
            spikesSlownessCollider.TermitesDamage = termitesDamage;
        }

        private IEnumerator RegisterUserInputs()
        {
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out var hitInfo1, float.PositiveInfinity, CastableLayer))
                yield break;
            
            var position1 = hitInfo1.point;
            var position2 = position1;
            
            spikesSlownessCollider.InitilizeTermites(EarthPool.HasTermites);

            while (_touchRegisterTime < _touchRegisterMaxTime)
            {
                var rayEnd = _mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(rayEnd, out var hitInfo2, float.PositiveInfinity, CastableLayer))
                    position2 = hitInfo2.point;

                _touchRegisterTime += Time.deltaTime;
                yield return null;
            }
            
            if (position1 == position2)
                yield break;
            yield return InstantiateSpikes(position1, position2);
        }

        private IEnumerator InstantiateSpikes(Vector3 start, Vector3 finish)
        {
            var spikes = new List<SpikesGroup>();
            var direction = finish - start;
            var step = direction / direction.magnitude;
            var count = direction.magnitude / step.magnitude;
            var currentPosition = start;

            spikesSlownessCollider.BoxCollider.isTrigger = !EarthPool.HasSolidWalls;
            spikesAreaAround.gameObject.SetActive(EarthPool.HasDustCloud);

            while (count > 0)
            {
                var group = Instantiate(spikesGroupObject, currentPosition, Quaternion.identity, spikesHolder.transform);
                group.ApplyStunOverlappedOnMobs(FallDamage, (int)StunTime*10);

                if (EarthPool.HasExplosivePebbles)
                    group.ExecutePebblesExplosion(pebbleDamage, pebbleStunTicks);
                
                spikes.Add(group);
                spikesSlownessCollider.SetColliderParameters(spikes, finish);
                currentPosition += step;
                count--;
                yield return new WaitForSeconds(.02f);
            }

            yield return new WaitForSeconds(Lifetime);
            yield return HandleSpikesSpellEnding(spikes);
        }

        private IEnumerator HandleSpikesSpellEnding(List<SpikesGroup> spikes)
        {
            spikesAreaAround.gameObject.SetActive(false);
            spikesSlownessCollider.gameObject.SetActive(false);
            
            // Destroy spikes
            foreach (var spike in spikes)
            {
                Destroy(spike.gameObject);
                yield return new WaitForSeconds(SpikeDisappearCooldown);
            }
        }
        
        public override void InstantiateSpellExecution()
        {
            _touchRegisterTime = 0f;
            _touchRegisterMaxTime = MaxDrawTime;
            StartCoroutine(RegisterUserInputs());
        }
    }
}