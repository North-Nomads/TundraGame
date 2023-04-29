using Level;
using Mobs.MobsBehaviour;
using Mobs.MobEffects;
using System.Collections;
using System.Linq;
using UnityEngine;
namespace Spells
{
    public class FireStormSpell : MagicSpell
    {
        private const int MobsLayerMask = 1 << 8;

        [SerializeField] private float lifetime;
        [SerializeField] private float damage;
        [SerializeField] private float damageDelay;
        [SerializeField] private float burnDamage;
        [SerializeField] private float burnTime;
        private Collider[] mobs = new Collider[100];
        private CapsuleCollider interactionCollider;
        public override BasicElement Element => BasicElement.Fire | BasicElement.Air;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            transform.position = hitInfo.point;
            interactionCollider = GetComponent<CapsuleCollider>();
            StartCoroutine(MakeSpell());
        }
        private IEnumerator MakeSpell()
        {
            float time = 0;
            bool s = false;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
            while (time < lifetime)
            {
               
                /*int mobsAmount = Physics.OverlapSphereNonAlloc(transform.position, interactionCollider.radius, mobs, MobsLayerMask);
                for (int i = 0; i < mobsAmount; i++)
                {
                    var mob = mobs[i].GetComponent<MobBehaviour>();
                    mob.HitThisMob(damage, BasicElement.Fire);
                }*/
                
                RaycastHit _hit;
                if (Physics.Raycast(ray, out _hit, Mathf.Infinity))
                {
                    s = true;
                }
                if (s)
                {
                    Collider[] fa = Physics.OverlapSphere(_hit.point, interactionCollider.radius, MobsLayerMask);
                    foreach (var col in fa)
                    {
                        col.GetComponent<MobBehaviour>().MobModel.Rigidbody.AddForce((_hit.point - col.transform.position) * 1000);
                    }
                }
                yield return new WaitForSeconds(0.1f);
                time += damageDelay;
            }
            int amount = Physics.OverlapSphereNonAlloc(transform.position, interactionCollider.radius, mobs, MobsLayerMask);
            for (int i = 0; i < amount; i++)
            {
                var mob = mobs[i].GetComponent<MobBehaviour>();
                if (!mob.CurrentEffects.OfType<BurningEffect>().Any())
                    mob.AddSingleEffect(new BurningEffect(burnDamage, burnTime.SecondsToTicks()));
            }
            interactionCollider.enabled = false;
            DisableEmissionOnChildren();
            s = false;
            yield return new WaitForSeconds(5);
            Destroy(gameObject);
        }
    }
}