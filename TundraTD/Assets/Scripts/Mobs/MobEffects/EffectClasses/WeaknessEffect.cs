using Mobs.MobsBehaviour;

namespace Mobs.MobEffects
{
	public class WeaknessEffect : TimeBoundEffect
	{
        public override VisualEffectCode Code => VisualEffectCode.Weakness;

        public float DamageCoefficient { get; }

        public WeaknessEffect(int maxTicksAmount, float damageCoefficient) : base(maxTicksAmount)
        {
            DamageCoefficient = damageCoefficient;
        }

        public override bool OnAttach(MobBehaviour mob)
        {
            mob.MobModel.CurrentMobDamage *= DamageCoefficient;
            return true;
        }

        public override void OnDetach(MobBehaviour mob)
        {
            mob.MobModel.CurrentMobDamage /= DamageCoefficient;
        }
    }
}