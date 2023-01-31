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
        [SerializeField] private float secondsUntilNextWave;
        [SerializeField] private float secondsBetweenMobSpawn;
        public int _mobAmountOnWave;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
                StartWavesOfMobs();
        }
        public void DecreaseMobsCountByOne()
        {
            _mobAmountOnWave--;
        }
        public void StartWavesOfMobs()
        {
            StartCoroutine(MakeWavesOfMobs());
        }


        IEnumerator MakeWavesOfMobs()
        {
            bool _firstWave = true;
            foreach (var mobWave in mobWaves)
            {
                yield return new WaitUntil(() => _mobAmountOnWave == 0);
                if (!_firstWave)
                    yield return new WaitForSeconds(secondsUntilNextWave);
                _firstWave = false;
                foreach (var mobProperty in mobWave.MobProperties)
                {
                    for (int i = 0; i < mobProperty.MobQuantity; i++)
                    {
                        yield return new WaitForSeconds(secondsBetweenMobSpawn);
                        _mobAmountOnWave++;
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