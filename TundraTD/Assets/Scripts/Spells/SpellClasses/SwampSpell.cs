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
	public class SwampSpell : MagicSpell
	{
        [SerializeField] private float lifetime;
        [SerializeField] private float slowdownTime;
        private float radius_swamp = 10;
        private float mob_position_from_center;
        private float slowdown_mob;
        private float slowdown_max = 0.8f;
        private float slowdown_min = 0.2f;

        public override BasicElement Element => BasicElement.Earth | BasicElement.Water;

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
            if (other.CompareTag("Mob") && Mathf.Abs(other.transform.position.y - transform.position.y) < 1.5f)
            {
                mob_position_from_center = Vector3.Distance(transform.position, other.transform.position);
                slowdown_mob = mob_position_from_center / radius_swamp * (slowdown_max - slowdown_min) + slowdown_min;

                var mob = other.GetComponent<MobBehaviour>();
                mob.ClearMobEffects();
                if (!mob.CurrentEffects.OfType<SlownessEffect>().Any())
                    mob.AddSingleEffect(new SlownessEffect(slowdown_mob, slowdownTime.SecondsToTicks()));
            }
        }

    }
}