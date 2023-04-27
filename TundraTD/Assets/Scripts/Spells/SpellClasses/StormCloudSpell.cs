using System.Collections;
using System.Collections.Generic;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Spells.SpellClasses
{
    public class StormCloudSpell : MagicSpell
    {
        [SerializeField] private float lifetime;
        [SerializeField] private float zapDamage;
        [SerializeField] private float zapCooldownTime;
        private List<MobBehaviour> _cachedMobs;
        private float _zapTimer;

        public override BasicElement Element => BasicElement.Lightning | BasicElement.Water;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            transform.position = hitInfo.point;
            _cachedMobs = new List<MobBehaviour>();
            StartCoroutine(StayAlive());
        }

        private IEnumerator StayAlive()
        {
            yield return new WaitForSeconds(lifetime);
            Destroy(gameObject);
        }

        private void Update()
        {
            // Check if cloud is ready to zap
            if (_zapTimer >= 0)
            {
                _zapTimer -= Time.deltaTime;
                return;
            }

            // Get the first alive mob or null. Attack alive mob 
            var mob = GetFirstAliveMob();
            if (!(mob is null))
                ApplyZapOnMob();
            

            MobBehaviour GetFirstAliveMob()
            {
                // Searching for first alive mob. If mob is dead -> remove it from the list
                int decrement = 0;
                for (int i = 0; i < _cachedMobs.Count - decrement; i++)
                {
                    if (!_cachedMobs[i].MobModel.IsAlive)
                    {
                        _cachedMobs.RemoveAt(i);
                        decrement++;
                    }
                    else
                    {
                        return _cachedMobs[i];
                    }
                }

                // If no mobs are in list or are alive -> return null
                return null;
            }
            
            // Apply all effects on this mob
            void ApplyZapOnMob()
            {
                mob.ClearMobEffects();
                mob.HitThisMob(zapDamage, BasicElement.Lightning);
                mob.AddSingleEffect(new StunEffect(3));
                _zapTimer = zapCooldownTime;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Mob"))
                return;

            var mob = other.GetComponent<MobBehaviour>();
            _cachedMobs.Add(mob);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Mob"))
                return;

            var mob = other.GetComponent<MobBehaviour>();
            _cachedMobs.Remove(mob);
        }
    }
}