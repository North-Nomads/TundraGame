using Mobs.MobsBehaviour;
namespace Mobs.MobEffects
{
    public class SlownessEffect : Effect
    {
        public float SpeedModificator { get; }
        public override int MaxTicksAmount { get; }
        public override EffectCode Code => EffectCode.Slowness;

        public SlownessEffect(float modificator, int time)
        {
            SpeedModificator = modificator;
            MaxTicksAmount = time;
        }

        public override void OnAttach(MobBehaviour mob)
        {
            mob.MobModel.CurrentMobSpeed *= SpeedModificator;
        }

        public override void OnDetach(MobBehaviour mob)
        {
            mob.MobModel.CurrentMobSpeed /= SpeedModificator;
        }
    }
}