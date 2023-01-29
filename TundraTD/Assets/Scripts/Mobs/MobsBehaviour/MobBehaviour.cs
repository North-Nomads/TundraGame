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
        private MobPortal _mobPortal;
        private Transform _defaultDestinationPoint;
        private Transform _currentDestinationPoint;
        
        protected List<Effect> CurrentEffects { get; } = new List<Effect>();
        public MobPortal MobPortal 
        {
            get => _mobPortal;
            set => _mobPortal = value;
        }
        public Transform DefaultDestinationPoint { get; protected set; }
        public Transform CurrentDestinationPoint
        {
            get => _currentDestinationPoint;
            set => _currentDestinationPoint = value;
        }

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
        public abstract BasicElement MobCounterElement { get; }

        public abstract void ExecuteOnMobSpawn(Transform gates, MobPortal mobPortal);
        public abstract void MoveTowards(Vector3 point);
        public abstract void HandleIncomeDamage(float damage, BasicElement damageElement);

        public void AddReceivedEffects(IEnumerable<Effect> effectsToApply)
        {
            CurrentEffects.AddRange(effectsToApply);
        }
        
        public void KillThisMob()
        {
            Destroy(gameObject);
            _mobPortal.DecreaseMobsCountByOne();
        }

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
