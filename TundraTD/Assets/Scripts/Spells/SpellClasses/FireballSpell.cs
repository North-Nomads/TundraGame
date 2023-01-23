using City.Building.ElementPools;
using Mobs;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spells.SpellClasses
{
    [Spell(BasicElement.Fire, "Meteor", "Casts a fire meteor on heads of your enemies.")]
    public class FireballSpell : MagicSpell
    {
        private const float HitDelay = 0.5f;
        private const int MobsLayerMask = 1 << 8;
        private const int LavaLifetime = 5;
        private readonly float flyDistance = 30;
        private float currentHitTime;
        private Vector3 target;
        private static readonly Collider[] availableTargetsPool = new Collider[1000];

        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private GameObject lavaPrefab;
        [SerializeField] private float explosionDelay;

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
                target = hit.point;
                var reflect = Vector3.Reflect(Quaternion.Euler(0, -90, 0) * Camera.main.transform.forward, hit.normal).normalized;
                transform.position = hit.point + (reflect * flyDistance);
                transform.forward = target;
                HitDamageRadius *= FirePool.MeteorRadiusMultiplier;
            }
        }

        private void Update()
        {
            // Performs flight towards target.
            transform.position += Vector3.Normalize(target - transform.position) * (Time.deltaTime * flyDistance / (HitDelay - FirePool.MeteorLandingReduction));

            currentHitTime += Time.deltaTime;
            if (currentHitTime > HitDelay)
            {
                int hits = Physics.OverlapSphereNonAlloc(transform.position, HitDamageRadius, availableTargetsPool, MobsLayerMask);
                var effects = new List<Effect>()
                {
                    new MeteoriteBurningEffect(BurnDamage * FirePool.AfterburnDamageMultiplier, (int)BurnDuration)
                };
                if (FirePool.HasLandingStun)
                {
                    effects.Add(new StunEffect());// TODO: implement stun effect.
                }
                for (int i = 0; i < hits; i++)
                {
                    var target = availableTargetsPool[i];
                    var mob = target.GetComponent<MobBehaviour>();
                    float damage = HitDamageValue * Vector3.Distance(target.transform.position, transform.position) / HitDamageRadius;
                    Debug.Log($"Target {target}, Damage: {damage}");
                    mob.HandleIncomeDamage(damage * FirePool.DamageMultipliers[mob.MobBasicElement], BasicElement.Fire);
                    mob.AddReceivedEffects(effects);
                    if (FirePool.HasLandingImpulse)
                    {
                        mob.GetComponent<Rigidbody>().AddExplosionForce(damage, this.target, HitDamageRadius);
                    }
                }
                StartCoroutine(RunExplosionAnimation());
                if (FirePool.HasLandingLavaPool)
                    StartCoroutine(RunLavaPool());
                gameObject.SetActive(false);
            }
        }

        private IEnumerator RunLavaPool()
        {
            var lava = Instantiate(lavaPrefab);
            lava.transform.parent = transform;
            lava.transform.position = target;
            lava.transform.localScale = new Vector3(HitDamageRadius, 1, HitDamageRadius);
            for (int i = 0; i < LavaLifetime; i++)
            {
                int hits = Physics.OverlapBoxNonAlloc(target, new Vector3(HitDamageRadius, 1, HitDamageRadius), availableTargetsPool);
                for (int j = 0; j < hits; j++)
                {
                    var target = availableTargetsPool[j];
                    var mob = target.GetComponent<MobBehaviour>();
                    mob.HandleIncomeDamage(BurnDamage * FirePool.AfterburnDamageMultiplier, BasicElement.Fire);
                }
                yield return new WaitForSecondsRealtime(1f);
            }
            Destroy(lava);
        }

        private IEnumerator RunExplosionAnimation()
        {
            var obj = Instantiate(explosionPrefab);
            obj.transform.position = target;
            obj.transform.localScale = new Vector3(5, 5, 5);
            yield return new WaitForSecondsRealtime(explosionDelay);
            Destroy(obj);
            Destroy(gameObject);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, HitDamageRadius);
        }
    }
}