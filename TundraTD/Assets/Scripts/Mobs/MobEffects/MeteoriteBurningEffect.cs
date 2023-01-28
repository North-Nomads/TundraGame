using Mobs.MobsBehaviour;
using Spells;

namespace Mobs.MobEffects
{
    public class MeteoriteBurningEffect : Effect
    {
        private float BurningDamage { get; }
        
        public override int MaxTicksAmount { get; }

        public override EffectCode Code => EffectCode.MeteoriteBurning;

        public MeteoriteBurningEffect(float burningDamage, int maxTicksAmount)
        {
            BurningDamage = burningDamage;
            MaxTicksAmount = maxTicksAmount;
        }

        public override void HandleTick(MobBehaviour mob)
        {
            mob.HandleIncomeDamage(BurningDamage, BasicElement.Fire);
            CurrentTicksAmount++;
        }
    }
}