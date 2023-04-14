﻿using Mobs.MobsBehaviour;
using Spells;

namespace Mobs.MobEffects
{
    /// <summary>
    /// Hit mob with constant damage over time
    /// </summary>
    public class BurningEffect : Effect
    {
        public bool CanBeExtinguished { get; set; }
        public override int MaxTicksAmount { get; }
        private float BurningDamage { get; }

        public override VisualEffectCode Code => VisualEffectCode.MeteoriteBurning;

        public BurningEffect(float burningDamage, int maxTicksAmount, bool canBeExtinguished = true)
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