using System.Collections;
using System.Linq;
using Level;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Spells.SpellClasses
{
    [RequireComponent(typeof(MeshCollider))]
	public class SwampSpell : MagicSpell
    {
        [SerializeField] private Transform swampVFX;
        [SerializeField] private float lifetime;
        private const float SwampRadius = 10;
        private const float MaxSlownessModifier = 0.8f;
        private const float MinSlownessModifier = 0.2f;
        
        public override BasicElement Element => BasicElement.Earth | BasicElement.Water;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            transform.position = hitInfo.point;
            //swampVFX.transform.position = hitInfo.point + Vector3.up;
            StartCoroutine(StayAlive());
        }

        private IEnumerator StayAlive()
        {
            yield return new WaitForSeconds(lifetime);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Mob"))
                other.GetComponent<MobBehaviour>().AddSingleEffect(new WetEffect(lifetime.SecondsToTicks()));
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Mob"))
                other.GetComponent<MobBehaviour>().RemoveFilteredEffects(x => x is WetEffect);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Mob"))
            {
                var mobPositionFromCenter = Vector3.Distance(transform.position, other.transform.position);
                var slowdownMob = mobPositionFromCenter / SwampRadius * (MaxSlownessModifier - MinSlownessModifier) + MinSlownessModifier;

                var mob = other.GetComponent<MobBehaviour>();
                mob.ClearMobEffects();
                ApplyAdditionalEffects(mob);
                if (!mob.CurrentEffects.OfType<SlownessEffect>().Any())
                    mob.AddSingleEffect(new SlownessEffect(slowdownMob, 1));
            }
        }

    }
}