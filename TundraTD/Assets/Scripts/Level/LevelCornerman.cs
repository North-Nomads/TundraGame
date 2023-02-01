using System;
using System.Collections;
using System.Linq;
using Mobs;
using UnityEngine;

namespace Level
{
    public class LevelCornerman : MonoBehaviour
    {
        [SerializeField] private LevelJudge levelJudge;
        [SerializeField] private MobPortal[] mobPortals;
        [SerializeField] private float mobWaveDelay;
        [SerializeField] private float mobSpawnDelay;
        private int _maxWavesAmongPortals;

        private void Start()
        {
            StartCoroutine(WaitPortalsInstantiation());
        }

        private IEnumerator WaitPortalsInstantiation()
        {
            yield return new WaitUntil(() => mobPortals.All(x => x.IsInstantiated));
            _maxWavesAmongPortals = mobPortals.Max(x => x.WavesAmount);
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
                StartCoroutine(StartWavesLoop());
        }

        private IEnumerator StartWavesLoop()
        {
            Debug.Log(_maxWavesAmongPortals);
            for (int i = 0; i < _maxWavesAmongPortals; i++)
            {
                Debug.Log($"Wave {i} started");
                foreach (var mobPortal in mobPortals)
                {
                    mobPortal.EquipNextWave();
                    StartCoroutine(StartMobSpawning(mobPortal));
                }
                print("Print1");
                yield return new WaitUntil(() => mobPortals.Sum(x => x.MobsLeftThisWave) == 0);
                print("Print2");
                yield return new WaitForSeconds(mobWaveDelay);
                print("Print3");
            }
            Debug.Log("Finished loop");
            levelJudge.HandlePlayerVictory();
        }

        private IEnumerator StartMobSpawning(MobPortal portal)
        {
            Debug.Log(portal.TotalWaveMobQuantity);
            for (int i = 0; i < portal.TotalWaveMobQuantity; i++)
            {
                yield return new WaitForSeconds(mobSpawnDelay);
                portal.SpawnNextMob();
            }
        }

        /*private IEnumerator StartWavesLoop1(MobPortal mobPortal)
        {
            
            foreach (var mobWave in mobWaves)
            {
                if (!_isFirstWave)
                    yield return new WaitForSeconds(mobWaveDelay);
                _isFirstWave = false;

                yield return SpawnWaveMobs(mobWave);
                var waveMobs = mobWave.MobProperties;
                _mobAmountOnWave += waveMobs.Sum(x => x.MobQuantity);
                var totalScore = waveMobs.Sum(x => x.Mob.MobModel.MobWaveWeight);
                
                if (totalScore <= 0)
                    throw new Exception($"TotalScore is unacceptable: {totalScore}");
                mobWaveBar.ResetValuesOnWaveStarts(totalScore);
                
                yield return new WaitUntil(() => _mobAmountOnWave == 0);
                gates.HandleWaveEnding();
            }
            levelJudge.HandlePlayerVictory();
        }*/
    }
}