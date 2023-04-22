using Level;
using Mobs.MobsBehaviour;
using Mobs.MobEffects;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Spells
{
    /// <summary>
    /// The spell with lava pool which hits all enemies who are standing on it.
    /// </summary>
	public class LavaSpell : MagicSpell
	{
        private const int MobsLayerMask = 1 << 8;

        [SerializeField] private float lifetime;
        [SerializeField] private float damage;
        [SerializeField] private float damageDelay;
        [SerializeField] private float burnDamage;
        [SerializeField] private float burnTime;

        private Collider[] mobs = new Collider[100];
        private CapsuleCollider interactionCollider;

        public override BasicElement Element => BasicElement.Fire;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            transform.position = hitInfo.point;
            interactionCollider = GetComponent<CapsuleCollider>();
            StartCoroutine(StayAlive());
        }

        IEnumerator StayAlive()
        {
            float time = 0;
            while (time < lifetime)
            {
                yield return new WaitForSeconds(damageDelay);
                int mobsAmount = Physics.OverlapSphereNonAlloc(transform.position, interactionCollider.radius, mobs, MobsLayerMask);
                for (int i = 0; i < mobsAmount; i++)
                {
                    var mob = mobs[i].GetComponent<MobBehaviour>();
                    mob.HitThisMob(damage, BasicElement.Fire);
                }
                time += damageDelay;
            }
            int amount = Physics.OverlapSphereNonAlloc(transform.position, interactionCollider.radius, mobs, MobsLayerMask);
            for (int i = 0; i < amount; i++)
            {
                var mob = mobs[i].GetComponent<MobBehaviour>();
                if (!mob.CurrentEffects.OfType<BurningEffect>().Any())
                    mob.AddSingleEffect(new BurningEffect(burnDamage, burnTime.SecondsToTicks()));
            }
            interactionCollider.enabled = false;
            DisableEmissionOnChildren();
            yield return new WaitForSeconds(5);
            Destroy(gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            // Check if the enemy is mob and it's walking on the pool
            if (other.CompareTag("Mob") && Mathf.Abs(other.transform.position.y - transform.position.y) < 1.5f)
            {
                var mob = other.GetComponent<MobBehaviour>();
                if (!mob.CurrentEffects.OfType<BurningEffect>().Any())
                    mob.AddSingleEffect(new BurningEffect(burnDamage, burnTime.SecondsToTicks()));
            }
        }
    }
}