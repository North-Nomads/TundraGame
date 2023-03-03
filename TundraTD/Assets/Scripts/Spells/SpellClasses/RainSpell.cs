using City.Building.ElementPools;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using System.Collections;
using UnityEngine;

namespace Spells.SpellClasses
{
    [Spell(BasicElement.Water, "Rain", "Throws the rain into your enemies.")]
    public class RainSpell : MagicSpell
    {
        private const float RainHeight = 30f;

        [SerializeField] private GameObject rainParticles;
        [SerializeField] private GameObject rainSplashes;
        [SerializeField] private CapsuleCollider mainCollider;
        private Vector3 _targetPosition;

        [IncreasableProperty(BasicElement.Air, 2f)]
        private float Radius { get; set; } = 10f;

        [IncreasableProperty(BasicElement.Water, 1f)]
        [IncreasableProperty(BasicElement.Lightning, -1f)]
        private float Lifetime { get; set; } = 3f;

        [IncreasableProperty(BasicElement.Lightning, -1f)]
        private float EffectTime { get; set; } = 10f;

        [IncreasableProperty(BasicElement.Earth, 0.05f)]
        private float SlownessValue { get; set; } = 0.1f;

        [IncreasableProperty(BasicElement.Fire, 0.02f)]
        private float LightningMultiplier { get; set; } = 1.1f;

        public override void ExecuteSpell()
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var hit))
            {
                _targetPosition = hit.point;
                rainSplashes.transform.position = (transform.position = _targetPosition) + Vector3.up;
                rainParticles.SetActive(true);
                rainSplashes.SetActive(true);
                if (WaterPool.UnlimitedRadius) Radius = 1000;
                if (WaterPool.CreateBarrier) mainCollider.isTrigger = false;
                if (WaterPool.AdditionalSlowness) SlownessValue *= 3;
                if (WaterPool.AllowSuperLightning) LightningMultiplier *= 3;
                rainParticles.transform.localScale = new Vector3(Radius / 10, RainHeight, Radius / 10);
                mainCollider.radius = Radius;
                mainCollider.height = RainHeight;
                rainParticles.transform.localPosition = (Vector3.up * (RainHeight / 2));
                rainSplashes.transform.localScale = new Vector3(Radius / 10, 1, Radius / 10);
                StartCoroutine(WaitTime());
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private IEnumerator WaitTime()
        {
            yield return new WaitForSeconds(Lifetime);
            DisableEmissionOnChildren();
            mainCollider.enabled = false;
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Mob"))
            {
                var mob = other.gameObject.GetComponent<MobBehaviour>();
                ApplyEffects(mob);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Mob"))
            {
                var mob = collision.gameObject.GetComponent<MobBehaviour>();
                ApplyEffects(mob);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (WaterPool.CastSnowInsteadOfRain && other.CompareTag("Mob"))
            {
                var mob = other.gameObject.GetComponent<MobBehaviour>();
                var freeze = mob.CurrentEffects.OfType<FreezeEffect>().FirstOrDefault();
                if (freeze != null)
                {
                    freeze.ContinueFreeze = true;
                }
                else
                {
                    mob.AddSingleEffect(new FreezeEffect(0, 2, (int)EffectTime));
                }
            }
        }

        private void ApplyEffects(MobBehaviour mob)
        {
            // Add here basic effects
            var effects = new List<Effect>()
            {
                new SlownessEffect(1 - SlownessValue, (int)EffectTime),
                new VulnerabilityEffect((int)EffectTime, BasicElement.Lightning, 1 / LightningMultiplier)
            };
            if (WaterPool.ApplyWeaknessOnEnemies)
            {
                effects.Add(new WeaknessEffect((int)EffectTime, 0.5f));
            }
            if (WaterPool.CastSnowInsteadOfRain)
            {
                effects.Add(new FreezeEffect(0, 2, (int)EffectTime));
            }
            mob.AddReceivedEffects(effects);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Mob"))
            {
                var mob = collision.gameObject.GetComponent<MobBehaviour>();
                mob.AddReceivedEffects(new Effect[] { new SlownessEffect(1 - SlownessValue, (int)EffectTime), new WeaknessEffect((int)EffectTime, BasicElement.Lightning, 1 / LightningMultiplier) });
            }
        }

        private void DisableEmissionOnChildren()
        {
            foreach (var system in GetComponentsInChildren<ParticleSystem>())
            {
                var emission = system.emission;
                emission.enabled = false;
            }
        }
    }
}
