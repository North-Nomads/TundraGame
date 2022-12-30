using UnityEngine;
using UnityEngine.AI;

namespace Mobs
{
    public class MobModel : MonoBehaviour
    {
        [SerializeField] private float maxMobHealth;
        [SerializeField] private float defaultMobDamage;
        [SerializeField] private float defaultMobSpeed;
        private float _currentMobHealth;
        private float _currentMobDamage;
        private float _currentMobSpeed;
        private NavMeshAgent _mobNavMeshAgent;

        public NavMeshAgent MobNavMeshAgent => _mobNavMeshAgent;

        public float CurrentMobHealth
        {
            get => _currentMobHealth;
            set => _currentMobHealth = value;
        }
        public float CurrentMobDamage
        {
            get => _currentMobDamage;
            set => _currentMobDamage = value;
        }
        public float CurrentMobSpeed
        {
            get => _currentMobSpeed;
            set
            {
                _currentMobSpeed = value;
                _mobNavMeshAgent.speed = _currentMobSpeed;
            }
        }
        
        private void Start()
        {
            _mobNavMeshAgent = GetComponent<NavMeshAgent>();
            _currentMobHealth = maxMobHealth;
            _currentMobSpeed = defaultMobSpeed;
            _currentMobDamage = defaultMobDamage;
        }
    }
}
