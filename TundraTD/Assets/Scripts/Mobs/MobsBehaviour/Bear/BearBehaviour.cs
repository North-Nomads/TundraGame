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
        [SerializeField] private Transform armorStand;

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
            {
                MobShield -= damage;
                if (MobShield <= 0)
                {
                    ExecuteShieldCrackVfx();
                }
            }
            else
            {
                MobModel.CurrentMobHealth -= damage;
            }
                
        }

        private void ExecuteShieldCrackVfx()
        {
            armorStand.gameObject.SetActive(false);
        }

        public override void ExecuteOnMobSpawn(MobPortal mobPortal)
        {
            MobPortal = mobPortal;
            MobModel.InstantiateMobModel();
        }

        private void FixedUpdate()
        {
            MoveTowardsNextPoint();
            HandleTickTimer();
        }
    }
}