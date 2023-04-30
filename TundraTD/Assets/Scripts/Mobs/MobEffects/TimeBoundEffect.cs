using Mobs.MobsBehaviour;

namespace Mobs.MobEffects
{
	public abstract class TimeBoundEffect : Effect
	{
        public const float BasicTickTime = .1f;

        public int CurrentTicks { get; protected set; }

        public int MaxTicks { get; protected set; }

        public override bool ShouldBeDetached => CurrentTicks >= MaxTicks;

        protected TimeBoundEffect(int maxTicks)
        {
            MaxTicks = maxTicks;
        }

        public override void HandleTick(MobBehaviour mob) => CurrentTicks++;

        public void DoubleCurrentDuration() => MaxTicks *= 2;
    }
}