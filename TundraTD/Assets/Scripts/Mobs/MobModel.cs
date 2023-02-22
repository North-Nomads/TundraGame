using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Mobs
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MobModel : MonoBehaviour
    {
        [SerializeField]
        private Material hitMaterial; 

        [SerializeField]
        private Sprite mobSprite;

        [SerializeField]
        private float maxMobHealth;

        [SerializeField]
        private float defaultMobDamage;

        [SerializeField]
        private float defaultMobSpeed;

        [SerializeField]
        private SkinnedMeshRenderer renderer;

        
        private float _defaultMobAngularSpeed;
        private float _currentMobHealth;
        private float _currentMobSpeed;
        private NavMeshAgent _mobNavMeshAgent;
        private Material _defaultMaterial;
        
        public float DefaultMobAngularSpeed => _defaultMobAngularSpeed;        
        public Sprite MobSprite => mobSprite;
        public NavMeshAgent MobNavMeshAgent => _mobNavMeshAgent;
        public bool IsAlive => CurrentMobHealth > 0;

        public float CurrentMobHealth
        {
            get => _currentMobHealth;
            set
            {
                if (value <= 0)
                    _currentMobHealth = 0;
                else
                    _currentMobHealth = value;
            }
        }

        public float CurrentMobDamage { get; set; }

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
            _defaultMobAngularSpeed = _mobNavMeshAgent.angularSpeed;
            _currentMobHealth = maxMobHealth;
            _currentMobSpeed = defaultMobSpeed;
            CurrentMobDamage = defaultMobDamage;
            _defaultMaterial = renderer.material;
        }
        
        public void SetHitMaterial()
        {
            renderer.material = hitMaterial;
            StartCoroutine(VisualEffectDamage());
        }

        private IEnumerator VisualEffectDamage()
        {
            yield return new WaitForSeconds(.1f);
            renderer.material = _defaultMaterial;
        } 
    }
}