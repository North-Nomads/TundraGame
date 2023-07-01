using UnityEngine;
using UnityEngine.VFX;

namespace Mobs.MobsBehaviour.Boar
{
    /// <summary>
    /// Handles boar mob movement and taking and dealing maxDamage behaviour
    /// </summary>
    [RequireComponent(typeof(MobModel))]
    public class BoarBehaviour : MobBehaviour
    {

        [SerializeField] private Transform gasSpawnPosition;
        [SerializeField] private VisualEffect gasVFX;
        
        private float _chargeLeftTime;
        private bool _isCharged;
        
        public override void ExecuteOnMobSpawn(MobPortal mobPortal)
        {
            MobPortal = mobPortal;
            MobModel.InstantiateMobModel();
            _chargeLeftTime = 3f;
        }

        private void FixedUpdate()
        {
            MoveTowardsNextPoint();
            HandleTickTimer();
            
            if (_chargeLeftTime > 0)
                _chargeLeftTime -= Time.fixedDeltaTime;

            if (_chargeLeftTime <= 0 && !_isCharged)
            {
                TakeChargeMode();
                _isCharged = true;
            }
        }

        private void TakeChargeMode()
        {
            Instantiate(gasVFX, gasSpawnPosition);
            MobModel.CurrentMobSpeed *= 1.5f;
            MobModel.CurrentMobDamage *= 2f;
        }
    }
}