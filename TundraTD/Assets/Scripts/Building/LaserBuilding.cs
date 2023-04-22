using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Building
{
    public class Laser : MonoBehaviour
    {
        [SerializeField] private MeshRenderer laserMesh;
        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private float cooldownLaser;
        
        private float distanceForMeteor;
        private bool meteorInLaserRange = false;

        void Start()
        {
            
        }
    }
}
