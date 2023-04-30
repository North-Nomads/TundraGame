using System;
using System.Linq;
using Mobs.MobsBehaviour;

namespace Mobs.MobEffects
{
    /// <summary>
    /// Doubles the duration left on each effect  
    /// </summary>
    public class InspirationEffect : TimeBoundEffect
    {
        public InspirationEffect() : base(1)
        {
        }

        public override VisualEffectCode Code => VisualEffectCode.Stun; // Set visual effect for inspiration later

        public override bool OnAttach(MobBehaviour mob)
        {
            foreach (var effect in mob.CurrentEffects.Where(effect => !(effect is InspirationEffect)).OfType<TimeBoundEffect>())
                effect.DoubleCurrentDuration();

            return true;
        }
    }
}