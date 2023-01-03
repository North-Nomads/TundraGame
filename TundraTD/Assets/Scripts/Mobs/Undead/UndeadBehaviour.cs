using UnityEngine;

namespace Mobs.Undead
{
    /// <summary>
    /// 
    /// </summary>
    [RequireComponent(typeof(MobModel))]
    public class UndeadBehaviour : MobBehaviour
    {
        [SerializeField] private Transform gates;

        private MobModel _mobModel;

        public override void HandleAppliedEffects()
        {
            throw new System.NotImplementedException();
        }

        public override void ApplyReceivedEffects()
        {
            throw new System.NotImplementedException();
        }

        public override void MoveTowards(Vector3 point)
        {
            _mobModel.MobNavMeshAgent.SetDestination(point);
        }

        public override void HandleIncomeDamage(float damage)
        {
            _mobModel.CurrentMobHealth -= damage;
        }

        public override void KillThisMob()
        {
            Destroy(gameObject);
        }

        private void Start()
        {
            _mobModel = GetComponent<MobModel>();
        }

        private void FixedUpdate()
        {
            MoveTowards(gates.position);
        }
    }
}