using Mobs.MobEffects;
using Spells;
using System.Linq;
using UnityEngine;

namespace Mobs.MobsBehaviour.Eagle
{
    [RequireComponent(typeof(MobModel))]
    public class EagleBehaviour : MobBehaviour
    {


        protected override void HandleIncomeDamage(float damage, BasicElement damageElement)
        {
            if (damageElement != BasicElement.Fire)
            {
                if (CurrentEffects.Any(x => x is WetEffect))
                {
                    MobModel.CurrentMobHealth -= damage;
                }
            }
                
        }

        public override void ExecuteOnMobSpawn(MobPortal mobPortal)
        {
            MobPortal = mobPortal;
            MobModel.InstantiateMobModel();
        }

        private void FixedUpdate()
        {
            MoveTowardsNextPoint();
        }
    }
}

