using Mobs.MobsBehaviour;
using Spells;

namespace Mobs.MobEffects
{
    /// <summary>
    /// Hit mob with constant damage over time
    /// </summary>
    public class BurningEffect : TimeBoundEffect
    {
        private float BurningDamage { get; }

        public bool CanBeExtinguished { get; set; }

        public override VisualEffectCode Code => VisualEffectCode.MeteoriteBurning;

        public BurningEffect(float burningDamage, int maxTicksAmount, bool canBeExtinguished = true) : base(maxTicksAmount)
        {
            BurningDamage = burningDamage;
            CanBeExtinguished = canBeExtinguished;
        }

        public override void HandleTick(MobBehaviour mob)
        {
            base.HandleTick(mob);
            mob.HitThisMob(BurningDamage, BasicElement.Fire);
        }
    }
}