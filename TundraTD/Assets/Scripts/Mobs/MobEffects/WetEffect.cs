namespace Mobs.MobEffects
{
    /// <summary>
    /// Mobs with this effect has twice more damage from lightning source
    /// </summary>
    public class WetEffect : Effect
    {
        public override int MaxTicksAmount { get; protected set; }

        public override VisualEffectCode Code => VisualEffectCode.Wet;

        public WetEffect(int time)
        {
            MaxTicksAmount = time;
        }
    }
}