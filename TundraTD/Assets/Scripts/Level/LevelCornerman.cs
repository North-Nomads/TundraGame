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
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
                StartCoroutine(StartWavesLoop());
        }

        private IEnumerator StartWavesLoop()
        {
            StartCoroutine(new WaitUntil(() => mobPortals.All(x => x.IsInstantiated)));
            for (int i = 0; i < _maxWavesAmongPortals; i++)
            {
                foreach (var mobPortal in mobPortals)
                {
                    mobPortal.EquipNextWave();
                    StartCoroutine(StartMobSpawning(mobPortal));
                }
                yield return new WaitUntil(() => mobPortals.Sum(x => x.MobsLeftThisWave) == 0);
                
                waveStartTimer.gameObject.SetActive(true);
                for (int j = mobWaveDelay; j > 0; j--)
                {
                    waveStartTimer.text = j.ToString();
                    yield return new WaitForSeconds(1f);
                }
                waveStartTimer.gameObject.SetActive(false);
            }
            levelJudge.HandlePlayerVictory();
        }

        private IEnumerator StartMobSpawning(MobPortal portal)
        {
            for (int i = 0; i < portal.TotalWaveMobQuantity; i++)
            {
                yield return new WaitForSeconds(mobSpawnDelay);
                portal.SpawnNextMob();
            }
        }
    }
}