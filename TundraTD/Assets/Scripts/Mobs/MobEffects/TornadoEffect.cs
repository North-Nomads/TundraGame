namespace Mobs.MobEffects
{
    public class TornadoEffect : Effect
    {
        public override int MaxTicksAmount { get; protected set; }

        public override VisualEffectCode Code => VisualEffectCode.Wet;

        public TornadoEffect(int time)
        {
            MaxTicksAmount = time;
        }
    }
}