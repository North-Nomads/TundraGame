using Mobs.MobsBehaviour;

namespace Mobs.MobEffects
{
    public class StunEffect : Effect
    {
        public override int MaxTicksAmount => 5;

        public override EffectCode Code => EffectCode.Stun;

        public override void HandleTick(MobBehaviour mob)
        {
            CurrentTicksAmount++;
        }

        public override bool OnAttach(MobBehaviour mob)
        {
            mob.MobModel.CurrentMobSpeed = 0;
            return true;
        }

        public override void OnDetach(MobBehaviour mob)
        {
            mob.MobModel.CurrentMobSpeed = 1;
        }
    }
}