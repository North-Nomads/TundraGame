using System.Collections;
using Level;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using UnityEngine;
using UnityEngine.VFX;

namespace Spells.SpellClasses
{
	public class SpikesSpell : MagicSpell
    {
        [SerializeField] private VisualEffect[] spawnEffects;
        private const int CirclesAmount = 3;
        private const float RadiusMultiplier = 1.5f;
        private const float Seconds = .07f;
        private const float SpikesLifeTime = 1f;
        private const float SpikesDamage = 35f;
        private SphereCollider _sphereCollider;
        
        public override BasicElement Element => BasicElement.Earth;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            var castPosition = hitInfo.point;
            _sphereCollider = GetComponent<SphereCollider>();
            StartCoroutine(StartRadialSpikesSpawning(castPosition));
        }

        private IEnumerator StartRadialSpikesSpawning(Vector3 castPosition)
        {
            var radius = RadiusMultiplier;
            var spikesQuantity = 6;
            
            foreach (var spawnEffect in spawnEffects)
            {
                Instantiate(spawnEffect, castPosition, Quaternion.identity, transform);
            }
            
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

                    foreach (var spawnEffect in spawnEffects)
                    {
                        Instantiate(spawnEffect, spikesPos, Quaternion.identity, transform);
                    }
                }

                _sphereCollider.radius += RadiusMultiplier;
                yield return new WaitForSeconds(Seconds);
                
                radius += RadiusMultiplier;
                spikesQuantity *= 2;
            }
            
            yield return new WaitForSeconds(3.3f); // 3.3f - max time pebbles (that live longer than the spike) can live (random.uniform) 
            Destroy(gameObject);
        }

        

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Mob"))
                return;

            var mob = other.GetComponent<MobBehaviour>();
            mob.HitThisMob(SpikesDamage, BasicElement.Earth);
            mob.AddSingleEffect(new StunEffect(1f.SecondsToTicks()));
        }
    }
}