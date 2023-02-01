using UnityEngine;
using UnityEngine.AI;

namespace Mobs
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MobModel : MonoBehaviour
    {
        [SerializeField] private float maxMobHealth;
        [SerializeField] private float defaultMobDamage;
        [SerializeField] private float defaultMobSpeed;
        [SerializeField] private float mobWaveWeight;
        private float _currentMobHealth;
        private float _currentMobDamage;
        private float _currentMobSpeed;
        private NavMeshAgent _mobNavMeshAgent;

        public NavMeshAgent MobNavMeshAgent => _mobNavMeshAgent;
        public float MobWaveWeight => mobWaveWeight;
        
        public float CurrentMobHealth
        {
            get => _currentMobHealth;
            set
            {
                if (value < 0)
                    _currentMobHealth = 0;
                else
                    _currentMobHealth = value;   
            }
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
        
        public void InstantiateMobModel()
        {
            _mobNavMeshAgent = GetComponent<NavMeshAgent>();
            _currentMobHealth = maxMobHealth;
            _currentMobSpeed = defaultMobSpeed;
            _currentMobDamage = defaultMobDamage;
        }
    }
}
