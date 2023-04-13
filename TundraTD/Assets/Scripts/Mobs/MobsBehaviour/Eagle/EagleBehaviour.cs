using System;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using Spells;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mobs.MobsBehaviour.Eagle
{
    [RequireComponent(typeof(MobModel))]
    public class EagleBehaviour : MobBehaviour
    {
        public override BasicElement MobBasicElement => BasicElement.Air;
        public override BasicElement MobCounterElement => BasicElement.Water;

        protected override void HandleIncomeDamage(float damage, BasicElement damageElement)
        {
            if (damageElement != BasicElement.Fire)
            {
                if (CurrentEffects.Any(x => x is WetEffect))
                {
                    var multiplier = 1f;
                    if (damageElement == MobBasicElement)
                        multiplier = 0.8f;
                    else if (damageElement == MobCounterElement)
                        multiplier = 1.2f;
                    MobModel.CurrentMobHealth -= damage * multiplier;
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

