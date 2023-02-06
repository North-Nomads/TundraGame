using City.Building.ElementPools;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spells.SpellClasses
{
    /// <summary>
    /// Fireball (or meteorite) spell. Manages the behaviour of the spell on execution and until it's destruction
    /// </summary>
    [Spell(BasicElement.Fire, "Meteor", "Casts a fire meteor on heads of your enemies.")]
    public class FireballSpell : MagicSpell
    {
        private const float FlyDistance = 30;
        private const float HitDelay = 0.5f;
        private const int MobsLayerMask = 1 << 8;
        private const int LavaLifetime = 5;
        
        private static readonly Collider[] AvailableTargetsPool = new Collider[1000];

        [SerializeField] private MeshRenderer meteoriteMesh;
        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private GameObject lavaPrefab;
        [SerializeField] private float explosionDelay;
        private float _currentHitTime;
        private Vector3 _target;
        private bool _isLanded;
        //private Camera _mainCamera;
        
        /// <summary>
        /// The radius of the hit area
        /// </summary>
        public float HitDamageRadius { get; set; } = 5f;

        /// <summary>
        /// The damage of the hit epicenter.
        /// </summary>
        [MultiplictableProperty(BasicElement.Earth, 1.35f)]
        public float HitDamageValue { get; set; } = 100f;

        /// <summary>
        /// Duration of the burn effect.
        /// </summary>
        [IncreasableProperty(BasicElement.Air, 5f)]
        public float BurnDuration { get; set; } = 5f;

        /// <summary>
        /// Damage of the burn effect.
        /// </summary>
        [MultiplictableProperty(BasicElement.Fire, 1.25f)]
        [MultiplictableProperty(BasicElement.Water, 0.75f)]
        public float BurnDamage { get; set; } = 20f;

        /// <summary>
        /// Value of the slowness effect.
        /// </summary>
        public float SlownessValue { get; set; } = 0.3f;

        /// <summary>
        /// Duration of the slowness effect.
        /// </summary>
        [IncreasableProperty(BasicElement.Lightning, 2f)]
        public float SlownessDuration { get; set; }

        public override void ExecuteSpell()
        {
            Debug.Log("Fireball has been executed!");
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var hit))
            {
                _target = hit.point;
                var reflect = Vector3.Reflect(Quaternion.Euler(0, -90, 0) * Camera.main.transform.forward, hit.normal).normalized;
                transform.position = hit.point + (reflect * FlyDistance);
                transform.forward = _target;
                HitDamageRadius *= FirePool.MeteorRadiusMultiplier;
                StartCoroutine(LaunchFireball());
            }
        }

        private IEnumerator LaunchFireball()
        {
            // Performs flight towards target.
            do
            {
                transform.position += Vector3.Normalize(_target - transform.position) *
                                      (Time.deltaTime * FlyDistance / (HitDelay - FirePool.MeteorLandingReduction));
                yield return new WaitForEndOfFrame();
                _currentHitTime += Time.deltaTime;
            } while (_currentHitTime <= HitDelay - FirePool.MeteorLandingReduction);
            
            // Register hit effects on mobs
            int hits = Physics.OverlapSphereNonAlloc(transform.position, HitDamageRadius, AvailableTargetsPool, MobsLayerMask);
            var effects = new List<Effect>
            {
                new MeteoriteBurningEffect(BurnDamage * FirePool.AfterburnDamageMultiplier, (int)BurnDuration)
            };
            
            if (FirePool.HasLandingStun)
                effects.Add(new StunEffect()); // TODO: implement stun effect.
            
            for (int i = 0; i < hits; i++)
            {
                var target = AvailableTargetsPool[i];
                var mob = target.GetComponent<MobBehaviour>();
                float damage = HitDamageValue * Vector3.Distance(target.transform.position, transform.position) / HitDamageRadius;
                
                Debug.Log($"Target {target}, Damage: {damage}");
                mob.HandleIncomeDamage(damage * FirePool.DamageMultipliers[mob.MobBasicElement], BasicElement.Fire);
                mob.AddReceivedEffects(effects);
                if (FirePool.HasLandingImpulse)
                {
                    mob.GetComponent<Rigidbody>().AddExplosionForce(damage, _target, HitDamageRadius);
                }
            }

            // Aftershock animations & stuff
            StartCoroutine(RunExplosionAnimation());
            if (FirePool.HasLandingLavaPool)
                StartCoroutine(RunLavaPool());
            meteoriteMesh.enabled = false;
        }

        private IEnumerator RunLavaPool()
        {
            var lava = Instantiate(lavaPrefab, transform, true);
            lava.transform.position = _target;
            lava.transform.localScale = new Vector3(HitDamageRadius, 1, HitDamageRadius);
            for (int i = 0; i < LavaLifetime; i++)
            {
                int hits = Physics.OverlapBoxNonAlloc(_target, new Vector3(HitDamageRadius, 1, HitDamageRadius), AvailableTargetsPool);
                for (int j = 0; j < hits; j++)
                {
                    var target = AvailableTargetsPool[j];
                    var mob = target.GetComponent<MobBehaviour>();
                    mob.HandleIncomeDamage(BurnDamage * FirePool.AfterburnDamageMultiplier, BasicElement.Fire);
                }
                yield return new WaitForSecondsRealtime(1f);
            }
            Destroy(lava);
        }

        private IEnumerator RunExplosionAnimation()
        {
            var obj = Instantiate(explosionPrefab, transform);
            DisableEmissionOnChildren();
            obj.transform.position = _target;
            obj.transform.localScale = new Vector3(5, 5, 5);
            yield return new WaitForSecondsRealtime(explosionDelay);
            Destroy(gameObject);
        }

        // харчок
        #pragma warning disable CS0618
        private void DisableEmissionOnChildren()
        {
            foreach (var system in meteoriteMesh.GetComponentsInChildren<ParticleSystem>())
                system.enableEmission = false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, HitDamageRadius);
        }
    }
}