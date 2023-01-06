using Spells;
using UnityEngine;

namespace Mobs.MobsBehaviour.Undead
{
    /// <summary>
    /// 
    /// </summary>
    [RequireComponent(typeof(MobModel))]
    public class UndeadBehaviour : MobBehaviour
    {
        [SerializeField] private Transform gates;
        private MobModel _mobModel;

        public override BasicElement MobBasicElement => BasicElement.Water;
        public override BasicElement MobCounterElement => BasicElement.Lightning;

        public override void MoveTowards(Vector3 point)
        {
            _mobModel.MobNavMeshAgent.SetDestination(point);
        }

        public override void HandleIncomeDamage(float damage, BasicElement damageElement)
        {
            var multiplier = 1f;
            if (damageElement == MobBasicElement)
                multiplier = 0.8f;
            else if  (damageElement == MobCounterElement)
                multiplier = 1.2f;

            _mobModel.CurrentMobHealth -= damage * multiplier;
            print($"{name}: {_mobModel.CurrentMobHealth}");
            
            if (_mobModel.CurrentMobHealth <= 0)
                KillThisMob();
        }

        public override void KillThisMob()
        {
            Destroy(gameObject);
        }

        private void Start()
        {
            _mobModel = GetComponent<MobModel>();
        }

        private void FixedUpdate()
        {
            MoveTowards(gates.position);
            
            if (CurrentEffects.Count > 0)
                TickTimer -= Time.fixedDeltaTime;
        }
    }
}