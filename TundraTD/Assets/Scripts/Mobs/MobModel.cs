using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

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
        private Renderer[] renderers;
        
        [SerializeField] 
        private new Rigidbody rigidbody;

        private Material[] _defaultMaterials;
        private Animator _animator;
        private float _currentMobHealth;
        private float _currentMobSpeed;

        public Renderer[] Renderers => renderers;
        public Rigidbody Rigidbody => rigidbody;
        public Animator Animator => _animator;
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

            _defaultMaterials = new Material[Renderers.Length];
            for (int i = 0; i < Renderers.Length; i++)
                _defaultMaterials[i] = Renderers[i].material;
        }
        
        public IEnumerator ShowHitVFX()
        {
            foreach (var each in Renderers)
                each.material = hitMaterial;
            
            yield return new WaitForSeconds(.1f);
            SetDefaultMaterial();
        }

        public void SetDefaultMaterial()
        {
            for (int i = 0; i < _defaultMaterials.Length; i++)
                Renderers[i].material = _defaultMaterials[i];
        }
    }
}