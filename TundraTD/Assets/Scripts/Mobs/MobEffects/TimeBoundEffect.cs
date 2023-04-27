using Mobs.MobsBehaviour;

namespace Mobs.MobEffects
{
	public abstract class TimeBoundEffect : Effect
	{
        public const float BasicTickTime = .1f;

        public int CurrentTicks { get; protected set; }

        public abstract int MaxTicks { get; protected set; }

        public override void HandleTick(MobBehaviour mob) => CurrentTicks++;

        public void DoubleCurrentDuration() => MaxTicksAmount *= 2;
    }
}