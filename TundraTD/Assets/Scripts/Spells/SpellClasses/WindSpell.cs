using System.Collections;
using System.Linq;
using Level;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Spells.SpellClasses
{
    public class WindSpell : MagicSpell
    {
        [SerializeField] private SphereCollider mainCollider;
        
        private const float HitDelay = 0.5f;
        private int _mobsAmount;
        private readonly MobBehaviour[] _mobsInSpell = new MobBehaviour[100];
        

        private float TornadoRadius { get; set; } = 6f;
        private float SpellDuration { get; set; } = 6f;
        private float SlownessValue { get; set; } = 0.4f;
        

        public override BasicElement Element => BasicElement.Air;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Mob"))
            {
                print(1);
                var mob = other.GetComponent<MobBehaviour>();
                _mobsInSpell[_mobsAmount] = mob;
                _mobsAmount++;
                mob.RemoveFilteredEffects(x => x is WetEffect);
                mob.AddSingleEffect(new SlownessEffect(SlownessValue, SpellDuration.SecondsToTicks()));
            }
        }        

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            transform.position = hitInfo.point;
            mainCollider = GetComponent<SphereCollider>();
            mainCollider.radius = TornadoRadius;            
            StartCoroutine(StayAlive());
        }

        IEnumerator StayAlive()
        {
            yield return new WaitForSeconds(SpellDuration);
            // Remove slowness effects on spell ends
            for (int i = 0; i < _mobsAmount; i++)
            {
                var mob = _mobsInSpell[i].GetComponent<MobBehaviour>();
                if (!mob.CurrentEffects.OfType<SlownessEffect>().Any())
                    mob.RemoveFilteredEffects(x => x is SlownessEffect);
            }
            Destroy(gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            // If the leaving object is mob -> remove it from the list and remove slowness from him
            if (other.CompareTag("Mob"))
            {
                _mobsInSpell[_mobsAmount] = null;
                _mobsAmount--;
                var mob = other.GetComponent<MobBehaviour>();
                if (!mob.CurrentEffects.OfType<SlownessEffect>().Any())
                    mob.RemoveFilteredEffects(x => x is SlownessEffect effect);
            }
        }
    }
}