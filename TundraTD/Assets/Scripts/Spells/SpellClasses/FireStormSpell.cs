using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Level;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Spells.SpellClasses
{
    public class FireStormSpell : MagicSpell
    {
        [SerializeField] private float lifetime;
        private Vector3 _tornadoCenter;

        private Collider[] _mobs;
        private BoxCollider _boxCollider;
        private List<MobBehaviour> _affectedMobs;
        public override BasicElement Element => BasicElement.Fire | BasicElement.Air;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            _boxCollider = GetComponent<BoxCollider>();
            _boxCollider.enabled = false;
            _mobs = new Collider[64];
            _affectedMobs = new List<MobBehaviour>();
            transform.position = hitInfo.point;
            _tornadoCenter = hitInfo.point;
            StartCoroutine(StayAlive());
        }

        private void HandleStormDestroying()
        {
            foreach (var mob in _affectedMobs)
            {
                if (!mob.MobModel.IsAlive)
                    return;
                
                mob.ClearMobEffects();
                mob.IsMoving = false;
                mob.MobModel.Rigidbody.AddForce(Vector3.down * 10000);
                mob.IsMoving = true;
                mob.SetFocusingTarget(null);
            }
        }

        private void ScanForMobs(int c)
        {
            for (int i = 0; i < c; i++)
            {
                var mobCollider = _mobs[i];
                var mob = mobCollider.GetComponent<MobBehaviour>();
                if (mob is null)
                    continue;
                
                if (mob.CurrentEffects.Any(x => x is TornadoEffect))
                    continue;

                var destination = _tornadoCenter + Vector3.up * 4;
                mob.SetFocusingTarget(destination);
                mob.AddReceivedEffects(new List<Effect>
                {
                    new BurningEffect(.5f, 6f.SecondsToTicks(), false),
                    new InspirationEffect(),
                    new SpeedEffect(6f.SecondsToTicks(), 4),
                    new TornadoEffect(lifetime.SecondsToTicks())
                });
                ApplyAdditionalEffects(mob);
                _affectedMobs.Add(mob);
            }
        }

        private IEnumerator StayAlive()
        {
            float step = lifetime / 10;
            float i = 0;
            while (i < lifetime)
            {
                var c = Physics.OverlapBoxNonAlloc(transform.position + Vector3.up * 2, _boxCollider.size / 2, _mobs);
                ScanForMobs(c);
                i += step;
                yield return new WaitForSeconds(.5f);
            }
            
            HandleStormDestroying();
            Destroy(gameObject);
        }
    }
}