using System;
using System.Collections;
using City.Building.ElementPools;
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
                Debug.Log("Ram");
                var mobsAmount = Physics.OverlapBoxNonAlloc(floorHitMarker.transform.position, floorHitMarker.size,
                    _colliders, Quaternion.identity, 1 << 8);
                for (int i = 0; i < mobsAmount; i++)
                {
                    _colliders[i].GetComponent<MobBehaviour>().HitThisMob(hammerDamage, BasicElement.Earth);
                    Debug.Log("Hit the mob");
                }
                    
            }
        }

        private void OnValidate()
        {
            if (hammerCooldown < 0.01)
                hammerCooldown = 0.01f;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawCube(floorHitMarker.transform.position, floorHitMarker.size);
        }
    }
}