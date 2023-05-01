using Mobs.MobsBehaviour;

namespace Mobs.MobEffects
{
    public class SpeedEffect : Effect
    {
        public SpeedEffect(int ticks, float multiplier)
        {
            MaxTicksAmount = ticks;
            _multiplier = multiplier;
        }

        private readonly float _multiplier;
        public override int MaxTicksAmount { get; protected set; }
        public override VisualEffectCode Code => VisualEffectCode.Stun; 

        public override bool OnAttach(MobBehaviour mob)
        {
            mob.MobModel.CurrentMobSpeed *= _multiplier;
            return true;
        }

        public override void OnDetach(MobBehaviour mob)
        {
            mob.MobModel.CurrentMobSpeed /= _multiplier;
        }
    }
}