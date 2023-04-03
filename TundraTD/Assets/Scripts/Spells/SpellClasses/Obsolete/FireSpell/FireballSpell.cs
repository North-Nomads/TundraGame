//using System.Collections;
//using System.Collections.Generic;
//using City.Building.ElementPools;
//using Level;
//using Mobs.MobEffects;
//using Mobs.MobsBehaviour;
//using UnityEngine;
//using Random = UnityEngine.Random;

//namespace Spells.SpellClasses.FireSpell
//{
//    /// <summary>
//    /// Fireball (or meteorite) spell. Manages the behaviour of the spell on execution and until it's destruction
//    /// </summary>
//    public class FireballSpell_Obsolete : MagicSpell
//    {
//        private const float FlyDistance = 30;
//        private const float HitDelay = 0.5f;
//        private const int MobsLayerMask = 1 << 8;
//        private const int LavaLifetime = 5;
//        private const int TrapsAmount = 5;

//        private static readonly Collider[] AvailableTargetsPool = new Collider[1000];

//        [SerializeField] private MeshRenderer meteoriteMesh;
//        [SerializeField] private GameObject explosionPrefab;
//        [SerializeField] private GameObject lavaPrefab;
//        [SerializeField] private GameObject ghostPrefab;
//        [SerializeField] private float explosionDelay;
//        [SerializeField] private AudioClip flightSound;
//        [SerializeField] private AudioClip explosionSound;

//        [Header("Landing stun value")] [SerializeField] private float stunTime;
//        [SerializeField] private GameObject trapPrefab;
//        private Camera _mainCamera;
//        private float _currentHitTime;
//        private Vector3 _target;
//        private bool _isLanded;
//        private AudioSource _source;

//        /// <summary>
//        /// The radius of the hit area
//        /// </summary>
//        private float HitDamageRadius { get; set; } = 5f;

//        /// <summary>
//        /// The damage of the hit epicenter.
//        /// </summary>
//        private float HitDamageValue { get; set; } = 40f;

//        /// <summary>
//        /// Duration of the burn effect.
//        /// </summary>
//        private float BurnDuration { get; set; } = 3f;

//        /// <summary>
//        /// Damage of the burn effect.
//        /// </summary>
//        private float BurnDamage { get; set; } = 7f;

//        /// <summary>
//        /// Value of the slowness effect.
//        /// </summary>
//        public float SlownessValue { get; set; } = 0.3f;

//        /// <summary>
//        /// Duration of the slowness effect.
//        /// </summary>
//        public float SlownessDuration { get; set; }

//        public override BasicElement Element => throw new System.NotImplementedException();

//        public override void ExecuteSpell(RaycastHit hitInfo)
//        {
//            if (_mainCamera is null)
//                _mainCamera = Camera.main;

//            _target = hitInfo.point;
//            _source = GetComponent<AudioSource>();
//            _source.volume = GameParameters.EffectsVolumeModifier;
//            _source.clip = flightSound;
//            _source.Play();
            
//            var reflect = Vector3.Reflect(Quaternion.Euler(0, -90, 0) * Camera.main.transform.forward, hitInfo.normal).normalized;
//            transform.position = _target + reflect * FlyDistance;
//            transform.forward = (_target - transform.position).normalized;
//            StartCoroutine(LaunchFireball());
//        }
        

//        private IEnumerator LaunchFireball()
//        {
//            // Performs flight towards target.
//            do
//            {
//                transform.position += Vector3.Normalize(_target - transform.position) *
//                                      (Time.deltaTime * FlyDistance / HitDelay);
//                yield return new WaitForEndOfFrame();
//                _currentHitTime += Time.deltaTime;
//            } while (_currentHitTime <= HitDelay);

//            _source.Stop();
//            _source.PlayOneShot(explosionSound);

//            // Register hit effects on mobs
//            int hits = Physics.OverlapSphereNonAlloc(transform.position, HitDamageRadius, AvailableTargetsPool, MobsLayerMask);
//            var effects = new List<Effect>
//            {
//                new MeteoriteBurningEffect(BurnDamage, BurnDuration.SecondsToTicks(), !FirePool.HasWaterResist)
//            };

