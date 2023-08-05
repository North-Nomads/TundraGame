using Mobs.MobEffects;
using Spells;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

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

        public bool IsMoving { get; set; }
        private Vector3? TargetToFocus { get; set; }
        
        private int _currentWaypointIndex;
        private float _tickTimer;
        
        protected List<Transform> MobPath { get; private set; }

        protected int CurrentWaypointIndex => _currentWaypointIndex;
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

        public void ClearMobEffects()
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
            MobModel.SetDefaultMaterial();
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
        
        public void RespawnMobFromPool(Vector3 position, List<Transform> mobPath)
        {
            // Set mob position
            var mobTransform = transform;
            mobTransform.position = position;

            IsMoving = true;
            ClearMobEffects();
            
            // Set visual effects
            gameObject.SetActive(true);
            
            // Set hp, speed & etc 
            mobModel.SetDefaultValues();
            MobModel.SetDefaultMaterial();

            _currentWaypointIndex = 0;
            MobPath = mobPath;
        }

        private void HandleWaypointApproachingOrPassing()
        {
            _currentWaypointIndex++;
        }

        /// <summary>
        /// Sets the current index according to the mob and gates position
        /// </summary>
        private void UpdateCurrentWaypoint()
        {
            var finishPoint = MobPath.Last().transform.position; // gates

            var finishDirection = transform.position - finishPoint;
            var currentPoint = MobPath[_currentWaypointIndex].transform.position;
            
            // Current point direction projection on finish direction 
            var currentWaypointProjection = Vector3.Project(transform.position - currentPoint, finishDirection);
            
            // If the Dot is < 0 -> point is behind our mob and we don't have to return back 
            bool hasMobPassedPointBy = Vector3.Dot(currentWaypointProjection, finishDirection) <= 0;
            // Check whether we are close enough to the point
            var distance = Vector3.Distance(transform.position, currentPoint);
            bool isMobCloseEnough = distance <= .5f; 
            if (isMobCloseEnough || hasMobPassedPointBy) 
                HandleWaypointApproachingOrPassing();
        }

        protected void MoveTowardsNextPoint(Vector3 waypoint = default)
        {
            if (!IsMoving)
                return;
            
            if (TargetToFocus.HasValue)
                waypoint = TargetToFocus.Value;
            
            UpdateCurrentWaypoint();
            var direction = waypoint - transform.position;
            if (waypoint == Vector3.zero)
            {
                waypoint = new Vector3(MobPath[_currentWaypointIndex].transform.position.x, transform.position.y,
                    MobPath[_currentWaypointIndex].transform.position.z);
                direction = waypoint - transform.position;
                transform.rotation = Quaternion.LookRotation(direction);
            }
            mobModel.Rigidbody.velocity = direction.normalized * mobModel.CurrentMobSpeed;
        }


        private void OnDrawGizmos()
        {
            #if (!UNITY_EDITOR)
                Gizmos.DrawSphere(MobPath[_currentWaypointIndex].transform.position, 1f);
            #endif
        }

        public void SetFocusingTarget(Vector3? destination)
        {
            TargetToFocus = destination;
        }
    }
}