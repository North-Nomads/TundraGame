using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using System.Collections;
using UnityEngine;

namespace Spells.SpellClasses
{
    [Spell(BasicElement.Fire, "Meteor", "Casts a fire meteor on heads of your enemies.")]
    public class FireballSpell : MagicSpell
    {
        private const float HitDelay = 0.5f;
        private const int MobsLayerMask = 1 << 8;
        private const float FlyDistance = 30;
        private float _currentHitTime;
        private Vector3 _target;
        private UnityEngine.Camera _mainCamera;

        [SerializeField] private GameObject explosionPrefab;
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

        private void Start()
        {
            _mainCamera = UnityEngine.Camera.main;
        }

        public override void ExecuteSpell()
        {
            Debug.Log("Fireball has been executed!");
            if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out var hit))
            {
                _target = hit.point;
                var reflect = Vector3.Reflect(Quaternion.Euler(0, -90, 0) * _mainCamera.transform.forward, hit.normal).normalized;
                transform.position = hit.point + (reflect * FlyDistance);
                transform.forward = _target;
                Debug.DrawLine(transform.position, _target, Color.red, 2);
                Debug.DrawRay(_target, Vector3.up * HitDamageRadius, Color.blue, 2);
            }
        }

        private void Update()
        {
            // Performs flight towards target.
            transform.position += Vector3.Normalize(_target - transform.position) * (Time.deltaTime * FlyDistance / HitDelay);

            _currentHitTime += Time.deltaTime;
            if (_currentHitTime > HitDelay)
            {
                var targets = Physics.OverlapSphere(transform.position, HitDamageRadius, MobsLayerMask);
                var effects = new Effect[]
                {
                    new MeteoriteBurningEffect(BurnDamage, (int)BurnDuration)
                };
                foreach (var target in targets)
                {
                    var mob = target.GetComponent<MobBehaviour>();
                    float damage = HitDamageValue * Vector3.Distance(target.transform.position, transform.position) / HitDamageRadius;
                    Debug.Log($"Target {target}, Damage: {damage}");
                    mob.HandleIncomeDamage(damage, BasicElement.Fire);
                    mob.AddReceivedEffects(effects);
                }
                StartCoroutine(RunExplosionAnimation());
                Destroy(gameObject);
            }
        }

        private IEnumerator RunExplosionAnimation()
        {
            var obj = Instantiate(explosionPrefab);
            obj.transform.position = _target;
            obj.transform.localScale = new Vector3(5, 5, 5);
            yield return new WaitForSecondsRealtime(explosionDelay);
            Destroy(obj);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, HitDamageRadius);
        }
    }
}