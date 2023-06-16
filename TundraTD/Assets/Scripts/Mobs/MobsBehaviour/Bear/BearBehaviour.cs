using Spells;
using UnityEngine;
using UnityEngine.VFX;

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
        [SerializeField] private GameObject armorDestroyVFX;

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
                print($"{MobModel.CurrentMobHealth}, {damage}");
                MobShield -= damage;
                if (MobShield <= 0)
                {
                    print($"{MobModel.CurrentMobHealth}, {mobShield}");
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
            Instantiate(armorDestroyVFX, transform.position, Quaternion.identity);
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