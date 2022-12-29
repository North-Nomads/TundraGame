using System;
using UnityEngine;

namespace Mobs.Boar
{
    [RequireComponent(typeof(MobModel))]
    public class BoarBehavior : MobBehaviour
    {
        [SerializeField] private Transform gates;
        private MobModel _mobModel;

        private void Start()
        {
            _mobModel = GetComponent<MobModel>();
        }

        public override void HandleAppliedEffects()
        {
            throw new System.NotImplementedException();
        }

        public override void ApplyReceivedEffects()
        { }

        public override void MoveTowards(Vector3 point)
        {
            _mobModel.MobNavMeshAgent.SetDestination(point);
        }

        public override void KillThisMob()
        {
            throw new System.NotImplementedException();
        }

        private void Update()
        {
            MoveTowards(gates.position);
        }
    }
}