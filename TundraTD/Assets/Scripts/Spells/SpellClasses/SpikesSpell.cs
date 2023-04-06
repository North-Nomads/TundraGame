using System.Collections;
using UnityEngine;

namespace Spells.SpellClasses
{
	public class SpikesSpell : MagicSpell
    {
        private const int CirclesAmount = 3;
        private const float RadiusMultiplier = .9f;
        private const float Seconds = .07f;

        
        [SerializeField] private Transform spikesPrefab;

        public override BasicElement Element => BasicElement.Earth;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            var castPosition = hitInfo.point;
            StartCoroutine(StartRadialSpikesSpawning(castPosition));
        }

        private IEnumerator StartRadialSpikesSpawning(Vector3 castPosition)
        {
            var radius = RadiusMultiplier;
            var spikesQuantity = 6;

            StartCoroutine(GrowSpikesPrefab(castPosition));
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
                    
                    StartCoroutine(GrowSpikesPrefab(spikesPos));
                }
                yield return new WaitForSeconds(Seconds);
                
                radius += RadiusMultiplier;
                spikesQuantity *= 2;
            }

            IEnumerator GrowSpikesPrefab(Vector3 position)
            {
                var spikes = Instantiate(spikesPrefab, position, Quaternion.identity, transform);
                yield return null;

            }
        }
    }
}