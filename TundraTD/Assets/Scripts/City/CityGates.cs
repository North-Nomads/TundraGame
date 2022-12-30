using System;
using Mobs;
using UnityEngine;

namespace City
{
    public class CityGates : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.tag);
            if (string.Equals(other.tag, "Mob"))
            {
                var mb = other.GetComponent<MobBehaviour>();
                mb.KillThisMob();
            }
        }
    } 
}
