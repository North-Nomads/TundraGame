using Mobs.MobsBehaviour;
namespace Mobs.MobEffects
{
    public class SlownessEffect : Effect
    {
        public float SpeedModifier  { get; }
        public override int MaxTicksAmount { get; }
        public override VisualEffectCode Code => VisualEffectCode.Slowness;

        public SlownessEffect(float modifier, int maxTicksAmount)
        {
            SpeedModifier = modifier;
            MaxTicksAmount = maxTicksAmount;
        }

        public override bool OnAttach(MobBehaviour mob)
        {
            mob.MobModel.CurrentMobSpeed *= SpeedModifier;
            return true;
        }

        public override void OnDetach(MobBehaviour mob)
        {
            mob.MobModel.CurrentMobSpeed /= SpeedModifier;
        }
    }
}