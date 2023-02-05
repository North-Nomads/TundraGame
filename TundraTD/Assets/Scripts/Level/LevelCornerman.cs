using System.Collections;
using System.Linq;
using Mobs;
using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    /// <summary>
    /// Takes control over time related to portals
    /// </summary>
    public class LevelCornerman : MonoBehaviour
    {
        [SerializeField] private MobPortal[] mobPortals;
        [SerializeField] private LevelJudge levelJudge;
        [SerializeField] private Text waveStartTimer; 
        [SerializeField] private float mobSpawnDelay;
        [SerializeField] private int mobWaveDelay;
        private int _maxWavesAmongPortals;

        private void Start()
        {
            waveStartTimer.gameObject.SetActive(false);
            StartCoroutine(WaitPortalsInstantiation());
        }

        private IEnumerator WaitPortalsInstantiation()
        {
            yield return new WaitUntil(() => mobPortals.All(x => x.IsInstantiated));
            _maxWavesAmongPortals = mobPortals.Max(x => x.WavesAmount);
        }
        
        private IEnumerator StartWavesLoop()
        {
            yield return new WaitUntil(() => mobPortals.All(x => x.IsInstantiated));
            
            for (int i = 0; i < _maxWavesAmongPortals; i++)
            {
                foreach (var mobPortal in mobPortals)
                {
                    mobPortal.EquipNextWave();
                    yield return StartMobSpawning(mobPortal);
                }
                yield return new WaitUntil(() => mobPortals.Sum(x => x.MobsLeftThisWave) == 0);

                if (i != _maxWavesAmongPortals - 1)
                    yield return ShowTimerBetweenWaves();
            }
            levelJudge.HandlePlayerVictory();
        }

        private IEnumerator ShowTimerBetweenWaves()
        {
            waveStartTimer.gameObject.SetActive(true);
            for (int j = mobWaveDelay; j > 0; j--)
            {
                waveStartTimer.text = j.ToString();
                yield return new WaitForSeconds(1f);
            }
            waveStartTimer.gameObject.SetActive(false);
        }

        private IEnumerator StartMobSpawning(MobPortal portal)
        {
            for (int i = 0; i < portal.TotalWaveMobQuantity; i++)
            {
                yield return new WaitForSeconds(mobSpawnDelay);
                portal.SpawnNextMob();
            }
        }
        
        public void StartFirstWave()
        {
            StartCoroutine(StartWavesLoop());
        }
    }
}