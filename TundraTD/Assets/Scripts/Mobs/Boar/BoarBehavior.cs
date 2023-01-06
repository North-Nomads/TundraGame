﻿using Spells;
using UnityEngine;

namespace Mobs.Boar
{
    /// <summary>
    /// Handles boar mob movement and taking and dealing damage behaviour 
    /// </summary>
    [RequireComponent(typeof(MobModel))]
    public class BoarBehavior : MobBehaviour
    {
        [SerializeField] private Transform gates;
        private const BasicElement MobElement = BasicElement.Earth;
        private const BasicElement MobCounterElement = BasicElement.Air;
        
        private MobModel _mobModel;
        private bool _canDistractFromCurrentTarget;
        private float _chargeLeftTime;

        public override BasicElement MobBasicElement => BasicElement.Earth;
        
        public override void HandleIncomeDamage(float damage, BasicElement damageElement)
        {
            float multiplier;
            switch (damageElement)
            {
                case MobElement:
                    multiplier = 0.8f;
                    break;
                case MobCounterElement:
                    multiplier = 1.2f;
                    break;
                default:
                    multiplier = 1;
                    break;
            }

            _mobModel.CurrentMobHealth -= damage * multiplier;
            print($"{name}: {_mobModel.CurrentMobHealth}");
            
            if (_mobModel.CurrentMobHealth <= 0)
                KillThisMob();
        }
        
        public override void MoveTowards(Vector3 point)
        {
            _mobModel.MobNavMeshAgent.SetDestination(point);
        }

        public override void KillThisMob()
        {
            Destroy(gameObject);
        }

        private void Start()
        {
            _mobModel = GetComponent<MobModel>();
            _canDistractFromCurrentTarget = true;
            _chargeLeftTime = 3f;
        }
        
        private void FixedUpdate()
        {
            if (_chargeLeftTime > 0)
                _chargeLeftTime -= Time.fixedDeltaTime;

            if (_chargeLeftTime <= 0 && _canDistractFromCurrentTarget)
                TakeChargeMode();
                
            MoveTowards(gates.position);

            if (CurrentEffects.Count > 0)
                TickTimer -= Time.fixedDeltaTime;
        }
        
        private void TakeChargeMode()
        {
            _canDistractFromCurrentTarget = false;
            _mobModel.CurrentMobSpeed *= 1.5f;
            _mobModel.CurrentMobDamage *= 2f;
        }
    }
}