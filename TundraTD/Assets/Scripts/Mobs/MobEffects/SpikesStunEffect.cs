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
            
            mob.MobModel.MobNavMeshAgent.velocity = Vector3.zero;
            mob.MobModel.MobNavMeshAgent.isStopped = true;
            return true;
        }
        
        public override void OnDetach(MobBehaviour mob)
        {
            mob.MobModel.MobNavMeshAgent.isStopped = false;
        }
    }
}