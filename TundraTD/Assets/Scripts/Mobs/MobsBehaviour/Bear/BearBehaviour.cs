using Spells;
using UnityEngine;

namespace Mobs.MobsBehaviour.Bear
{
    /// <summary>
    ///
    /// </summary>
    [RequireComponent(typeof(MobModel))]
    public class BearBehaviour : MobBehaviour
    {
        [SerializeField] private float mobShield;

        private float MobShield
        {
            get => mobShield;
            set
            {
                if (value < 0)
                    mobShield = 0;
                else
                    mobShield = value;
            }
        }

        protected override void HandleIncomeDamage(float damage, BasicElement damageElement)
        {
            if (MobShield > 0 && !float.IsPositiveInfinity(damage))
                MobShield -= damage;
            else
                MobModel.CurrentMobHealth -= damage;
        }

        public override void ExecuteOnMobSpawn(MobPortal mobPortal)
        {
            MobPortal = mobPortal;
            MobModel.InstantiateMobModel();
        }

        private void FixedUpdate()
        {
            MoveTowardsNextPoint();
            if (CurrentEffects.Count > 0)
                TickTimer -= Time.fixedDeltaTime;
        }
    }
}