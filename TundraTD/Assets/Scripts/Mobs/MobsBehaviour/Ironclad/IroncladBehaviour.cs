using Spells;
using UnityEngine;

namespace Mobs.MobsBehaviour.Ironclad
{
    /// <summary>
    /// 
    /// </summary>
    [RequireComponent(typeof(MobModel))]
    public class IroncladBehaviour : MobBehaviour
    {
        [SerializeField] private Transform gates;
        private const BasicElement MobElement = BasicElement.Earth;
        private const BasicElement MobCounterElement = BasicElement.Air;
        
        private float _mobShield;
        private float MobShield
        {
            get => _mobShield;
            set
            {
                if (value < 0)
                    _mobShield = 0; 
                else
                    _mobShield = value;
            }
        }

        private MobModel _mobModel;

        public override BasicElement MobBasicElement => BasicElement.Earth;
        
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
            
            if (MobShield > 0)
            {
                MobShield -= damage * multiplier;
                return;
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
            MobShield = 10;
        }
        
        private void FixedUpdate()
        {
            MoveTowards(gates.position);
            
            if (_mobModel.CurrentMobHealth <= 0)
                KillThisMob();
            
            if (CurrentEffects.Count > 0)
                TickTimer -= Time.fixedDeltaTime;
        }
    }
}