using Mobs.MobEffects;
using Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

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
        private List<WayPoint> _waypointRoute;
        private int _currentWaypointIndex;
        public List<WayPoint> WaypointRoute => _waypointRoute;

        public int CurrentWaypointIndex => _currentWaypointIndex;

        
        
        public List<Effect> CurrentEffects { get; } = new List<Effect>();
        
        public MobModel MobModel => mobModel;

        public MobPortal MobPortal { get; protected set; }

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

        public abstract void ExecuteOnMobSpawn(MobPortal mobPortal);

        protected abstract void HandleIncomeDamage(float damage, BasicElement damageElement);

        public void HitThisMob(float damage, BasicElement damageElement, string sourceName)
        {
            if (!MobModel.IsAlive) return;

            damage = CurrentEffects.Aggregate(damage, (dmg, effect) => effect.OnHitReceived(this, dmg, damageElement));
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
            CurrentEffects.Add(effect);
            effect.OnAttach(this);
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
        
        public void RespawnMobFromPool(Vector3 position, Route routeToSet)
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
            
            // Set route values
            if (_waypointRoute is null)
                _waypointRoute = new List<WayPoint>();
            
            _currentWaypointIndex = 0;
            _waypointRoute = routeToSet.WayPoints;
        }

        public void HandleWaypointApproaching()
        {
            _currentWaypointIndex++;
        }

        protected virtual void MoveTowardsNextPoint()
        {
            print(_currentWaypointIndex);
            var waypoint = new Vector3(_waypointRoute[_currentWaypointIndex].transform.position.x,
                transform.position.y,
                _waypointRoute[_currentWaypointIndex].transform.position.z);
            var direction = waypoint - transform.position;
            mobModel.Rigidbody.velocity = direction  / direction.magnitude * mobModel.CurrentMobSpeed;
        }

        private void OnDrawGizmos()
        {
            var wp = new Vector3(_waypointRoute[_currentWaypointIndex].transform.position.x,
                transform.position.y,
                _waypointRoute[_currentWaypointIndex].transform.position.z);
            Gizmos.DrawLine(wp, transform.position);
        }
    }
}