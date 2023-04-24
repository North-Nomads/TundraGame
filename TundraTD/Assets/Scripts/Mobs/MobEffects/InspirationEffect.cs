using System;
using System.Linq;
using Mobs.MobsBehaviour;

namespace Mobs.MobEffects
{
    /// <summary>
    /// Doubles the duration left on each effect  
    /// </summary>
    public class InspirationEffect : Effect
    {
        public override int MaxTicksAmount  { get => 1; protected set => throw new ArithmeticException("Can't set value for inspiration"); }

        public override VisualEffectCode Code => VisualEffectCode.Stun; // Set visual effect for inspiration later

        public override bool OnAttach(MobBehaviour mob)
        {
            foreach (var effect in mob.CurrentEffects.Where(effect => !(effect is InspirationEffect)))
                effect.DoubleCurrentDuration();

            return true;
        }
    }
}