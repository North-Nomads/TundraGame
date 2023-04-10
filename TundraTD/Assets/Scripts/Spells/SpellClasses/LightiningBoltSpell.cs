using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
namespace Spells.SpellClasses
{
    
    public class LightiningBoltSpell : MagicSpell
    {
        private List<MobBehaviour> _mobsInRadius = new List<MobBehaviour>();
        [SerializeField] private LineRenderer lightining;
        [SerializeField] private float directDamage;
        [SerializeField] private int amountOFBounces;

        public override BasicElement Element => BasicElement.Lightning;

        public override void ExecuteSpell(RaycastHit hit)
        {
            Collider[] collidersInRadius =  new Collider[200];
            Physics.OverlapSphereNonAlloc(hit.point, 10, collidersInRadius, ~0, QueryTriggerInteraction.Ignore);

            lightining.enabled = true;
            lightining.SetPosition(0, new Vector3(hit.point.x, hit.point.y, hit.point.z));
            lightining.SetPosition(1, Camera.main.transform.position);

            foreach (Collider collider in collidersInRadius)
            {
                if(collider != null && collider.gameObject.TryGetComponent<MobBehaviour>(out var mobBehaviour))
                    _mobsInRadius.Add(mobBehaviour);
            }

            if(_mobsInRadius.Count == 0)
            {
                Debug.Log("Miss");
                return;
            }

            StartCoroutine(HitMobs(hit.point));
        }

        private MobBehaviour GetClosestMob(Vector3 CurrentMobPosition)
        {
            if (!_mobsInRadius.Any())
                return null;
            MobBehaviour closestMob = _mobsInRadius[0];
            foreach(MobBehaviour mob in _mobsInRadius)
            {
                if((CurrentMobPosition - mob.transform.position).magnitude < (CurrentMobPosition - closestMob.transform.position).magnitude)
                    closestMob = mob;
            }
            Debug.DrawLine(CurrentMobPosition, closestMob.transform.position);
            return closestMob;
        }

        private IEnumerator HitMobs(Vector3 hitPosition)
        {
            MobBehaviour mobToStrike, nextMob = GetClosestMob(hitPosition);
            for(int strikesLeft = amountOFBounces; strikesLeft >= 0 && _mobsInRadius.Count > 0; strikesLeft--)
            {
                mobToStrike = nextMob;
                lightining.SetPosition(0, nextMob.transform.position);
                _mobsInRadius.Remove(mobToStrike);
                nextMob = GetClosestMob(mobToStrike.transform.position);
                if(nextMob == null)
                {
                    lightining.SetPosition(0, new Vector3(0, -100, 0));
                    lightining.SetPosition(1, new Vector3(0, -100, 0));
                }
                else
                    lightining.SetPosition(1, nextMob.transform.position);

                mobToStrike.HitThisMob(directDamage, BasicElement.Lightning, "LightningStrike");
                yield return new WaitForSeconds(.1f);
                
            }
            
            Destroy(gameObject);
            yield return null;
        }
    }
}