using Mobs.MobsBehaviour;
namespace Mobs.MobEffects
{
    public class SlownessEffect : Effect
    {
        public float SpeedModifier  { get; }
        public override int MaxTicksAmount { get; }
        public override EffectCode Code => EffectCode.Slowness;

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