using Mobs.MobEffects;
using UnityEngine;
using System.Collections.Generic;
using Spells;
using System.Linq;
using System;

namespace Mobs.MobsBehaviour
{
    /// <summary>
    /// An abstract class that represents the basic behavior of every mob on the map
    /// </summary>
    public abstract class MobBehaviour : MonoBehaviour
    {
        private float _tickTimer;
        private Transform _defaultDestinationPoint;
        private Transform _currentDestinationPoint;

        [SerializeField] private GameObject[] effectPrefabs;

        protected List<Effect> CurrentEffects { get; } = new List<Effect>();
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

        public void AddReceivedEffects(IEnumerable<Effect> effectsToApply)
        {
            CurrentEffects.AddRange(effectsToApply);
        }
        
        public abstract void MoveTowards(Vector3 point);
        public abstract void HandleIncomeDamage(float damage, BasicElement damageElement);
        public abstract void KillThisMob();

        protected virtual void Start()
        {
            foreach (var prefab in effectPrefabs)
            {
                if (prefab != null) prefab.SetActive(false);
            }
        }

        private void HandleAppliedEffects()
        {
            for (int i = 0; i < CurrentEffects.Count;)
            {
                CurrentEffects[i].HandleTick(this);

                if (CurrentEffects[i].CurrentTicksAmount == CurrentEffects[i].MaxTicksAmount)
                    CurrentEffects.RemoveAt(i);
                else
                    i++;
            }
            foreach (var prefab in effectPrefabs)
                if (prefab != null) prefab.SetActive(false);
            foreach (var effect in CurrentEffects)
            {
                int effectIndex = (int)Mathf.Log((int)effect.Code, 2);
                if (effectPrefabs[effectIndex] != null) effectPrefabs[effectIndex].SetActive(true);
            }
        }

        public abstract void ExecuteOnMobSpawn(Transform gates);
    }
}
