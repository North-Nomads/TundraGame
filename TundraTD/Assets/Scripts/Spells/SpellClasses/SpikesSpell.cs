using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spells.SpellClasses
{
    [Spell(BasicElement.Earth, "Spikes", "Creates a spikes in front of you to penetrate your enemies.")]
    public class SpikesSpell : MagicSpell
    {
        private const int CastableLayer = 1 << 11 | 1 << 10;
        //private const Vector3 StepValue = 1.375f;
        
        [SerializeField] private Transform spikesGroupObject;
        [SerializeField] private float spikesDistance = 1;
        
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

        [IncreasableProperty(BasicElement.Water, -0.05f)]
        public float SlownessValue { get; set; } = 0.7f;

        public float Lifetime { get; set; } = 4f;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        public override void InstantiateSpellExecution()
        {
            _touchRegisterTime = 0f;
            _touchRegisterMaxTime = MaxDrawTime;
            StartCoroutine(ExecuteSpikesSpell());
        }

        private IEnumerator ExecuteSpikesSpell()
        {
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out var hitInfo1, float.PositiveInfinity, CastableLayer))
                yield break;
            
            var position1 = hitInfo1.point;
            var position2 = position1;

            while (_touchRegisterTime < _touchRegisterMaxTime)
            {
                /*if (Input.touchCount != 1)
                    yield return null;*/
                
                var rayEnd = _mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(rayEnd, out var hitInfo2))
                {
                    position2 = hitInfo2.point;
                }
                
                _touchRegisterTime += Time.deltaTime;
                yield return null;
            }
            
            
            if (position1 == position2)
                yield break;
            yield return InstantiateSpikes(position1, position2);
        }

        private IEnumerator InstantiateSpikes(Vector3 start, Vector3 finish)
        {
            var spikes = new List<Transform>();
            var direction = finish - start;
            var count = direction / 7;
            var currentPosition = start;

            while (currentPosition.magnitude < finish.magnitude)
            {
                print(count);
                var group = Instantiate(spikesGroupObject, currentPosition, Quaternion.identity);
                spikes.Add(group);
                currentPosition += count;
                yield return new WaitForSeconds(.05f);
            }

            yield return new WaitForSeconds(Lifetime);
            yield return DestroySpikes(spikes);
        }

        private IEnumerator DestroySpikes(List<Transform> spikes)
        {
            foreach (var spike in spikes)
            {
                Destroy(spike.gameObject);
                yield return new WaitForSeconds(SpikeDisappearCooldown);
            }
        }
    }
}