using Mobs.MobsBehaviour;

namespace Mobs.MobEffects
{
	public class WeaknessEffect : Effect
	{
        public override int MaxTicksAmount { get; protected set; }

        public override VisualEffectCode Code => VisualEffectCode.Weakness;

        public float DamageCoefficient { get; }

        public WeaknessEffect(int maxTicksAmount, float damageCoefficient)
        {
            MaxTicksAmount = maxTicksAmount;
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