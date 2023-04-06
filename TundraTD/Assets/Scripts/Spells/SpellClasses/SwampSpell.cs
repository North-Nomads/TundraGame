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

        public override BasicElement Element => BasicElement.Earth | BasicElement.Water;

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
            if (other.CompareTag("Mob") && Mathf.Abs(other.transform.position.y - transform.position.y) < 1.5f)
            {
                mob_position_from_center = Vector3.Distance(transform.position, other.transform.position);
                if (mob_position_from_center >= 10)
                    slowdown_mob = 0.8f;
                else if (mob_position_from_center >= 9)
                    slowdown_mob = 0.75f;
                else if (mob_position_from_center >= 8)
                    slowdown_mob = 0.7f;
                else if (mob_position_from_center >= 7)
                    slowdown_mob = 0.65f;
                else if (mob_position_from_center >= 6)
                    slowdown_mob = 0.6f;
                else if (mob_position_from_center >= 5)
                    slowdown_mob = 0.55f;
                else if (mob_position_from_center >= 4)
                    slowdown_mob = 0.45f;
                else if (mob_position_from_center >= 3)
                    slowdown_mob = 0.4f;
                else if (mob_position_from_center >= 2)
                    slowdown_mob = 0.35f;
                else if (mob_position_from_center >= 1)
                    slowdown_mob = 0.3f;
                else if (mob_position_from_center >= 0.5)
                    slowdown_mob = 0.25f;
                else if (mob_position_from_center >= 0)
                    slowdown_mob = 0.2f;

                var mob = other.GetComponent<MobBehaviour>();
                mob.ClearMobEffects();
                if (!mob.CurrentEffects.OfType<SlownessEffect>().Any())
                    mob.AddSingleEffect(new SlownessEffect(slowdown_mob, slowdownTime.SecondsToTicks()));
            }
        }

    }
}