//            if (FirePool.HasLandingStun)
//                effects.Add(new SpikesStunEffect(stunTime.SecondsToTicks()));

//            for (int i = 0; i < hits; i++)
//            {
//                var target = AvailableTargetsPool[i];
//                var mob = target.GetComponent<MobBehaviour>();
//                float damage = HitDamageValue * Vector3.Distance(target.transform.position, transform.position) / HitDamageRadius;

//                mob.HitThisMob(damage, BasicElement.Fire, nameof(LaunchFireball));
//                mob.AddReceivedEffects(effects);
//                if (FirePool.HasLandingImpulse)
//                {
//                    mob.GetComponent<Rigidbody>().AddExplosionForce(HitDamageRadius * HitDamageRadius, _target, HitDamageRadius);
//                    mob.AddSingleEffect(new SpikesStunEffect(stunTime.SecondsToTicks()));
//                }
//            }

//            // Aftershock animations & stuff
//            StartCoroutine(RunExplosionAnimation());
//            if (FirePool.HasLavaPool)
//                StartCoroutine(RunLavaPool());
//            if (FirePool.HasLandingGhost)
//                Instantiate(ghostPrefab, _target + Vector3.up * 2, default);
//            if (FirePool.HasLandingTraps)
//            {
//                var hitInfos = new RaycastHit[5];
//                for (int i = 0; i < TrapsAmount; i++)
//                {
//                    float deltaX = Random.Range(-5, 5);
//                    float deltaZ = Random.Range(-5, 5);
//                    Ray summonRay = new Ray(new Vector3(_target.x + deltaX, _target.y + 10, _target.z + deltaZ), Vector3.down);
//                    if (Physics.Raycast(summonRay, out var hitInfo, 15))
//                    {
//                        hitInfos[i] = hitInfo;
//                    }
//                }
//                foreach (var hit in hitInfos)
//                {
//                    if (hit.collider != null)
//                    {
//                        Instantiate(trapPrefab, hit.point, Random.rotation);
//                    }
//                }
//            }
//            meteoriteMesh.enabled = false;
//        }

//        private IEnumerator RunLavaPool()
//        {
//            var lava = Instantiate(lavaPrefab, transform, true);
//            // The addition is performed to correctly display hit area.
//            lava.transform.position = _target + Vector3.up * 0.1f;
//            lava.transform.localScale = new Vector3(HitDamageRadius, 1, HitDamageRadius);
//            for (int i = 0; i < LavaLifetime; i++)
//            {
//                int hits = Physics.OverlapCapsuleNonAlloc(_target, _target + Vector3.up * 5, HitDamageRadius, AvailableTargetsPool, MobsLayerMask);
//                for (int j = 0; j < hits; j++)
//                {
//                    var target = AvailableTargetsPool[j];
//                    var mob = target.GetComponent<MobBehaviour>();
//                    var damage = BurnDamage;
//                    mob.HitThisMob(damage, BasicElement.Fire, nameof(RunLavaPool));
//                }
//                yield return new WaitForSecondsRealtime(1f);
//            }
//            Destroy(lava);
//        }

//        private IEnumerator RunExplosionAnimation()
//        {
//            var obj = Instantiate(explosionPrefab, transform);
//            DisableEmissionOnChildren();
//            obj.transform.position = _target;
//            obj.transform.localScale = new Vector3(5, 5, 5);
//            yield return new WaitForSecondsRealtime(FirePool.HasLavaPool ? Mathf.Max(explosionDelay, LavaLifetime) : explosionDelay);
//            Destroy(gameObject);
//        }
        
//        #pragma warning disable CS0618
//        private void DisableEmissionOnChildren()
//        {
//            foreach (var system in meteoriteMesh.GetComponentsInChildren<ParticleSystem>())
//                system.enableEmission = false;
//        }
//    }
//}