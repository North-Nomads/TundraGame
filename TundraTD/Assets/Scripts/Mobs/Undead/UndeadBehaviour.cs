using Spells;
using UnityEngine;

namespace Mobs.Undead
{
    /// <summary>
    /// 
    /// </summary>
    [RequireComponent(typeof(MobModel))]
    public class UndeadBehaviour : MobBehaviour
    {
        [SerializeField] private Transform gates;
        private const BasicElement MobElement = BasicElement.Water;
        private const BasicElement MobCounterElement = BasicElement.Lightning;

        private MobModel _mobModel;

        public override BasicElement MobBasicElement => BasicElement.Water;
        
        public override void MoveTowards(Vector3 point)
        {
            _mobModel.MobNavMeshAgent.SetDestination(point);
        }

        public override void HandleIncomeDamage(float damage, BasicElement damageElement)
        {
            float multiplier;
            switch (damageElement)
            {
                case MobElement:
                    multiplier = 0.8f;
                    break;
                case MobCounterElement:
                    multiplier = 1.2f;
                    break;
                default:
                    multiplier = 1;
                    break;
            }

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