using Level;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Spells
{
    /// <summary>
    /// The spell with lava pool which hits all enemies who are standing on it.
    /// </summary>
	public class LavaSpell : MagicSpell
	{
        [SerializeField] private float lifetime;
        [SerializeField] private float damage;
        [SerializeField] private float burnDamage;
        [SerializeField] private float burnTime;

        public override BasicElement Element => BasicElement.Fire;

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
            // Check if the enemy is mob and it's walking on the pool
            if (other.CompareTag("Mob") && Mathf.Abs(other.transform.position.y - transform.position.y) < 1.5f)
            {
                var mob = other.GetComponent<MobBehaviour>();
                if (!mob.CurrentEffects.OfType<BurnEffect>().Any())
                    mob.AddSingleEffect(new BurnEffect(burnDamage, burnTime.SecondsToTicks()));
            }
        }
    }
}