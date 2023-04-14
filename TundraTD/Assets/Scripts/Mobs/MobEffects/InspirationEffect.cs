using Mobs.MobsBehaviour;

namespace Mobs.MobEffects
{
    /// <summary>
    /// Doubles the duration left on each effect  
    /// </summary>
    public class InspirationEffect : Effect
    {
        public override int MaxTicksAmount => 1;
        public override VisualEffectCode Code => VisualEffectCode.Stun; // Set visual effect for inspiration later

        public override bool OnAttach(MobBehaviour mob)
        {
            foreach (var effect in mob.CurrentEffects)
                effect.DoubleCurrentDuration();
            
            return true;
        }
    }
}