using Mobs.MobsBehaviour;

namespace Mobs.MobEffects
{
    /// <summary>
    /// Forces mob to run towards his portal
    /// </summary>
    public class FearEffect : Effect
    {
        public override int MaxTicksAmount { get; }
        public override VisualEffectCode Code => VisualEffectCode.Fear;

        public FearEffect(int ticks)
        {
            MaxTicksAmount = ticks;
        }

        public override bool OnAttach(MobBehaviour mob)
        {
            var agent = mob.MobModel.MobNavMeshAgent;
            if (!agent.isActiveAndEnabled)
                return false;
            
            mob.RemoveFilteredEffects(x => x is FearEffect);
            
            agent.SetDestination(mob.MobPortal.transform.position);
            return true;
        }

        public override void OnDetach(MobBehaviour mob)
        {
            var agent = mob.MobModel.MobNavMeshAgent;
            if (!agent.isActiveAndEnabled)
                return;

            agent.SetDestination(mob.DefaultDestinationPoint.position);
        }
    }
}