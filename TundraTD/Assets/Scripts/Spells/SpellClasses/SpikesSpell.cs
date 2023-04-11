using System.Collections;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Spells.SpellClasses
{
	public class SpikesSpell : MagicSpell
    {
        [SerializeField] private Transform spikesPrefab;
        [SerializeField] private ParticleSystem spawnEffect;
        private const int CirclesAmount = 3;
        private const float RadiusMultiplier = .9f;
        private const float Seconds = .07f;
        private const float GrowthTime = .5f;
        private const float SpikesLifeTime = 1f;
        private const float SpikesDamage = 35f;
        private SphereCollider _sphereCollider;
        private Vector3 _targetSpikesScale; 
        
        public override BasicElement Element => BasicElement.Earth;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            var castPosition = hitInfo.point;
            _targetSpikesScale = spikesPrefab.transform.localScale;
            _sphereCollider = GetComponent<SphereCollider>();
            StartCoroutine(StartRadialSpikesSpawning(castPosition));
        }

        private IEnumerator StartRadialSpikesSpawning(Vector3 castPosition)
        {
            var radius = RadiusMultiplier;
            var spikesQuantity = 6;
            
            StartCoroutine(ExecuteSpikesLifecycle(castPosition));
            _sphereCollider.center = castPosition;
            yield return new WaitForSeconds(Seconds);
            
            for (int j = 0; j < CirclesAmount; j++)
            {
                float segment = 360f / spikesQuantity;
                for (int i = 0; i < spikesQuantity; i++)
                {
                    var phi = Mathf.Deg2Rad * segment * i;
                    var spikesPos = castPosition;
                    var x = radius * Mathf.Cos(phi);
                    var z = radius * Mathf.Sin(phi);
                    spikesPos.x += x;
                    spikesPos.z += z;
                    
                    StartCoroutine(ExecuteSpikesLifecycle(spikesPos));
                }

                _sphereCollider.radius += RadiusMultiplier;
                yield return new WaitForSeconds(Seconds);
                
                radius += RadiusMultiplier;
                spikesQuantity *= 2;
            }

            yield return new WaitForSeconds(GrowthTime * 2 + SpikesLifeTime);
            
            IEnumerator ExecuteSpikesLifecycle(Vector3 position)
            {
                Instantiate(spawnEffect, position, Quaternion.identity, transform);
                var spikes = Instantiate(spikesPrefab, position, Quaternion.identity, transform);
                
                // Grow spikes
                var time = 0f;
                while (time < GrowthTime)
                {
                    var coefficient = Mathf.Lerp(0, 1, time / GrowthTime);
                    spikes.transform.localScale = _targetSpikesScale * coefficient;
                    time += Time.deltaTime;
                    yield return null;
                }

                yield return new WaitForSeconds(SpikesLifeTime);

                // Shrink spikes
                time = 0f;
                while (time < GrowthTime)
                {
                    var coefficient = Mathf.Lerp(1, 0, time / GrowthTime);
                    spikes.transform.localScale = _targetSpikesScale * coefficient;
                    time += Time.deltaTime;
                    yield return null;
                }
                Destroy(spikes.gameObject);
            }
        }

        

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Mob"))
                return;

            var mob = other.GetComponent<MobBehaviour>();
            mob.HitThisMob(SpikesDamage, BasicElement.Earth, "SpikesSpell");
            mob.AddSingleEffect(new StunEffect(1));
        }
    }
}