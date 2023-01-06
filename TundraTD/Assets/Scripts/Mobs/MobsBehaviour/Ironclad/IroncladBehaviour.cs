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
        public override BasicElement MobCounterElement =>  BasicElement.Air;

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