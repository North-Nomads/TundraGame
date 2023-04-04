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
        private MeshRenderer meshRenderer;
        public override BasicElement MobBasicElement => BasicElement.Earth;
        public override BasicElement MobCounterElement => BasicElement.Air;

        protected override void HandleIncomeDamage(float damage, BasicElement damageElement)
        {
            if (damageElement == BasicElement.Fire) // если элемент заклинания земля, то выкидываем крота из земли
            {
                _isUnderground = false;
                meshRenderer.enabled = true;
            }

            if (_isUnderground == false) // пока под землей урона нет
            {
                var multiplier = 1f;
                if (damageElement == MobBasicElement)
                    multiplier = 0.8f;
                else if (damageElement == MobCounterElement)
                    multiplier = 1.2f;

                MobModel.CurrentMobHealth -= damage * multiplier;
            }
        }

        public override void ExecuteOnMobSpawn(Transform gates, MobPortal mobPortal)
        {
            MobPortal = mobPortal;
            MobModel.InstantiateMobModel();

            DefaultDestinationPoint = gates;
            MobModel.MobNavMeshAgent.enabled = true;
            MobModel.MobNavMeshAgent.SetDestination(DefaultDestinationPoint.position);
        }

        private void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.enabled = false;

        }
    }
}