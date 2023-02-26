using Mobs.MobsBehaviour;
using Spells;

namespace Mobs.MobEffects
{
    public class MeteoriteBurningEffect : Effect
    {
        private float BurningDamage { get; }

        public override int MaxTicksAmount { get; }

        public override VisualEffectCode Code => VisualEffectCode.MeteoriteBurning;

        public MeteoriteBurningEffect(float burningDamage, int maxTicksAmount)
        {
            BurningDamage = burningDamage;
            MaxTicksAmount = maxTicksAmount;
        }

        public override void HandleTick(MobBehaviour mob)
        {
            mob.HitThisMob(BurningDamage, BasicElement.Fire);
            CurrentTicksAmount++;
        }
    }
}