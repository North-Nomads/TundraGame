using Level;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using System;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
namespace Spells
{
    public class StormCloudSpell : MagicSpell
    {
        [SerializeField] private float lifetime;
        [SerializeField] private float stormCloudDamage;
        [SerializeField] private float delayLightning;
        float countDown = 0;

        public override BasicElement Element => BasicElement.Lightning | BasicElement.Water;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            transform.position = hitInfo.point;
            StartCoroutine(StayAlive());
        }

        IEnumerator StayAlive()
        {
            yield return new WaitForSeconds(lifetime);
            Destroy(gameObject);
        }

        private void OnTriggerStay(Collider other)
        {
            countDown -= Time.deltaTime;
            if (countDown < 0 && other.CompareTag("Mob"))
            {
                var mob = other.GetComponent<MobBehaviour>();
                mob.ClearMobEffects();
                mob.HitThisMob(stormCloudDamage, BasicElement.Lightning, "StormCloudDamage");
                mob.AddSingleEffect(new SpikesStunEffect(1));
                countDown = delayLightning;
            }
        }

    }
}