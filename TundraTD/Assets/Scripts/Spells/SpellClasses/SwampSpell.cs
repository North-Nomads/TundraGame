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
                var mob = other.GetComponent<MobBehaviour>();
                if (!mob.CurrentEffects.OfType<SlownessEffect>().Any())
                    mob.AddSingleEffect(new SlownessEffect(0.5f, slowdownTime.SecondsToTicks()));
            }
        }

    }
}