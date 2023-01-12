using Mobs;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Spells.SpellClasses
{
    [Spell(BasicElement.Fire, "Meteor", "Casts a fire meteor on heads of your enemies.")]
    public class FireballSpell : MagicSpell
    {
        private const float HitDelay = 0.7f;
        private const int MobsLayerMask = 1 << 8;
        private readonly float flyDistance = 30;
        private float currentHitTime;
        private Vector3 target;

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
                Debug.DrawLine(transform.position, target, Color.red, 2);
                Debug.DrawRay(target, Vector3.up * HitDamageRadius, Color.blue, 2);
            }
        }

        private void Update()
        {
            // Performs flight towards target.
            transform.position += Vector3.Normalize(target - transform.position) * (Time.deltaTime * flyDistance / HitDelay);

            currentHitTime += Time.deltaTime;
            if (currentHitTime > HitDelay)
            {
                var targets = Physics.OverlapSphere(transform.position, HitDamageRadius, MobsLayerMask);
                var effects = new Effect[]
                {
                    new MeteoriteBurningEffect()
                };
                foreach (var target in targets)
                {
                    var mob = target.GetComponent<MobBehaviour>();
                    float damage = HitDamageValue * Vector3.Distance(target.transform.position, transform.position) / HitDamageRadius;
                    Debug.Log($"Target {target}, Damage: {damage}");
                    mob.HandleIncomeDamage(damage, BasicElement.Fire);
                    mob.AddReceivedEffects(effects);
                }
                Destroy(gameObject);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, HitDamageRadius);
        }
    }
}