using System.Collections;
using System.Collections.Generic;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Spells.SpellClasses.EarthSpell
{
    public class SpikesGroup : MonoBehaviour
    {
        [SerializeField] private float growthIncrement = 0.001f;
        [SerializeField] private ParticleSystem cloudEffect;
        [SerializeField] private Transform pebblesEffect;
        private const int MobsMask = 1 << 8;
        private readonly Collider[] _colliders = new Collider[10];

        public int StunTicks { get; set; }


        public IEnumerator InitializeSpikesGrowth()
        {
            float size = 0;
            float increment = 0;
            transform.localScale = Vector3.zero;
            while (size < 1)
            {
                size += increment;
                increment += growthIncrement;
                transform.localScale = Vector3.one * size;
                yield return null;
            }
        }

        public void PlayCloudAnimation()
        {
            cloudEffect.gameObject.SetActive(true);
        }
        
        public void ApplyStunOverlappedOnMobs(float stunDamage, int stunTicks)
        {
            var touches = Physics.OverlapBoxNonAlloc(transform.position, new Vector3(1.5f, 1, .5f), _colliders,
                Quaternion.identity, MobsMask);

            for (int i = 0; i < touches; i++)
            {
                var mob = _colliders[i].GetComponent<MobBehaviour>();
                mob.AddReceivedEffects(new List<Effect> { new SpikesStunEffect(stunTicks) });
            }
        }

        public void ExecutePebblesExplosion(float pebbleDamage, int pebbleStunTicks)
        {
            pebblesEffect.gameObject.SetActive(true);
            
            var touches = Physics.OverlapBoxNonAlloc(transform.position, new Vector3(2f, 1, .5f), _colliders,
                Quaternion.identity, MobsMask);
            
            for (int i = 0; i < touches; i++)
            {
                var mob = _colliders[i].GetComponent<MobBehaviour>();
                mob.AddReceivedEffects(new List<Effect> { new PebbleStunEffect(pebbleStunTicks, pebbleDamage) });
            }
        }

        public IEnumerator InitializeSpikesShrinking()
        {
            float size = 1;
            float increment = 0;
            transform.localScale = Vector3.one;
            while (size > 0)
            {
                size -= increment;
                increment += growthIncrement;
                transform.localScale = Vector3.one * size;
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}