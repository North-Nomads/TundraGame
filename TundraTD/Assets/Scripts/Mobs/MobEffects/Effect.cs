using Mobs.MobsBehaviour;
using System;

namespace Mobs.MobEffects
{
    /// <summary>
    /// Represents the model of mob effects
    /// </summary>
    public abstract class Effect
    {
        public const float BasicTickTime = .1f;

        public int CurrentTicksAmount { get; protected set; }

        public abstract int MaxTicksAmount { get; protected set;  }

        public abstract VisualEffectCode Code { get; }

        public virtual void HandleTick(MobBehaviour mob) => CurrentTicksAmount++;

        public virtual bool OnAttach(MobBehaviour mob)
        {
            return true;
        }

        public virtual void OnDetach(MobBehaviour mob)
        { }

        public void DoubleCurrentDuration() => MaxTicksAmount *= 2;

        protected void ClearThisEffectOnMob(MobBehaviour mob)
        {
            CurrentTicksAmount = MaxTicksAmount - 1;
            OnDetach(mob);
            mob.CurrentEffects.Remove(this);
        }

        public virtual float OnHitReceived(float damageAmount) => damageAmount;
    }

    /// <summary>
    /// An enumeration that is used to identify the effect prefab used in this effect instance.
    /// </summary>
    [Flags]
    public enum VisualEffectCode
    {
        /// <summary>
        /// No running effects/no prefabs are needed.
        /// </summary>
        None = 0,

        /// <summary>
        /// Represents the meteorite burning effect.
        /// </summary>
        Burning = 1 << 0,

        /// <summary>
        /// Represents the stun effect.
        /// </summary>
        Stun = 1 << 1,

        /// <summary>
        /// Represents the slowness effect.
        /// </summary>
        Slowness = 1 << 2,

        /// <summary>
        /// Represents the wet effect.
        /// </summary>
        Wet = 1 << 3
    }
}