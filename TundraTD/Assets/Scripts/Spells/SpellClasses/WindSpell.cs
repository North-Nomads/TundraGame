using System.Collections;
using System.Collections.Generic;
using Level;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Spells.SpellClasses
{
    public class WindSpell : MagicSpell
    {
        [SerializeField] private SphereCollider mainCollider;
        
        private const float MaxCooldownTime = 0.5f;
        private const float HitDamage = 3f;
        private float _cooldownTime;
        private int _mobsAmount;
        private readonly MobBehaviour[] _mobsInSpell = new MobBehaviour[100];

        private float TornadoRadius => 6f;
        private float SpellDuration => 6f;
        private float SlownessValue => 0.4f;


        public override BasicElement Element => BasicElement.Air;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Mob"))
            {
                var mob = other.GetComponent<MobBehaviour>();
                _mobsInSpell[_mobsAmount] = mob;
                _mobsAmount++;
                mob.RemoveFilteredEffects(x => x is WetEffect);
                mob.AddReceivedEffects(new List<Effect>
                {
                    new SlownessEffect(SlownessValue, SpellDuration.SecondsToTicks()),
                    new InspirationEffect()
                });
            }
        }        

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            transform.position = hitInfo.point;
            mainCollider = GetComponent<SphereCollider>();
            mainCollider.radius = TornadoRadius;            
            StartCoroutine(StayAlive());
        }

        private void Update()
        {
            _cooldownTime -= Time.deltaTime;
            if (_cooldownTime >= 0)
                return;

            _cooldownTime = MaxCooldownTime;
            foreach (var mob in _mobsInSpell)
                if (!(mob is null))
                    mob.HitThisMob(HitDamage, BasicElement.Air);
        }

        private IEnumerator StayAlive()
        {
            yield return new WaitForSeconds(SpellDuration);
            // Remove slowness effects on spell ends
            for (int i = 0; i < _mobsAmount; i++)
            {
                var mob = _mobsInSpell[i].GetComponent<MobBehaviour>();
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
                mob.RemoveFilteredEffects(x => x is SlownessEffect);
            }
        }
    }
}