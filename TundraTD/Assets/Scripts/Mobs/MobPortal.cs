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
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
                SpawnNextMob();
        }

        public void SpawnNextMob()
        {
            IEnumerator Waits()
            {
                bool _isFirstWave = true;
                foreach (var mobWave in mobWaves)
                {
                    if (!_isFirstWave)
                    {
                        yield return new WaitForSeconds(10);
                    }
                    else{
                        _isFirstWave = false;
                    }
                    foreach (var mobProperty in mobWave.MobProperties)
                    {                        
                        for (int i = 0; i < mobProperty.MobQuantity; i++)
                        {
                            yield return new WaitForSeconds(1.5f);
                            var mob = Instantiate(mobProperty.Mob, mobSpawner.position, Quaternion.identity, mobSpawner.transform);
                            mob.ExecuteOnMobSpawn(gates);                    
                        }
                    }      
                }          
            }
            StartCoroutine(Waits());
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