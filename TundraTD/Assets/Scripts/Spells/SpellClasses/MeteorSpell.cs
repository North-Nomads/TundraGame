using System.Collections;
using Level;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Spells.SpellClasses
{
	public class MeteorSpell : MagicSpell
	{
        [SerializeField] private MeshRenderer meteoriteMesh;
        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private float explosionDelay = 2;
        [SerializeField] private AudioClip flightSound;
        [SerializeField] private AudioClip explosionSound;
       
        private const float FlyDistance = 30;
        private const float HitDelay = 0.5f;
        private const int MobsLayerMask = 1 << 8;
        
        private readonly Collider[] _availableTargetsPool = new Collider[1000];
        private Camera _mainCamera;
        private float _currentHitTime;
        private Vector3 _target;
        private bool _isLanded;
        private AudioSource _source;

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

        /// <summary>
        /// Value of the slowness effect.
        /// </summary>
        public float SlownessValue { get; set; } = 0.3f;

        /// <summary>
        /// Duration of the slowness effect.
        /// </summary>
        public float SlownessDuration { get; set; }

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

            var reflect = Vector3.Reflect(Quaternion.Euler(0, -90, 0) * Camera.main.transform.forward, hitInfo.normal).normalized;
            transform.position = _target + reflect * FlyDistance;
            transform.forward = (_target - transform.position).normalized;
            StartCoroutine(LaunchFireball());
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
            }

            // Aftershock animations & stuff
            print("Aftershock");
            StartCoroutine(RunExplosionAnimation(transform.position));
            meteoriteMesh.enabled = false;
        }

        private IEnumerator RunExplosionAnimation(Vector3 hitPosition)
        {
            var obj = Instantiate(explosionPrefab, hitPosition, Quaternion.identity);
            DisableEmissionOnChildren();
            obj.transform.position = _target;
            obj.transform.localScale = new Vector3(5, 5, 5);
            yield return new WaitForSecondsRealtime(explosionDelay);
            Destroy(gameObject);
        }
    }
}