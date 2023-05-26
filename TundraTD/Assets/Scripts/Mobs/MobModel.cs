using System.Collections;
using UnityEngine;

namespace Mobs
{
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
        private new Renderer renderer;

        [SerializeField] 
        private Material defaultMaterial;
        
        [SerializeField] 
        private new Rigidbody rigidbody;

        [SerializeField]
        private float defaultMobAngularSpeed;

        private Animator _animator;
        private float _currentMobHealth;
        private float _currentMobSpeed;

        public Renderer Renderer => renderer;
        public Rigidbody Rigidbody => rigidbody;
        public Animator Animator => _animator;
        public float DefaultMobAngularSpeed => defaultMobAngularSpeed;        
        public Sprite MobSprite => mobSprite;
        public bool IsAlive => CurrentMobHealth > 0;

        public float DefaultMobSpeed => defaultMobSpeed;
        
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
            set => _currentMobSpeed = value;
        }

        public void InstantiateMobModel()
        {
            SetDefaultValues();
        }

        public void SetDefaultValues()
        {
            if (_animator is null)
                _animator = GetComponent<Animator>();
            
            rigidbody.velocity = Vector3.zero;
            
            _currentMobSpeed = DefaultMobSpeed;
            _currentMobHealth = maxMobHealth;
            CurrentMobDamage = defaultMobDamage;
        }
        
        public IEnumerator ShowHitVFX()
        {
            renderer.material = hitMaterial;
            yield return new WaitForSeconds(.1f);
            SetDefaultMaterial();
        }

        public void SetDefaultMaterial()
        {
            renderer.material = defaultMaterial;
        }
    }
}