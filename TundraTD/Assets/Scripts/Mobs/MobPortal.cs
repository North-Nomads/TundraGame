using System;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Mobs
{
    public class MobPortal : MonoBehaviour
    {
        [SerializeField] private Transform gates;
        [SerializeField] private MobWave[] mobWaves;
        [SerializeField] private Transform mobSpawner;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
                SpawnNextMob();
        }

        public void SpawnNextMob()
        {
            var mob = Instantiate(mobWaves[0].MobProperties[0].Mob, mobSpawner.position, Quaternion.identity, mobSpawner.transform);
            mob.ExecuteOnMobSpawn(gates);
        }
        
        [Serializable]
        class MobWave
        {
            [SerializeField] private MobProperty[] mobProperties;

            public MobProperty[] MobProperties => mobProperties;
        }

        [Serializable]
        class MobProperty
        {
            [SerializeField] private MobBehaviour mob;
            [SerializeField] private int mobQuantity;

            public MobBehaviour Mob => mob;
            public int MobQuantity => mobQuantity;
        }
    }
}