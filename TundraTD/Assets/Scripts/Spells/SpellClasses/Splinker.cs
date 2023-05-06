using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Level;
using Mobs.MobEffects;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Spells.SpellClasses
{
    public class Splinker : MonoBehaviour
    {
        [SerializeField] private SphereCollider mainCollider;
       
        private int _mobsAmount;
        private readonly MobBehaviour[] _mobsInSpell = new MobBehaviour[100];

        private float SprayRadius => 6f;
        private float WetDuration => 6f;

        private void Start()
        {
            mainCollider = GetComponent<SphereCollider>();
            mainCollider.radius = SprayRadius;
        }


        //public override BasicElement Element => BasicElement.Water;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Mob"))
            {
                var mob = other.GetComponent<MobBehaviour>();
                _mobsInSpell[_mobsAmount] = mob;
                _mobsAmount++;
                mob.AddReceivedEffects(new List<Effect>
                {
                    new WetEffect(WetDuration.SecondsToTicks()),
                    new InspirationEffect()
                });
            }
        }      


        private void OnTriggerExit(Collider other)
        {
            // If the leaving object is mob -> remove it from the list and remove slowness from him
            if (other.CompareTag("Mob"))
            {
                _mobsInSpell[_mobsAmount] = null;
                _mobsAmount--;
                var mob = other.GetComponent<MobBehaviour>();
                mob.RemoveFilteredEffects(x => x is WetEffect);
            }
        }
    }
}

