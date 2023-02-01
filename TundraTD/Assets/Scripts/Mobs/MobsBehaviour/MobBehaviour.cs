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
        [SerializeField] private GameObject[] effectPrefabs;
        private float _tickTimer;
        private MobPortal _mobPortal;
        private Transform _defaultDestinationPoint;
        private Transform _currentDestinationPoint;

        protected List<Effect> CurrentEffects { get; } = new List<Effect>();
        protected Transform DefaultDestinationPoint { get; set; }
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
        public MobPortal MobPortal 
        {
            get => _mobPortal;
            set => _mobPortal = value;
        }
        public MobModel MobModel { get; protected set; }
        public Transform CurrentDestinationPoint
        {
            get => _currentDestinationPoint;
            set => _currentDestinationPoint = value;
        }
        public abstract BasicElement MobBasicElement { get; }
        public abstract BasicElement MobCounterElement { get; }
        
        private void Start()
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
        public void AddReceivedEffects(IEnumerable<Effect> effectsToApply)
        {
            CurrentEffects.AddRange(effectsToApply);
        }
        public void KillThisMob()
        {
            Destroy(gameObject);
            _mobPortal.DecreaseMobsCountByOne();
            _mobPortal.NotifyPortalOnMobDeath(this);
        }
        
        public abstract void ExecuteOnMobSpawn(Transform gates, MobPortal mobPortal);
        public abstract void MoveTowards(Vector3 point);
        public abstract void HandleIncomeDamage(float damage, BasicElement damageElement);
    }
}
