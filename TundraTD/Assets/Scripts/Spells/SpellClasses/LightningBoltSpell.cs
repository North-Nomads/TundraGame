using System;
using Mobs.MobsBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spells.SpellClasses
{
    [Spell(BasicElement.Lightning, "Lightning", "Shocking!")]
    public class LightningBoltSpell : MagicSpell
    {
        private readonly List<MobBehaviour> _mobsInRadius = new List<MobBehaviour>();
        [SerializeField] private LineRenderer lightning;
        public override void ExecuteSpell()
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit, float.PositiveInfinity, 1<<8|1<<10))
                return;
            Collider[] collidersInRadius =  new Collider[200];
            Physics.OverlapSphereNonAlloc(hit.point, 10, collidersInRadius, ~0, QueryTriggerInteraction.Ignore);
            foreach (Collider collider in collidersInRadius)
            {
                if(collider != null && collider.gameObject.TryGetComponent<MobBehaviour>(out var mobBehaviour))
                    _mobsInRadius.Add(mobBehaviour);
            }
            lightning.enabled = true;
            lightning.SetPosition(0, hit.point);
            if(_mobsInRadius.Count == 0)
            {
                Debug.Log("Miss");
                return;
            }

            lightning.SetPosition(1, GetClosestMob(hit.point).transform.position);
            LaunchStrike(GetClosestMob(hit.point),5);
        }

        private void LaunchStrike(MobBehaviour mobToStrike, int strikesLeft)
        {
            mobToStrike.HitThisMob(10000, BasicElement.Lightning, "Lightning.Strike");
            _mobsInRadius.Remove(mobToStrike);
            if(strikesLeft == 0 || _mobsInRadius.Count == 0)
                return;
           


            LaunchStrike(GetClosestMob(mobToStrike.transform.position), strikesLeft - 1);
        }

        private MobBehaviour GetClosestMob(Vector3 currentMobPosition)
        {
            MobBehaviour closestMob = _mobsInRadius[0];
            foreach(MobBehaviour mob in _mobsInRadius)
            {
                if((currentMobPosition - mob.transform.position).magnitude < (currentMobPosition - closestMob.transform.position).magnitude)
                    closestMob = mob;
            }
            Debug.DrawLine(currentMobPosition, closestMob.transform.position);
            return closestMob;
        }

        private IEnumerator DrawLightning()
        {
            yield return null;
        }
    }
}