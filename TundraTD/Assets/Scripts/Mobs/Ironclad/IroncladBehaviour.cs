using UnityEngine;

namespace Mobs.Ironclad
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
        
        public override void HandleAppliedEffects()
        {
            throw new System.NotImplementedException();
        }

        public override void ApplyReceivedEffects()
        {
            throw new System.NotImplementedException();
        }

        public override void MoveTowards(Vector3 point)
        {
            _mobModel.MobNavMeshAgent.SetDestination(point);
        }

        public override void HandleIncomeDamage(float damage)
        {
            if (MobShield > 0)
            {
                MobShield -= damage;
                return;
            }

            _mobModel.CurrentMobHealth -= damage;
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
        }
    }
}