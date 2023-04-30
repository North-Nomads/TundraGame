using Mobs.MobsBehaviour;
namespace Mobs.MobEffects
{
    /// <summary>
    /// Makes mob speed slower
    /// </summary>
    public class SlownessEffect : TimeBoundEffect
    {
        public float SpeedModifier  { get; }
        public override VisualEffectCode Code => VisualEffectCode.Slowness;

        public SlownessEffect(float modifier, int maxTicksAmount) : base(maxTicksAmount)
        {
            SpeedModifier = modifier;
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