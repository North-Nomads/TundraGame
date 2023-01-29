using System;
using System.Collections;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Mobs
{
    public class MobPortal : MonoBehaviour
    {
        [SerializeField] private Transform gates;
        [SerializeField] private MobWave[] mobWaves;
        [SerializeField] private Transform mobSpawner;
        public int _allMobs;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
                SpawnNextMob();
        }
        public void DecreaseMobsCountByOne()
        {
            _allMobs -= 1;
        }
        public void SpawnNextMob()
        {
            StartCoroutine(IntervalsBetweenWaves());
        }


        IEnumerator IntervalsBetweenWaves()
        {
            bool _firstWave = true;
            foreach (var mobWave in mobWaves)
            {
                while (_allMobs != 0)
                {
                    yield return new WaitForSeconds(1);
                }
                if (!_firstWave)
                {
                    yield return new WaitForSeconds(5);
                }
                _firstWave = false;
                foreach (var mobProperty in mobWave.MobProperties)
                {
                    for (int i = 0; i < mobProperty.MobQuantity; i++)
                    {
                        yield return new WaitForSeconds(1.5f);
                        _allMobs += 1;
                        var mob = Instantiate(mobProperty.Mob, mobSpawner.position, Quaternion.identity, mobSpawner.transform);
                        mob.ExecuteOnMobSpawn(gates, this);
                    }
                }
            }   
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