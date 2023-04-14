using UnityEngine;

namespace Mobs.MobsBehaviour.Spider
{
    /// <summary>
    ///
    /// </summary>
    [RequireComponent(typeof(MobModel))]
    public class SpiderBehaviour : MobBehaviour
    {
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