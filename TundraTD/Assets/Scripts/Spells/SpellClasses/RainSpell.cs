using City.Building.ElementPools;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
        public float Radius { get; set; } = 10f;

        [IncreasableProperty(BasicElement.Water, 1f)]
        [IncreasableProperty(BasicElement.Lightning, -1f)]
        public float Lifetime { get; set; } = 3f;

        [IncreasableProperty(BasicElement.Lightning, -1f)]
        public float EffectTime { get; set; } = 10f;

        [IncreasableProperty(BasicElement.Earth, 0.05f)]
        public float SlownessValue { get; set; } = 0.1f;

        [IncreasableProperty(BasicElement.Fire, 0.02f)]
        public float LightningMultiplier { get; set; } = 1.1f;

        public override void ExecuteSpell()
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var hit))
            {
                _targetPosition = hit.point;
                rainSplashes.transform.position = (transform.position = _targetPosition) + Vector3.up;
                rainParticles.SetActive(true);
                rainSplashes.SetActive(true);
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

        public IEnumerator WaitTime()
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
