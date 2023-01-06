using Mobs.MobEffects;
using UnityEngine;
using System.Collections.Generic;
using Spells;

namespace Mobs.MobsBehaviour
{
    /// <summary>
    /// An abstract class that represents the basic behavior of every mob on the map
    /// </summary>
    public abstract class MobBehaviour : MonoBehaviour
    {
        private float _tickTimer;
        protected List<Effect> CurrentEffects { get; } = new List<Effect>();

        protected float TickTimer
        {
            get => _tickTimer;
            set
            {
                if (value <= 0)
                {
                    HandleAppliedEffects();
                    _tickTimer = Effect.BasicTickTime;
                    return;
                }

                _tickTimer = value;
            }
        }
        public abstract BasicElement MobBasicElement { get; }

        public void AddReceivedEffects(IEnumerable<Effect> effectsToApply)
        {
            CurrentEffects.AddRange(effectsToApply);
        }
        
        public abstract void MoveTowards(Vector3 point);
        public abstract void HandleIncomeDamage(float damage, BasicElement damageElement);
        public abstract void KillThisMob();

        private void HandleAppliedEffects()
        {
            for (int i = 0; i < CurrentEffects.Count;)
            {
                CurrentEffects[i].HandleTick(this);
                
                if (CurrentEffects[i].TicksAmountLeft == CurrentEffects[i].MaxTicksAmount)
                    CurrentEffects.RemoveAt(i);
                else
                    i++;
            }
        }
    }
}
