using System.Linq;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Mobs.MobEffects
{
    public class SpikesStunEffect : Effect
    {
        public override int MaxTicksAmount { get; }

        public override EffectCode Code => EffectCode.Stun;

        public SpikesStunEffect(int time)
        {
            MaxTicksAmount = time;
        }
        
        public override bool OnAttach(MobBehaviour mob)
        {
            if (mob.CurrentEffects.Any(x => x is SpikesStunEffect))
                return false;
            
            mob.MobModel.MobNavMeshAgent.SetDestination(mob.transform.position);
            mob.MobModel.MobNavMeshAgent.angularSpeed = 0;
            return true;
        }
        
        public override void OnDetach(MobBehaviour mob)
        {
            mob.MobModel.MobNavMeshAgent.SetDestination(mob.DefaultDestinationPoint.position);
            mob.MobModel.MobNavMeshAgent.angularSpeed = mob.MobModel.DefaultMobAngularSpeed;
        }
    }
}