using Mobs.MobsBehaviour;
namespace Mobs.MobEffects
{
    /// <summary>
    /// Makes mob speed slower
    /// </summary>
    public class SlownessEffect : Effect
    {
        public float SpeedModifier  { get; }
        public override int MaxTicksAmount { get; protected set; }
        public override VisualEffectCode Code => VisualEffectCode.Slowness;

        public SlownessEffect(float modifier, int time)
        {
            SpeedModifier = modifier;
            MaxTicksAmount = time;
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