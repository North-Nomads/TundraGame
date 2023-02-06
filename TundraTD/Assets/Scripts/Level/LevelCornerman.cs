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
            // Main loop that handles beginning and ending of all waves
            
            // Wait for all portals instantiate themselves
            yield return new WaitUntil(() => mobPortals.All(x => x.IsInstantiated));
            
            for (int i = 0; i < _maxWavesAmongPortals; i++)
            {
                // Start mob spawning IEnumerator for each of the portals and wait for all mobs to die 
                foreach (var mobPortal in mobPortals)
                {
                    mobPortal.EquipNextWave();
                    yield return StartMobSpawning(mobPortal);
                }
                yield return new WaitUntil(() => mobPortals.Sum(x => x.MobsLeftThisWave) == 0);
                
                // Handle the ending of the wave (from all portals)
                foreach (var mobPortal in mobPortals) 
                    mobPortal.OnWaveEnded();
                
                if (i != _maxWavesAmongPortals - 1)
                    yield return ShowTimerBetweenWaves();
            }
            // After all waves were completed 
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
            // Spawn all mobs with a short delay
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