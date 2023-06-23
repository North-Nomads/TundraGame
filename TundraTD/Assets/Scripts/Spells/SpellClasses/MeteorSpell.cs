using System.Collections;
using System.Security.Cryptography;
using Level;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Spells.SpellClasses
{
	public class MeteorSpell : MagicSpell
	{
        [SerializeField] private MeshRenderer meteoriteMesh;
        [SerializeField] private ParticleSystem smokePrefab;
        [SerializeField] private GameObject sparklesPrefab;
        [SerializeField] private float explosionDelay = 2;
        [SerializeField] private float shakeDuration;
        [SerializeField] private AudioClip flightSound;
        [SerializeField] private AudioClip explosionSound;

        private Camera _camera;
        private const float FlyDistance = 30;
        private const float HitDelay = 0.5f;
        private const int MobsLayerMask = 1 << 8;
        
        private readonly Collider[] _availableTargetsPool = new Collider[1000];
        private Camera _mainCamera;
        private float _currentHitTime;
        private Vector3 _target;
        private bool _isLanded;
        private AudioSource _source;
        private Coroutine _fallTask;

        /// <summary>
        /// The radius of the hit area
        /// </summary>
        private float HitDamageRadius => 5f;

        /// <summary>
        /// The damage of the hit epicenter.
        /// </summary>
        private float HitDamageValue => 40f;

        /// <summary>
        /// Duration of the burn effect.
        /// </summary>
        private float BurnDuration => 3f;

        /// <summary>
        /// Damage of the burn effect.
        /// </summary>
        private float BurnDamage => 7f;

        public override BasicElement Element => BasicElement.Fire | BasicElement.Earth;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            if (_mainCamera is null)
                _mainCamera = Camera.main;

            _target = hitInfo.point;
            _source = GetComponent<AudioSource>();
            _source.volume = GameParameters.EffectsVolumeModifier;
            _source.clip = flightSound;
            _source.Play();

            _camera = Camera.main;
            var reflect = Vector3.Reflect(Quaternion.Euler(0, -90, 0) * _camera.transform.forward, hitInfo.normal).normalized;
            transform.position = _target + reflect * FlyDistance;
            transform.forward = (_target - transform.position).normalized;
            _fallTask = StartCoroutine(LaunchFireball());
        }

        private IEnumerator LaunchFireball()
        {
            // Performs flight towards target.
            do
            {
                transform.position += Vector3.Normalize(_target - transform.position) *
                                      (Time.deltaTime * FlyDistance / HitDelay);
                yield return new WaitForEndOfFrame();
                _currentHitTime += Time.deltaTime;
            } while (_currentHitTime <= HitDelay);

            _source.Stop();
            _source.PlayOneShot(explosionSound);

            // Register hit effects on mobs
            int hits = Physics.OverlapSphereNonAlloc(transform.position, HitDamageRadius, _availableTargetsPool, MobsLayerMask);
            var effect = new BurningEffect(BurnDamage, BurnDuration.SecondsToTicks());
            for (int i = 0; i < hits; i++)
            {
                var target = _availableTargetsPool[i];
                var mob = target.GetComponent<MobBehaviour>();
                float damage = HitDamageValue * Vector3.Distance(target.transform.position, transform.position) / HitDamageRadius;

                mob.HitThisMob(damage, BasicElement.Fire);
                mob.AddSingleEffect(effect);
                ApplyAdditionalEffects(mob);
            }

            // Aftershock animations & stuff
            print("Aftershock");
            StartCoroutine(RunExplosionAnimation());
            meteoriteMesh.enabled = false;
        }
        
        internal void Explode()
        {
            StopCoroutine(_fallTask);
            StartCoroutine(RunExplosionAnimation());
            meteoriteMesh.enabled = false;
        }

        private IEnumerator RunExplosionAnimation()
        {
            var hitPosition = transform.position;
            var sparkles = Instantiate(sparklesPrefab, hitPosition, Quaternion.identity);
            var smoke = Instantiate(smokePrefab, hitPosition + Vector3.up * 0.2f, Quaternion.Euler(90, 0, 0));
            DisableEmissionOnChildren();
            sparkles.transform.localScale = new Vector3(5, 5, 5);
            StartCoroutine(CameraShake());
            yield return new WaitForSecondsRealtime(explosionDelay);
            Destroy(sparkles);
            Destroy(smoke);
            Destroy(gameObject);
        }

        private IEnumerator CameraShake()
        {
            var position = _camera.transform.position;
            var amplitude = 0.3f;
            var timer = 0f;
            while (timer < shakeDuration)
            {
                timer += Time.deltaTime;
                _camera.transform.localPosition = position + Random.insideUnitSphere * amplitude;    
                yield return new WaitForEndOfFrame();
            }
        }
    }
}