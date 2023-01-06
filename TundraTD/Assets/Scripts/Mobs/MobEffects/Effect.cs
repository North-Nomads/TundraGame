using Mobs.MobsBehaviour;

namespace Mobs.MobEffects
{
    /// <summary>
    /// Represents the model of effects in unity  
    /// </summary>
    public abstract class Effect
    {
        public const float BasicTickTime = 1f;

        public int TicksAmountLeft { get; protected set; } 
        
        public abstract int MaxTicksAmount { get; }
        public abstract void HandleTick(MobBehaviour mob);
    }
}