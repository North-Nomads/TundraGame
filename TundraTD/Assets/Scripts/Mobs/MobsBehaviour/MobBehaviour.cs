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
        public List<Effect> CurrentEffects { get; } = new List<Effect>();

        public Transform DefaultDestinationPoint { get; set; }

        public Vector3 CurrentDestinationPoint
        {
            get
            {
                if (MobModel.MobNavMeshAgent.enabled)
                    return MobModel.MobNavMeshAgent.destination;
                return default;
            }
            set
            {
                if (MobModel.MobNavMeshAgent.enabled)
                    MobModel.MobNavMeshAgent.SetDestination(value);
            }
        }

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

        public event EventHandler OnMobDied = delegate { };

        public abstract BasicElement MobBasicElement { get; }
        public abstract BasicElement MobCounterElement { get; }

        public abstract void ExecuteOnMobSpawn(Transform gates, MobPortal mobPortal);

        public abstract void MoveTowards(Vector3 point);

        protected abstract void HandleIncomeDamage(float damage, BasicElement damageElement);

        public void HitThisMob(float damage, BasicElement damageElement, string sourceName)
        {
            Debug.Log($"Handling {damage} damage from {sourceName} hitting {name}");
            damage = CurrentEffects.Aggregate(damage, (dmg, effect) => effect.OnHitReceived(this, dmg, damageElement));
            HandleIncomeDamage(damage, damageElement);
            MobModel.SetHitMaterial();
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
                    int effectIndex = (int)Mathf.Log((int)effect.Code, 2);
                    effectPrefabs[effectIndex].SetActive(true);
                }
            }
        }

        private void ClearMobEffects()
        {
            foreach (var effect in CurrentEffects)
            {
                effect.OnDetach(this);
                int effectIndex = (int)Mathf.Log((int)effect.Code, 2);
                effectPrefabs[effectIndex].SetActive(false);
            }
                
            CurrentEffects.Clear();
        }

        public void AddSingleEffect(Effect effect)
        {
            CurrentEffects.Add(effect);
            effect.OnAttach(this);
        }

        public void RemoveFilteredEffects(Func<Effect, bool> filter)
        {
            for (int i = 0; i < CurrentEffects.Count; i++)
            {
                var effect = CurrentEffects[i];
                if (filter(effect))
                {
                    effect.OnDetach(this);
                    CurrentEffects.RemoveAt(i--);
                }
            }
        }

        private void KillThisMob()
        {
            ClearMobEffects();
            Destroy(gameObject);
            OnMobDied(this, null);
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

                if (effect.CurrentTicksAmount == effect.MaxTicksAmount)
                {
                    CurrentEffects.RemoveAt(i);
                    effect.OnDetach(this);
                    int effectIndex = (int)Mathf.Log((int)effect.Code, 2);
                    effectPrefabs[effectIndex].SetActive(false);
                }
                else
                {
                    i++;
                }
            }
        }
    }
}