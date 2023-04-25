using System.Collections;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Spells.SpellClasses
{
    public class StormCloudSpell : MagicSpell
    {
        [SerializeField] private float lifetime;
        [SerializeField] private float zapDamage;
        [SerializeField] private float zapCooldownTime;
        private float _zapTimer = 0;

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

        private void Update()
        {
            if (_zapTimer >= 0)
                _zapTimer -= Time.deltaTime;
        }

        private void OnTriggerStay(Collider other)
        {
            if (_zapTimer < 0 && other.CompareTag("Mob"))
            {
                var mob = other.GetComponent<MobBehaviour>();
                mob.ClearMobEffects();
                mob.HitThisMob(zapDamage, BasicElement.Lightning);
                mob.AddSingleEffect(new StunEffect(1));
                _zapTimer = zapCooldownTime;
            }
        }

    }
}