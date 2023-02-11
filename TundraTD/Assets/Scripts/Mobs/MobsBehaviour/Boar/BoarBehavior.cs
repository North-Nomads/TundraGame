using Spells;
using UnityEngine;

namespace Mobs.MobsBehaviour.Boar
{
    /// <summary>
    /// Handles boar mob movement and taking and dealing damage behaviour
    /// </summary>
    [RequireComponent(typeof(MobModel))]
    public class BoarBehavior : MobBehaviour
    {
        // private MobModel MobModel;
        private bool _canDistractFromCurrentTarget;

        private float _chargeLeftTime;

        public override BasicElement MobBasicElement => BasicElement.Earth;
        public override BasicElement MobCounterElement => BasicElement.Air;

        public override void HandleIncomeDamage(float damage, BasicElement damageElement)
        {
            var multiplier = 1f;
            if (damageElement == MobBasicElement)
                multiplier = 0.8f;
            else if (damageElement == MobCounterElement)
                multiplier = 1.2f;

            MobModel.CurrentMobHealth -= damage * multiplier;

            if (MobModel.CurrentMobHealth <= 0)
                KillThisMob();
        }

        public override void MoveTowards(Vector3 point)
        {
            MobModel.MobNavMeshAgent.SetDestination(point);
        }

        public override void ExecuteOnMobSpawn(Transform gates, MobPortal mobPortal)
        {
            MobPortal = mobPortal;
            MobModel.InstantiateMobModel();
            _canDistractFromCurrentTarget = true;
            _chargeLeftTime = 3f;

            DefaultDestinationPoint = gates;
            MobModel.MobNavMeshAgent.SetDestination(DefaultDestinationPoint.position);
        }

        private void FixedUpdate()
        {
            if (_chargeLeftTime > 0)
                _chargeLeftTime -= Time.fixedDeltaTime;

            if (_chargeLeftTime <= 0 && _canDistractFromCurrentTarget)
                TakeChargeMode();

            if (CurrentEffects.Count > 0)
                TickTimer -= Time.fixedDeltaTime;
        }

        private void TakeChargeMode()
        {
            _canDistractFromCurrentTarget = false;
            MobModel.CurrentMobSpeed *= 1.5f;
            MobModel.CurrentMobDamage *= 2f;
        }
    }
}