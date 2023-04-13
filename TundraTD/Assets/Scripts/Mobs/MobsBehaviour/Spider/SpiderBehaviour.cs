using Spells;
using UnityEngine;

namespace Mobs.MobsBehaviour.Spider
{
    /// <summary>
    ///
    /// </summary>
    [RequireComponent(typeof(MobModel))]
    public class SpiderBehaviour : MobBehaviour
    {
        public override BasicElement MobBasicElement => BasicElement.Water;
        public override BasicElement MobCounterElement => BasicElement.Lightning;

        protected override void HandleIncomeDamage(float damage, BasicElement damageElement)
        {
            var multiplier = 1f;
            if (damageElement == MobBasicElement)
                multiplier = 0.8f;
            else if (damageElement == MobCounterElement)
                multiplier = 1.2f;
            
            MobModel.CurrentMobHealth -= damage * multiplier;
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