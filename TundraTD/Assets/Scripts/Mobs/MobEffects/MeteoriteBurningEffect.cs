using Mobs.MobsBehaviour;
using Spells;

namespace Mobs.MobEffects
{
    public class MeteoriteBurningEffect : Effect
    {
        private float BurningDamage { get; }

        public bool CanBeExtinguished { get; set; }

        public override int MaxTicksAmount { get; }

        public override VisualEffectCode Code => VisualEffectCode.MeteoriteBurning;

        public MeteoriteBurningEffect(float burningDamage, int maxTicksAmount, bool canBeExtinguished = true)
        {
            BurningDamage = burningDamage;
            MaxTicksAmount = maxTicksAmount;
            CanBeExtinguished = canBeExtinguished;
        }

        public override void HandleTick(MobBehaviour mob)
        {
            mob.HitThisMob(BurningDamage, BasicElement.Fire, "Fire.MeteoriteBurning");
            CurrentTicksAmount++;
        }
    }
}