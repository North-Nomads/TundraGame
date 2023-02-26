using Mobs.MobsBehaviour;
using Spells;

namespace Mobs.MobEffects
{
	public class WeaknessEffect : Effect
	{
        public override int MaxTicksAmount { get; }

        public override EffectCode Code => EffectCode.Weakness;

        public BasicElement TargetElements { get; }

        public float DamageCoefficient { get; }

        public WeaknessEffect(int maxTicksAmount, BasicElement targetElements, float damageCoefficient)
        {
            MaxTicksAmount = maxTicksAmount;
            TargetElements = targetElements;
            DamageCoefficient = damageCoefficient;
        }

        public override float OnHitReceived(MobBehaviour mob, float damageAmount, BasicElement element)
        {
            if ((TargetElements & element) == element)
            {
                return damageAmount * DamageCoefficient;
            }
            return damageAmount;
        }
    }
}