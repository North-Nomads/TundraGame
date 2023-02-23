using System.Linq;
using Mobs.MobsBehaviour;

namespace Mobs.MobEffects
{
    public class DisorientationEffect : Effect
    {
        public override int MaxTicksAmount { get; }
        public override EffectCode Code => EffectCode.Disorientation;

        public DisorientationEffect(int ticks)
        {
            MaxTicksAmount = ticks;
        }

        public override bool OnAttach(MobBehaviour mob)
        {
            foreach (var mobCurrentEffect in mob.CurrentEffects.OfType<DisorientationEffect>())
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