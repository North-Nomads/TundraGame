using Mobs.MobsBehaviour;
using System;
using UnityEngine.Rendering;

namespace Mobs.MobEffects
{
    /// <summary>
    /// Represents the model of mob effects
    /// </summary>
    public abstract class Effect
    {
        public const float BasicTickTime = .1f;

        public int CurrentTicksAmount { get; protected set; }

        public abstract int MaxTicksAmount { protected set; get; }

        public abstract VisualEffectCode Code { get; }

        public virtual void HandleTick(MobBehaviour mob) => CurrentTicksAmount++;

        public virtual bool OnAttach(MobBehaviour mob)
        {
            return true;
        }

        public virtual void OnDetach(MobBehaviour mob)
        { }

        public void DoubleCurrentDuration() => MaxTicksAmount *= 2;
        
        public void SetCurrentTicks(int value) => CurrentTicksAmount = value;

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
        MeteoriteBurning = 1 << 0,

        /// <summary>
        /// Represents the stun effect.
        /// </summary>
        Stun = 1 << 1,

        /// <summary>
        /// Represents the slowness effect.
        /// </summary>
        Slowness = 1 << 2,

        /// <summary>
        /// Represents the vulnerability effect.
        /// </summary>
        Vulnerability = 1 << 3,

        /// <summary>
        /// Represents the slowness effect of any spell
        /// </summary>
        Fear = 1 << 4,

        /// <summary>
        /// Represents the weakness effect.
        /// </summary>
        Weakness = 1 << 5,

        /// <summary>
        /// Represents the freeze effect.
        /// </summary>
        Freeze = 1 << 6,

        /// <summary>
        /// Represents the distract effect.
        /// </summary>
        Distract = 1 << 7,

        /// <summary>
        /// Represents the wet effect.
        /// </summary>
        Wet = 1 << 8,
    }
}