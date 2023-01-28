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
            print($"{name}: {_mobModel.CurrentMobHealth}, Damage dealt: {damage}, element: {damageElement}");
            
            if (_mobModel.CurrentMobHealth <= 0)
                KillThisMob();
        }

        public override void KillThisMob()
        {
            Destroy(gameObject);
        }

        public override void ExecuteOnMobSpawn(Transform gates)
        {
            _mobModel = GetComponent<MobModel>();
            _mobModel.InstantiateMobModel();
            
            MobShield = 10;
            DefaultDestinationPoint = gates;
            _mobModel.MobNavMeshAgent.SetDestination(DefaultDestinationPoint.position);
        }

        private void FixedUpdate()
        {
            if (_mobModel.CurrentMobHealth <= 0)
                KillThisMob();
            
            if (CurrentEffects.Count > 0)
                TickTimer -= Time.fixedDeltaTime;
        }
    }
}