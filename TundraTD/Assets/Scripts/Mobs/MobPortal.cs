using City;
using Mobs.MobsBehaviour;
using System;
using System.Collections.Generic;
using System.Linq;
using Level;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mobs
{
    /// <summary>
    /// Spawns mobs and manages waves
    /// </summary>
    public class MobPortal : MonoBehaviour
    {
        [SerializeField] private PortalInfoCanvas infoPanel;
        [SerializeField] private CityGates gates;
        [SerializeField] private Transform mobSpawner;
        [SerializeField] private MobWave[] mobWaves;

        private MobWave _currentMobWave;
        private List<MobBehaviour> _allWaveMobs;
        private int _currentMobWaveIndex;
        private int _currentMobIndex;

        public int WavesAmount { get; private set; }
        public int MobsLeftThisWave { get; private set; }
        private int MobsTotalCountOnWave { get; set; }
        public bool IsInstantiated { get; private set; }

        public int TotalWaveMobQuantity => _currentMobWave.MobProperties.Sum(x => x.MobQuantity);

        private void Start()
        {
            _currentMobWaveIndex = 0;
            _currentMobWave = mobWaves[_currentMobWaveIndex];
            _allWaveMobs = new List<MobBehaviour>();
            WavesAmount = mobWaves.Length;
            IsInstantiated = true;
            infoPanel.SendNewWaveMobs(_currentMobWave);
        }

        private void OnMouseDown()
        {
            if (LevelCornerman.IsInWaveMode || EventSystem.current.IsPointerOverGameObject())
                return;
            
            infoPanel.gameObject.SetActive(true);
        }

        public void EquipNextWave()
        {
            if (_currentMobWaveIndex >= mobWaves.Length)
                return;
            
            infoPanel.SendNewWaveMobs(_currentMobWave);
            
            _allWaveMobs.Clear();
            _currentMobIndex = 0;

            foreach (var property in _currentMobWave.MobProperties)
                for (int i = 0; i < property.MobQuantity; i++)
                    _allWaveMobs.Add(property.Mob);

            MobsLeftThisWave = _allWaveMobs.Count;
            MobsTotalCountOnWave = MobsLeftThisWave;
        }

        public void OnWaveEnded()
        {
            _currentMobWaveIndex++;
            if (_currentMobWaveIndex >= mobWaves.Length)
                return;
            _currentMobWave = mobWaves[_currentMobWaveIndex];
        }

        public void SpawnNextMob()
        {
            if (_currentMobIndex >= MobsTotalCountOnWave)
                return;

            var mobObject = _allWaveMobs[_currentMobIndex];
            var mob = Instantiate(
                mobObject,
                mobSpawner.position,
                Quaternion.identity,
                mobSpawner.transform
            );
            mob.ExecuteOnMobSpawn(gates.transform, this);
            mob.OnMobDied += (_, __) => NotifyPortalOnMobDeath(mob);
            _currentMobIndex++;
        }

        private void NotifyPortalOnMobDeath(MobBehaviour mob)
        {
            MobsLeftThisWave--;
        }

        [Serializable]
        public class MobWave
        {
            [SerializeField]
            private MobProperty[] mobProperties;

            public MobProperty[] MobProperties => mobProperties;
        }

        [Serializable]
        public class MobProperty
        {
            [SerializeField] private MobBehaviour mob;
            [SerializeField] private int mobQuantity;

            public MobBehaviour Mob => mob;
            public int MobQuantity => mobQuantity;
        }
    }
}
