using Level;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spells
{
    public class RainSpell : MagicSpell
    {
        private const float RainHeight = 30f;

        [SerializeField] private GameObject rainParticles;
        [SerializeField] private GameObject rainSplashes;
        [SerializeField] private CapsuleCollider mainCollider;
        private Vector3 _targetPosition;

        private float Radius { get; set; } = 10f;

        private float Lifetime { get; set; } = 3f;

        private float EffectTime { get; set; } = 10f;

        private float SlownessValue { get; set; } = 0.1f;

        private float LightningMultiplier { get; set; } = 1.1f;

        public override BasicElement Element => BasicElement.Water;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            _targetPosition = hitInfo.point;
            rainSplashes.transform.position = (transform.position = _targetPosition) + Vector3.up;
            rainParticles.SetActive(true);
            rainSplashes.SetActive(true);
            var shape = rainParticles.GetComponent<ParticleSystem>().shape;
            shape.radius = Radius / 10;
            mainCollider.radius = Radius;
            mainCollider.height = RainHeight;
            rainParticles.transform.localPosition = Vector3.up * (RainHeight / 2);
            rainSplashes.transform.localScale = new Vector3(Radius / 10, 1, Radius / 10);
            StartCoroutine(WaitTime());
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
            if (other.CompareTag("Mob"))
            {
                var mob = other.gameObject.GetComponent<MobBehaviour>();
                mob.RemoveFilteredEffects(x => x is BurningEffect effect && effect.CanBeExtinguished);
            }
        }

        private void ApplyEffects(MobBehaviour mob)
        {
            // Add here basic effects
            var effects = new List<Effect>()
            {
                new SlownessEffect(1 - SlownessValue, EffectTime.SecondsToTicks()),
                new VulnerabilityEffect(EffectTime.SecondsToTicks(), BasicElement.Lightning, 1 / LightningMultiplier)
            };
            mob.AddReceivedEffects(effects);
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