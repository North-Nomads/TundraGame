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

        [SerializeField] private MeshRenderer TornadoMesh;
        [SerializeField] private GameObject TornadoPrefab;
        [SerializeField] private AudioClip TornadoSound;
        
        
        private float TornadoRadius { get; set; } = 16f;

        private float TornadoSpeed { get; set; } = 1f;

        private float SpellDuration { get; set; } = 6f;
                
        private float TornadoArea { get; set; } = 30f;

        public float SlownessValue { get; set; } = 0.9f;

        public int SlownessDuration { get; set; } = 6;

        public override BasicElement Element => BasicElement.Air;

        private IEnumerator WaitTime()
        {
            yield return new WaitForSeconds(SpellDuration);
            //mainCollider.enabled = false;
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Mob"))
            {
                var mob = other.GetComponent<MobBehaviour>();
                if (!mob.CurrentEffects.OfType<SlownessEffect>().Any())
                    mob.AddSingleEffect(new SlownessEffect(SlownessValue, SlownessDuration));
            }
        }

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            transform.position = hitInfo.point;
            StartCoroutine(StayAlive());

        }

        IEnumerator StayAlive()
        {
            yield return new WaitForSeconds(SpellDuration);
            Destroy(gameObject);
        }

    }
}