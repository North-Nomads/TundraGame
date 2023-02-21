using Mobs.MobsBehaviour;
using UnityEngine;

namespace Mobs.MobEffects
{
    public class StunEffect : Effect
    {
        public override int MaxTicksAmount { get; }

        public override EffectCode Code => EffectCode.Stun;

        public StunEffect(int time)
        {
            MaxTicksAmount = time;
        }
        
        public override void OnAttach(MobBehaviour mob)
        {
            mob.MobModel.MobNavMeshAgent.isStopped = true;
        }
        
        public override void OnDetach(MobBehaviour mob)
        {
            mob.MobModel.MobNavMeshAgent.isStopped = false;
        }
    }
}