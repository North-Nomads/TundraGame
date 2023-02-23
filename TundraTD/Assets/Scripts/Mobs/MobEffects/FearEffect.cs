using System.Linq;
using Mobs.MobsBehaviour;

namespace Mobs.MobEffects
{
    public class FearEffect : Effect
    {
        public override int MaxTicksAmount { get; }
        public override EffectCode Code => EffectCode.Disorientation;

        public FearEffect(int ticks)
        {
            MaxTicksAmount = ticks;
        }

        public override bool OnAttach(MobBehaviour mob)
        {
            foreach (var mobCurrentEffect in mob.CurrentEffects.OfType<FearEffect>())
            {
                mobCurrentEffect.ClearThisEffectOnMob(mob);
            }

            mob.EnableDisorientation();
            return true;
        }

        public override void OnDetach(MobBehaviour mob)
        {
            mob.MobModel.MobNavMeshAgent.SetDestination(mob.DefaultDestinationPoint.position);
        }
    }
}