using System.Collections;
using Level;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Spells.SpellClasses
{
	public class SpikesSpell : MagicSpell
    {
        [SerializeField] private GameObject spikeVisualEffect;
        [SerializeField] private GameObject pebblesVisualEffect;
        private const int CirclesAmount = 3;
        private const float RadiusMultiplier = 1.5f;
        private const float Seconds = .07f;
        private const float SpikesDamage = 35f;
        private bool _isInstantiated;
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
            
            Instantiate(spikeVisualEffect, castPosition, Quaternion.identity, transform);
            Instantiate(pebblesVisualEffect, castPosition, Quaternion.Euler(90, 0, 0), transform);
             
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

                    Instantiate(spikeVisualEffect, spikesPos, Quaternion.identity, transform);
                    Instantiate(pebblesVisualEffect, spikesPos, Quaternion.Euler(90, 0, 0), transform);
                }

                _sphereCollider.radius += RadiusMultiplier;
                yield return new WaitForSeconds(Seconds);
                
                radius += RadiusMultiplier;
                spikesQuantity *= 2;
            }

            Debug.Log("Instantited");
            _isInstantiated = true;
            yield return new WaitForSeconds(3.3f); // 3.3f - max time pebbles (that live longer than the spike) can live (random.uniform) 
            Destroy(gameObject);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Mob"))
                return;

            var mob = other.GetComponent<MobBehaviour>();
            Debug.Log($"{_isInstantiated}: {mob}");
            if (_isInstantiated)
            {
                mob.AddSingleEffect(new SlownessEffect(0.4f, 3f.SecondsToTicks()));
            }
            else
            {
                mob.AddSingleEffect(new StunEffect(1f.SecondsToTicks()));    
                mob.HitThisMob(SpikesDamage, BasicElement.Earth);
            }
        }
    }
}