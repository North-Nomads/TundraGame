using System;
using Mobs;
using UnityEngine;

namespace City
{
    public class CityGates : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (string.Equals(other.tag, "Mob"))
                other.GetComponent<MobBehaviour>().KillThisMob();;
        }
    } 
}
