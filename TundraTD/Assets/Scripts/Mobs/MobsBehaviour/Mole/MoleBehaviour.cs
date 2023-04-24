using System;
using Spells;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mobs.MobsBehaviour.Mole
{
    [RequireComponent(typeof(MobModel))]
    public class MoleBehaviour : MobBehaviour
    {
        private bool _isUnderground = true;
        private MeshRenderer _meshRenderer;

        protected override void HandleIncomeDamage(float damage, BasicElement damageElement)
        {
            if (damageElement == BasicElement.Fire) // если элемент заклинания земля, то выкидываем крота из земли
            {
                _isUnderground = false;
                _meshRenderer.enabled = true;
            }

            if (_isUnderground == false) // пока под землей урона нет
            {
                MobModel.CurrentMobHealth -= damage;
            }
        }

        public override void ExecuteOnMobSpawn(MobPortal mobPortal)
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshRenderer.enabled = false;
            MobPortal = mobPortal;
            MobModel.InstantiateMobModel();
        }

        private void FixedUpdate()
        {
            MoveTowardsNextPoint();
            HandleTickTimer();
        }
    }
}