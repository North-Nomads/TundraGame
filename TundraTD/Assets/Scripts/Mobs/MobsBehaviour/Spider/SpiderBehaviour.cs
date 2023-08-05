using Mobs.MobEffects;
using UnityEngine;

namespace Mobs.MobsBehaviour.Spider
{
    /// <summary>
    ///
    /// </summary>
    [RequireComponent(typeof(MobModel))]
    public class SpiderBehaviour : MobBehaviour
    {
        [SerializeField] SpiderWalkIK walkController;
        public override void ExecuteOnMobSpawn(MobPortal mobPortal)
        {
            MobPortal = mobPortal;
            MobModel.InstantiateMobModel();
        }

        private void StunCheck()
        {
            foreach (var effect in CurrentEffects)
            {
                if (effect.GetType() == typeof(StunEffect))
                {
                    walkController.Stunned = true;
                    return;
                }
            }
            walkController.Stunned = false;
        }

        private void FixedUpdate()
        {
            StunCheck();
            MoveTowardsNextPoint();
            HandleTickTimer();
        }
    }
}