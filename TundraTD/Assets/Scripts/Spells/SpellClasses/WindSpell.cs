using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Level;
using UnityEngine.Internal;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spells
{
    public class WindSpell : MagicSpell
    {

        private const float HitDelay = 0.5f;
        private const int MobsLayerMask = 1 << 8;


        private static readonly Collider[] mobs = new Collider[100];

        [SerializeField] private GameObject TornadoPrefab;
        [SerializeField] private SphereCollider mainCollider;
        [SerializeField] private AudioClip TornadoSound;

        private float TornadoRadius { get; set; } = 6f;

        private float SpellDuration { get; set; } = 6f;                
        
        public float SlownessValue { get; set; } = 0.2f;
        

        public override BasicElement Element => BasicElement.Air;    
       

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Mob"))
            {
                var mob = other.GetComponent<MobBehaviour>();
                if (!mob.CurrentEffects.OfType<WetEffect>().Any())                    
                    mob.RemoveFilteredEffects(x => x is WetEffect effect);

                mob.AddSingleEffect(new SlownessEffect(SlownessValue, SpellDuration.SecondsToTicks()));
            }
        }        

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            transform.position = hitInfo.point;
            mainCollider = GetComponent<SphereCollider>();
            mainCollider.radius = TornadoRadius;            
            StartCoroutine(StayAlive());
        }

        IEnumerator StayAlive()
        {
            int mobsAmount = Physics.OverlapSphereNonAlloc(transform.position, mainCollider.radius, mobs, MobsLayerMask);
            for (int i = 0; i < mobsAmount; i++)
            {
                var mob = mobs[i].GetComponent<MobBehaviour>();
                mob.AddSingleEffect(new SlownessEffect(SlownessValue, SpellDuration.SecondsToTicks()));
            }
            yield return new WaitForSeconds(SpellDuration);
            
            for (int i = 0; i < mobsAmount; i++)
            {
                var mob = mobs[i].GetComponent<MobBehaviour>();
                if (!mob.CurrentEffects.OfType<SlownessEffect>().Any())
                    mob.RemoveFilteredEffects(x => x is SlownessEffect effect);
            }
            Destroy(gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Mob") && Mathf.Abs(other.transform.position.y - transform.position.y) < 1.5f)
            {
                var mob = other.GetComponent<MobBehaviour>();
                if (!mob.CurrentEffects.OfType<SlownessEffect>().Any())
                    mob.RemoveFilteredEffects(x => x is SlownessEffect effect);
            }
        }
    }
}