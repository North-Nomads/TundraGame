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


        private static readonly Collider[] AvailableTargetsPool = new Collider[1000];

        [SerializeField] private GameObject TornadoPrefab;
        [SerializeField] private SphereCollider mainCollider;
        [SerializeField] private AudioClip TornadoSound;

        private float TornadoRadius { get; set; } = 16f;

        private float SpellDuration { get; set; } = 6f;                
        
        public float SlownessValue { get; set; } = 0.2f;

        public float SlownessDuration { get; set; } = 6f;

        public override BasicElement Element => BasicElement.Air;

       
        private IEnumerator WaitTime()
        {
            yield return new WaitForSeconds(SpellDuration);
            mainCollider.enabled = false;
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Mob"))
            {
                var mob = other.GetComponent<MobBehaviour>();
                if (!mob.CurrentEffects.OfType<SlownessEffect>().Any())
                    mob.AddSingleEffect(new SlownessEffect(SlownessValue, SlownessDuration.SecondsToTicks()));
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Mob"))
            {
                var mob = other.gameObject.GetComponent<MobBehaviour>();
                mob.RemoveFilteredEffects(x => x is WetEffect effect);
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
            yield return new WaitForSeconds(SpellDuration);
            Destroy(gameObject);
        }
        
    }
}