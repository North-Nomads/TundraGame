﻿using Spells;
using UnityEngine;

namespace Mobs.MobsBehaviour.Undead
{
    /// <summary>
    ///
    /// </summary>
    [RequireComponent(typeof(MobModel))]
    public class UndeadBehaviour : MobBehaviour
    {
        public override BasicElement MobBasicElement => BasicElement.Water;
        public override BasicElement MobCounterElement => BasicElement.Lightning;

        public override void MoveTowards(Vector3 point)
        {
            MobModel.MobNavMeshAgent.SetDestination(point);
        }

        protected override void HandleIncomeDamage(float damage, BasicElement damageElement)
        {
            var multiplier = 1f;
            if (damageElement == MobBasicElement)
                multiplier = 0.8f;
            else if (damageElement == MobCounterElement)
                multiplier = 1.2f;
            
            MobModel.CurrentMobHealth -= damage * multiplier;
        }

        public override void EnableDisorientation()
        {
            throw new System.NotImplementedException();
        }

        public override void ExecuteOnMobSpawn(Transform gates, MobPortal mobPortal)
        {
            MobModel.InstantiateMobModel();

            DefaultDestinationPoint = gates;
            MobModel.MobNavMeshAgent.SetDestination(DefaultDestinationPoint.position);
        }

        private void FixedUpdate()
        {
            if (CurrentEffects.Count > 0)
                TickTimer -= Time.fixedDeltaTime;
        }
    }
}