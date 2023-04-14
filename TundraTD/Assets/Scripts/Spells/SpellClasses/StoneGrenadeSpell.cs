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
        [SerializeField] private GameObject stone;
        [SerializeField] private float liftTime;
        [SerializeField] private float lifeTime;
        [SerializeField] private ParticleSystem blowParticle;
        
        private const int MobLayer = 1 << 8; 
        public override BasicElement Element => BasicElement.Earth | BasicElement.Air;

        public override void ExecuteSpell(RaycastHit hitInfo) => StartCoroutine(ExecuteStoneLifting(hitInfo.point));
        
        private IEnumerator ExecuteStoneLifting(Vector3 position)
        {
            var instantiatedStone = Instantiate(stone, position, Quaternion.identity, transform);

            var time = 0f;
            while (time < liftTime)
            {
                var lerpedY = Mathf.Lerp(0, 1, time / liftTime);
                instantiatedStone.transform.position += Vector3.up * lerpedY;
                time += Time.deltaTime;
                yield return null;
            }

            var stonePosition = instantiatedStone.transform.position;
            Instantiate(blowParticle, stonePosition, Quaternion.identity); // Destroy after executing using particle system clear behaviour
            var mobs = Physics.OverlapSphere(stonePosition, 5f, MobLayer);
            foreach (var overlappedMob in mobs)
            {
                var mob = overlappedMob.GetComponent<MobBehaviour>();
                mob.AddReceivedEffects(new List<Effect>
                    { new InspirationEffect(), new StunEffect(1f.SecondsToTicks()) });
                mob.HitThisMob(100, BasicElement.None, "Grenade");
            }

            yield return new WaitForSeconds(lifeTime);
            Destroy(gameObject);
        }
    }
}