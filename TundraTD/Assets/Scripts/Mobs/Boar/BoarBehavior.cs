using UnityEngine;

namespace Mobs.Boar
{
    /// <summary>
    /// 
    /// </summary>
    [RequireComponent(typeof(MobModel))]
    public class BoarBehavior : MobBehaviour
    {
        [SerializeField] private Transform gates;

        private MobModel _mobModel;
        private bool _canDistractFromCurrentTarget;
        private float _chargeLeftTime;
        
        public override void HandleAppliedEffects()
        {
            throw new System.NotImplementedException();
        }

        public override void ApplyReceivedEffects()
        { }

        public override void MoveTowards(Vector3 point)
        {
            _mobModel.MobNavMeshAgent.SetDestination(point);
        }

        public override void HandleIncomeDamage(float damage)
        {
            _mobModel.CurrentMobHealth -= damage;
        }

        public override void KillThisMob()
        {
            Destroy(gameObject);
        }

        private void Start()
        {
            _mobModel = GetComponent<MobModel>();
            _canDistractFromCurrentTarget = true;
            _chargeLeftTime = 3f;
        }
        
        private void FixedUpdate()
        {
            if (_chargeLeftTime > 0)
                _chargeLeftTime -= Time.fixedDeltaTime;

            if (_chargeLeftTime <= 0 && _canDistractFromCurrentTarget)
                TakeChargeMode();
                
            MoveTowards(gates.position);
        }
        
        private void TakeChargeMode()
        {
            _canDistractFromCurrentTarget = false;
            _mobModel.CurrentMobSpeed *= 1.5f;
            _mobModel.CurrentMobDamage *= 2f;
        }
    }
}