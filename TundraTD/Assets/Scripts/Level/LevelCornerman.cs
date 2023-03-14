using Mobs;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    /// <summary>
    /// Takes control over time related to portals
    /// </summary>
    public class LevelCornerman : MonoBehaviour
    {
        private static bool _isInPlayMode;
        public static bool IsInPlayMode => _isInPlayMode;
        
        [SerializeField] private MobPortal[] mobPortals;
        [SerializeField] private LevelJudge levelJudge;
        [SerializeField] private Text waveStartTimer;
        [SerializeField] private float mobSpawnDelay;
        [SerializeField] private int mobWaveDelay;
        private int _maxWavesAmongPortals;
        private AudioSource _soundEffect;

        public static bool IsInWaveMode { get; private set; }
        
        private void Start()
        {
            IsInWaveMode = false;
            waveStartTimer.gameObject.SetActive(false);
            _soundEffect = GetComponent<AudioSource>();
            _soundEffect.volume = GameParameters.EffectsVolumeModifier;
            StartCoroutine(WaitPortalsInstantiation());
        }

        private IEnumerator WaitPortalsInstantiation()
        {
            yield return new WaitUntil(() => mobPortals.All(x => x.IsInstantiated));
            _maxWavesAmongPortals = mobPortals.Max(x => x.WavesAmount);
            
            foreach (var mobPortal in mobPortals)
                mobPortal.MobPool.EndMobInstantiation();
        }

        private IEnumerator StartWavesLoop()
        {
            // Main loop that handles beginning and ending of all waves

            // Wait for all portals instantiate themselves
            yield return new WaitUntil(() => mobPortals.All(x => x.IsInstantiated));

            for (int i = 0; i < _maxWavesAmongPortals; i++)
            {
                IsInWaveMode = true;
                // Start mob spawning IEnumerator for each of the portals and wait for all mobs to die
                foreach (var mobPortal in mobPortals)
                {
                    mobPortal.EquipNextWave();
                    yield return StartMobSpawning(mobPortal);
                }

                yield return new WaitUntil(() => mobPortals.Count(x => !x.MobPool.AreAllMobDead()) == 0);
                IsInWaveMode = false;
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
                if (j == 2) _soundEffect.Play();
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
            _isInPlayMode = true;
        }
    }
}