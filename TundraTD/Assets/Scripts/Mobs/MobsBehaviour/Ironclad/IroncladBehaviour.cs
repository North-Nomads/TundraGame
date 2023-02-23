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

        public override BasicElement MobBasicElement => BasicElement.Earth;
        public override BasicElement MobCounterElement => BasicElement.Air;

        public override void MoveTowards(Vector3 point)
        {
            MobModel.MobNavMeshAgent.SetDestination(point);
        }

        protected override void HandleIncomeDamage(float damage, BasicElement damageElement)
        {
            var multiplier = 1f;
            if (damageElement == MobBasicElement)
                multiplier = 0.8f;
            else if (damageElement == MobCounterElement)
                multiplier = 1.2f;

            var modifiedDamage = damage * multiplier;
            
            if (MobShield > 0 && !float.IsPositiveInfinity(damage))
            {
                MobShield -= modifiedDamage;
            }
            else
            {
                MobModel.CurrentMobHealth -= modifiedDamage;    
            }

            
        }

        public override void EnableDisorientation()
        {
            throw new System.NotImplementedException();
        }

        public override void ExecuteOnMobSpawn(Transform gates, MobPortal mobPortal)
        {
            MobModel.InstantiateMobModel();

            MobShield = 10;
            DefaultDestinationPoint = gates;
            MobModel.MobNavMeshAgent.SetDestination(DefaultDestinationPoint.position);
        }

        private void FixedUpdate()
        {
            if (CurrentEffects.Count > 0)
                TickTimer -= Time.fixedDeltaTime;
        }
    }
}