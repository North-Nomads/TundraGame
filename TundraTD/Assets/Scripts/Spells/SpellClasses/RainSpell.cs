using System.Collections;
using Level;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Spells.SpellClasses
{
    public class RainSpell : MagicSpell
    {
        private const float RainHeight = 30f;

        [SerializeField] private Transform vfx;
        [SerializeField] private CapsuleCollider mainCollider;

        private float Radius { get; set; } = 10f;

        private float Lifetime { get; set; } = 3f;

        private float EffectTime { get; set; } = 10f;

        private float SlownessValue { get; set; } = 0.1f;

        public override BasicElement Element => BasicElement.Water;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            mainCollider.radius = Radius;
            mainCollider.height = RainHeight;
            transform.position = hitInfo.point;
            StartCoroutine(WaitTime());
        }

        private IEnumerator WaitTime()
        {
            yield return new WaitForSeconds(Lifetime);
            DisableEmissionOnChildren();
            mainCollider.enabled = false;
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Mob"))
            {
                var mob = other.gameObject.GetComponent<MobBehaviour>();
                ApplyEffects(mob);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Mob"))
            {
                var mob = collision.gameObject.GetComponent<MobBehaviour>();
                ApplyEffects(mob);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Mob"))
            {
                var mob = other.gameObject.GetComponent<MobBehaviour>();
                mob.RemoveFilteredEffects(x => x is BurningEffect effect && effect.CanBeExtinguished);
            }
        }

        private void ApplyEffects(MobBehaviour mob) =>
            mob.AddSingleEffect(new SlownessEffect(1 - SlownessValue, EffectTime.SecondsToTicks()));

    }
}