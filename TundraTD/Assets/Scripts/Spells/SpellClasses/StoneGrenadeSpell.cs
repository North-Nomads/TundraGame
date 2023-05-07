using System.Collections;
using System.Collections.Generic;
using Level;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Spells.SpellClasses
{
    public class StoneGrenadeSpell : MagicSpell
    {
        [SerializeField] private float liftTime;
        [SerializeField] private float lifeTime;

        private readonly Collider[] _grenadeExplosionResults = new Collider[100];
        
        private const int MobLayer = 1 << 8; 
        public override BasicElement Element => BasicElement.Earth | BasicElement.Air;

        public override void ExecuteSpell(RaycastHit hitInfo) => StartCoroutine(ExecuteStoneLifting(hitInfo.point));

        private IEnumerator ExecuteStoneLifting(Vector3 position)
        {
            transform.position = position;
            yield return new WaitForSeconds(liftTime);
            
            var size = Physics.OverlapSphereNonAlloc(position + Vector3.up, 5f, _grenadeExplosionResults, MobLayer);
            for (int i = 0; i < size; i++)
            {
                var mob = _grenadeExplosionResults[i].GetComponent<MobBehaviour>();
                mob.AddReceivedEffects(new List<Effect>
                    { new InspirationEffect(), new StunEffect(1f.SecondsToTicks()) });
                mob.HitThisMob(100, BasicElement.None);
            }
            
            yield return new WaitForSeconds(lifeTime);
            Destroy(gameObject);
        }
    }
}