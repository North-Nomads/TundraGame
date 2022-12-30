using System;
using UnityEngine;

namespace Mobs.Boar
{
    [RequireComponent(typeof(MobModel))]
    public class BoarBehavior : MobBehaviour
    {
        [SerializeField] private Transform gates;

        private MobModel _mobModel;
        private bool _canDistractFromCurrentTarget;
        private float _currentSprintDelay;
        
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

        public override void KillThisMob()
        {
            throw new System.NotImplementedException();
        }

        private void Start()
        {
            _mobModel = GetComponent<MobModel>();
            _canDistractFromCurrentTarget = true;
            _currentSprintDelay = 3f;
        }
        
        private void FixedUpdate()
        {
            if (_currentSprintDelay > 0)
                _currentSprintDelay -= Time.fixedDeltaTime;

            if (_currentSprintDelay <= 0 && _canDistractFromCurrentTarget)
                TurnOnSprintingMode();
                
            MoveTowards(gates.position);
        }
        
        private void TurnOnSprintingMode()
        {
            _canDistractFromCurrentTarget = false;
            _mobModel.CurrentMobSpeed *= 1.5f;
        }
    }
}