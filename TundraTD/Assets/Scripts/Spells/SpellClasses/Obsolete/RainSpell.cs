//using CartoonFX;
//using City.Building.ElementPools;
//using Level;
//using Mobs.MobEffects;
//using Mobs.MobsBehaviour;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//namespace Spells.SpellClasses
//{
//    public class RainSpell_Obsolete : MagicSpell
//    {
//        private const float RainHeight = 30f;

//        [SerializeField] private GameObject rainParticles;
//        [SerializeField] private GameObject rainSplashes;
//        [SerializeField] private CapsuleCollider mainCollider;
//        [SerializeField] private GameObject barrierCollider;
//        private Vector3 _targetPosition;

//        private float Radius { get; set; } = 10f;

//        private float Lifetime { get; set; } = 3f;

//        private float EffectTime { get; set; } = 10f;

//        private float SlownessValue { get; set; } = 0.1f;

//        private float LightningMultiplier { get; set; } = 1.1f;

//        public override BasicElement Element => throw new System.NotImplementedException();

//        public override void ExecuteSpell(RaycastHit hitInfo)
//        {
//            _targetPosition = hitInfo.point;
//            rainSplashes.transform.position = (transform.position = _targetPosition) + Vector3.up;
//            rainParticles.SetActive(true);
//            rainSplashes.SetActive(true);
//            // TODO: implement it as normal full-map effect.
//            if (WaterPool.UnlimitedRadius) Radius = 50;
//            barrierCollider.SetActive(WaterPool.CreateBarrier);
//            barrierCollider.transform.localScale = new Vector3(Radius * 10, 100, Radius * 10);
//            if (WaterPool.AdditionalSlowness) SlownessValue *= 3;
//            if (WaterPool.AllowSuperLightning) LightningMultiplier *= 3;
//            var shape = rainParticles.GetComponent<ParticleSystem>().shape;
//            shape.radius = Radius / 10;
//            mainCollider.radius = Radius;
//            mainCollider.height = RainHeight;
//            rainParticles.transform.localPosition = Vector3.up * (RainHeight / 2);
//            rainSplashes.transform.localScale = new Vector3(Radius / 10, 1, Radius / 10);
//            StartCoroutine(WaitTime());
//        }

//        private IEnumerator WaitTime()
//        {
//            yield return new WaitForSeconds(Lifetime);
//            DisableEmissionOnChildren();
//            mainCollider.enabled = false;
//            yield return new WaitForSeconds(1);
//            Destroy(gameObject);
//        }

//        private void OnTriggerEnter(Collider other)
//        {
//            if (other.CompareTag("Mob"))
//            {
//                var mob = other.gameObject.GetComponent<MobBehaviour>();
//                ApplyEffects(mob);
//            }
//        }

//        private void OnCollisionEnter(Collision collision)
//        {
//            if (collision.gameObject.CompareTag("Mob"))
//            {
//                var mob = collision.gameObject.GetComponent<MobBehaviour>();
//                ApplyEffects(mob);
//            }
//        }

//        private void OnTriggerStay(Collider other)
//        {
//            if (other.CompareTag("Mob"))
//            {
//                var mob = other.gameObject.GetComponent<MobBehaviour>();
//                mob.RemoveFilteredEffects(x => x is MeteoriteBurningEffect effect && effect.CanBeExtinguished);
//                if (WaterPool.CastSnowInsteadOfRain)
//                {
//                    var freeze = mob.CurrentEffects.OfType<FreezeEffect>().FirstOrDefault();
//                    if (freeze != null)
//                    {
//                        freeze.ContinueFreeze = true;
//                    }
//                    else
//                    {
//                        mob.AddSingleEffect(new FreezeEffect(0, 2, EffectTime.SecondsToTicks()));
//                    }
//                }
//            }
//        }

//        private void ApplyEffects(MobBehaviour mob)
//        {
//            // Add here basic effects
//            var effects = new List<Effect>()
//            {
//                new SlownessEffect(1 - SlownessValue, EffectTime.SecondsToTicks()),
//                new VulnerabilityEffect(EffectTime.SecondsToTicks(), BasicElement.Lightning, 1 / LightningMultiplier)
//            };
//            if (WaterPool.ApplyWeaknessOnEnemies)
//            {
//                effects.Add(new WeaknessEffect(EffectTime.SecondsToTicks(), 0.5f));
//            }
//            if (WaterPool.CastSnowInsteadOfRain)
//            {
//                effects.Add(new FreezeEffect(0, 2, EffectTime.SecondsToTicks()));
//            }
//            mob.AddReceivedEffects(effects);
//        }

//        private void DisableEmissionOnChildren()
//        {
//            foreach (var system in GetComponentsInChildren<ParticleSystem>())
//            {
//                var emission = system.emission;
//                emission.enabled = false;
//            }
//        }
//    }
//}
