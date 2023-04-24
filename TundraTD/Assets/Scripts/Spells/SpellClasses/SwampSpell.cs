using System.Collections;
using System.Linq;
using Level;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Spells.SpellClasses
{
	public class SwampSpell : MagicSpell
	{
        [SerializeField] private float lifetime;
        [SerializeField] private float slowdownTime;
        private const float SwampRadius = 10;
        private const float MaxSlownessModifier = 0.8f;
        private const float MinSlownessModifier = 0.2f;
        
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
            if (other.CompareTag("Mob"))
            {
                var mobPositionFromCenter = Vector3.Distance(transform.position, other.transform.position);
                var slowdownMob = mobPositionFromCenter / SwampRadius * (MaxSlownessModifier - MinSlownessModifier) + MinSlownessModifier;

                var mob = other.GetComponent<MobBehaviour>();
                mob.ClearMobEffects();
                if (!mob.CurrentEffects.OfType<SlownessEffect>().Any())
                    mob.AddSingleEffect(new SlownessEffect(slowdownMob, slowdownTime.SecondsToTicks()));
            }
        }

    }
}