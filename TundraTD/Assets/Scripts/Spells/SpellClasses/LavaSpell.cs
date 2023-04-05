using Level;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using System.Collections;
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
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Mob"))
            {
                other.GetComponent<MobBehaviour>().AddSingleEffect(new BurnEffect(burnDamage, burnTime.SecondsToTicks()));
            }
        }
    }
}