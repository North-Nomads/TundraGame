using System;
using System.Collections;
using City;
using City.Building;
using Level;
using Mobs.MobsBehaviour;
using UnityEngine;

namespace Mobs
{
    public class MobPortal : MonoBehaviour
    { 
        [SerializeField] private LevelJudge levelJudge;
        [SerializeField] private CityGates gates;
        [SerializeField] private MobWave[] mobWaves;
        [SerializeField] private Transform mobSpawner;
        [SerializeField] private float secondsUntilNextWave;
        [SerializeField] private float secondsBetweenMobSpawn;
        [SerializeField] private MobWaveBar mobWaveBar;
        private bool _firstWave = true;
        private int _mobAmountOnWave;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
                StartCoroutine(StartWavesLoop());
        }

        private IEnumerator StartWavesLoop()
        {
            foreach (var mobWave in mobWaves)
            {
                if (!_firstWave)
                    yield return new WaitForSeconds(secondsUntilNextWave);
                _firstWave = false;
                
                var totalScore = 0f;
                foreach (var mobProperty in mobWave.MobProperties)
                {
                    for (int i = 0; i < mobProperty.MobQuantity; i++)
                    {
                        yield return new WaitForSeconds(secondsBetweenMobSpawn);
                        _mobAmountOnWave++;
                        var mob = Instantiate(mobProperty.Mob, mobSpawner.position, Quaternion.identity,
                            mobSpawner.transform);
                        mob.ExecuteOnMobSpawn(gates.transform, this);
                        totalScore += mob.MobModel.MobWaveWeight;
                    }
                }
                
                if (totalScore <= 0)
                    throw new Exception($"TotalScore is unacceptable: {totalScore}");
                mobWaveBar.ResetValuesOnWaveStarts(totalScore);
                
                yield return new WaitUntil(() => _mobAmountOnWave == 0);
                gates.HandleWaveEnding();
            }
            levelJudge.HandlePlayerVictory();
        }

        public void NotifyPortalOnMobDeath(MobBehaviour mob)
        {
            mobWaveBar.DecreaseCurrentMobScore(mob.MobModel.MobWaveWeight);
        }
        
        public void DecreaseMobsCountByOne()
        {
            _mobAmountOnWave--;
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