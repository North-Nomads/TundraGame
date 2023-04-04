using System;
using System.Collections;
using Mobs.MobsBehaviour;
using UnityEditor;
using UnityEngine;


namespace Spells.SpellClasses.EarthSpell
{
    public class SpikesHammerStone : MonoBehaviour
    {
        [SerializeField] private float hammerCooldown;
        [SerializeField] private float hammerDamage;
        [SerializeField] private BoxCollider floorHitMarker;
        private readonly Collider[] _colliders = new Collider[10];

        public void BeginHammeringCoroutine()
        {
            StartCoroutine(StartHammeringLoop());
        }

        private IEnumerator StartHammeringLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(hammerCooldown);
                var mobsAmount = Physics.OverlapBoxNonAlloc(floorHitMarker.transform.position, floorHitMarker.size,
                    _colliders, Quaternion.identity, 1 << 8);
                for (int i = 0; i < mobsAmount; i++)
                    _colliders[i].GetComponent<MobBehaviour>().HitThisMob(hammerDamage, BasicElement.Earth, "EarthMods.Hammer");
            }
        }

        private void OnValidate()
        {
            if (hammerCooldown < 0.01)
                hammerCooldown = 0.01f;
        }
    }
}