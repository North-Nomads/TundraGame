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

        public abstract int MaxTicksAmount { get; }

        public abstract EffectCode Code { get; }

        public abstract void HandleTick(MobBehaviour mob);

        public virtual void OnAttach(MobBehaviour mob)
        { }

        public virtual void OnDetach(MobBehaviour mob)
        { }
    }

    /// <summary>
    /// An enumeration that is used to identify the effect prefab used in this effect instance.
    /// </summary>
    [Flags]
    public enum EffectCode
    {
        /// <summary>
        /// No running effects/no prefabs are needed.
        /// </summary>
        None = 0,

        /// <summary>
        /// Represents the meteorite burning effect.
        /// </summary>
        MeteoriteBurning = 1 << 0,

        /// <summary>
        /// Represents the stun effect.
        /// </summary>
        Stun = 1 << 1,
    }
}