using Mobs.MobsBehaviour;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Spells.SpellClasses
{
    
    public class LightiningBoltSpell : MagicSpell
    {
        private List<MobBehaviour> _mobsInRadius = new List<MobBehaviour>();
        private bool _isOverriden = false;

        [SerializeField] private LineRenderer lightining;
        [SerializeField] private float directDamage;
        [SerializeField] private int amountOFBounces;
        [SerializeField] private float reachRadius;
        

        public override BasicElement Element => BasicElement.Lightning;

        public override void ExecuteSpell(RaycastHit hit)
        {
            if (_isOverriden)
                return;

            Collider[] collidersInRadius = Physics.OverlapSphere(hit.point, reachRadius, ~0, QueryTriggerInteraction.Ignore);

            foreach (Collider collider in collidersInRadius)
            {
                if(collider != null && collider.gameObject.TryGetComponent<MobBehaviour>(out var mobBehaviour))
                    _mobsInRadius.Add(mobBehaviour);
            }

            if(_mobsInRadius.Count == 0)
            {
                Destroy(gameObject);
                return;
            }

            StartCoroutine(HitMobs(hit.point));
        }

        /// <summary>
        /// Used to redirect strike in another position
        /// </summary>
        /// <param name="hitCoordinates" >
        /// Coordinates of initial impact
        /// </param>
        /// <param name="redirectCoordinates">
        /// Strike destination cooridnates
        /// </param>
        public void OverrideStrike(Vector3 hitCoordinates, Vector3 redirectCoordinates)
        {
            _isOverriden = true;
            StartCoroutine(RenderOverride(hitCoordinates, redirectCoordinates));
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
            MobBehaviour mobToStrike = GetClosestMob(hitPosition);
            lightining.SetPosition(0, hitPosition);
            lightining.SetPosition(1, mobToStrike.transform.position);
            for(int bounce = amountOFBounces; bounce > 0; --bounce) 
            {
                yield return new WaitForSeconds(.1f);
                if(GetClosestMob(mobToStrike.transform.position) != null)
                {
                    mobToStrike = GetClosestMob(mobToStrike.transform.position);
                    _mobsInRadius.Remove(mobToStrike);
                    lightining.SetPosition(0, mobToStrike.transform.position);
                    if (GetClosestMob(mobToStrike.transform.position) != null)
                        lightining.SetPosition(1, GetClosestMob(hitPosition).transform.position);
                    else
                        lightining.enabled = false;

                }
                mobToStrike.HitThisMob(directDamage, BasicElement.Lightning);
            }
            Destroy(gameObject);
            yield return null;
        }

        private IEnumerator RenderOverride(Vector3 startCords, Vector3 endCords)
        {

            lightining.SetPosition(0, startCords);
            lightining.SetPosition(1, endCords);
            yield return new WaitForSeconds(.1f);
            Destroy(gameObject);
            yield return null;
        }
    }
}