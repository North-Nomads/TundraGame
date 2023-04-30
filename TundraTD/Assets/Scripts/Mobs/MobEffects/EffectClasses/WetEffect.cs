namespace Mobs.MobEffects
{
    /// <summary>
    /// Mobs with this effect has twice more damage from lightning source
    /// </summary>
    public class WetEffect : TimeBoundEffect
    {
        public override VisualEffectCode Code => VisualEffectCode.Wet;

        public WetEffect(int time) : base(time)
        {
        }
    }
}