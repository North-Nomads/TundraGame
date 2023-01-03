namespace Mobs.MobEffects
{
    public class MeteoriteBurningEffect : Effect
    {
        private const float BurningDamage = 3f;
        
        public override int MaxTicksAmount => 3;

        public override void HandleTick(MobBehaviour mob)
        {
            mob.HandleIncomeDamage(BurningDamage);
            TicksAmountLeft++;
        }
    }
}