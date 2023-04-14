using Mobs.MobEffects;
using Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mobs.MobsBehaviour
{
    /// <summary>
    /// An abstract class that represents the basic behavior of every mob on the map
    /// </summary>
    [RequireComponent(typeof(MobModel))]
    public abstract class MobBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject[] effectPrefabs;
        [SerializeField] private MobModel mobModel;
        private float _tickTimer;
        private WayPoint[] _waypointRoute = new WayPoint[100];
        private int _currentWaypointIndex;
        
        protected MobPortal MobPortal { get; set; }
        public List<Effect> CurrentEffects { get; } = new List<Effect>();
        public MobModel MobModel => mobModel;

        private float TickTimer
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

        public abstract void ExecuteOnMobSpawn(MobPortal mobPortal);

        protected virtual void HandleIncomeDamage(float damage, BasicElement damageElement)
        {
            MobModel.CurrentMobHealth -= damage;
        }

        protected void HandleTickTimer()
        {
            if (CurrentEffects.Count > 0)
                TickTimer -= Time.fixedDeltaTime;
        }

        public void HitThisMob(float damage, BasicElement damageElement)
        {
            if (!MobModel.IsAlive) return;

            damage = CurrentEffects.Aggregate(damage, (dmg, effect) => effect.OnHitReceived(dmg));
            HandleIncomeDamage(damage, damageElement);
            StartCoroutine(MobModel.ShowHitVFX());
            if (!MobModel.IsAlive)
                KillThisMob();
        }

        public void AddReceivedEffects(IEnumerable<Effect> effectsToApply)
        {
            foreach (var effect in effectsToApply)
            {
                if (effect.OnAttach(this))
                {
                    CurrentEffects.Add(effect);
                    SetVFXPrefab(effect, true);
                }
            }
        }

        public void AddSingleEffect(Effect effect)
        {
            // Calling OnAttach anyways
            if (!effect.OnAttach(this)) return;
            
            // After adding effect to the list we can see the ticks 
            CurrentEffects.Add(effect);
            SetVFXPrefab(effect, true);
        }

        private void ClearMobEffects()
        {
            foreach (var effect in CurrentEffects)
            {
                effect.OnDetach(this);
                SetVFXPrefab(effect, false);
            }

            CurrentEffects.Clear();
        }

        private void SetVFXPrefab(Effect effect, bool value)
        {
            int effectIndex = (int)Mathf.Log((int)effect.Code, 2);
            if (effectIndex < effectPrefabs.Length && effectPrefabs[effectIndex] != null)
                effectPrefabs[effectIndex].SetActive(value);
        }
        
        public void RemoveFilteredEffects(Func<Effect, bool> filter)
        {
            for (int i = 0; i < CurrentEffects.Count; i++)
            {
                var effect = CurrentEffects[i];
                if (filter(effect))
                {
                    effect.OnDetach(this);
                    SetVFXPrefab(effect, false);
                    CurrentEffects.RemoveAt(i--);
                }
            }
        }

        private void KillThisMob()
        {
            ClearMobEffects();
            gameObject.SetActive(false);
            transform.parent = null;
        }

        protected virtual void Start()
        {
            mobModel = GetComponent<MobModel>();
            foreach (var prefab in effectPrefabs)
            {
                if (prefab != null)
                    prefab.SetActive(false);
            }
        }

        private void HandleAppliedEffects()
        {
            for (int i = 0; i < CurrentEffects.Count;)
            {
                var effect = CurrentEffects[i];
                effect.HandleTick(this);

                if (!mobModel.IsAlive)
                    return;

                if (effect.CurrentTicksAmount == effect.MaxTicksAmount)
                {
                    effect.OnDetach(this);
                    SetVFXPrefab(effect, false);
                    CurrentEffects.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }
        
        public void RespawnMobFromPool(Vector3 position, WayPoint[] routeToSet)
        {
            // Set mob position
            var mobTransform = transform;
            mobTransform.position = position;
            
            ClearMobEffects();
            
            // Set visual effects
            gameObject.SetActive(true);
            MobModel.SetDefaultMaterial();
            
            // Set hp, speed & etc 
            mobModel.SetDefaultValues();

            _currentWaypointIndex = 0;
            _waypointRoute = routeToSet;
        }

        public void HandleWaypointApproaching()
        {
            _currentWaypointIndex++;
        }

        protected virtual void MoveTowardsNextPoint()
        {
            var waypoint = new Vector3(_waypointRoute[_currentWaypointIndex].transform.position.x,
                transform.position.y,
                _waypointRoute[_currentWaypointIndex].transform.position.z);
            var direction = waypoint - transform.position;
            mobModel.Rigidbody.velocity = direction  / direction.magnitude * mobModel.CurrentMobSpeed;
        }
    }
}