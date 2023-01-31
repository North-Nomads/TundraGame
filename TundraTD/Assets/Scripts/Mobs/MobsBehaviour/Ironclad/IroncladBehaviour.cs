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
        public override BasicElement MobCounterElement =>  BasicElement.Air;
        
        
        public override void MoveTowards(Vector3 point)
        {
            MobModel.MobNavMeshAgent.SetDestination(point);
        }
        
        private void FixedUpdate()
        {
            if (CurrentEffects.Count > 0)
                TickTimer -= Time.fixedDeltaTime;
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
            
            MobModel.CurrentMobHealth -= damage * multiplier;
            print($"{name}: {MobModel.CurrentMobHealth}, Damage dealt: {damage}, element: {damageElement}");
            
            if (MobModel.CurrentMobHealth <= 0)
                KillThisMob();
        }
        
        public override void ExecuteOnMobSpawn(Transform gates, MobPortal mobPortal)
        {
            MobModel = GetComponent<MobModel>();
            MobModel.InstantiateMobModel();
            MobPortal = mobPortal;
            MobShield = 10;
            DefaultDestinationPoint = gates;
            MobModel.MobNavMeshAgent.SetDestination(DefaultDestinationPoint.position);
        }
    }
}