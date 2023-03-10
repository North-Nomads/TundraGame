using City;
using Mobs.MobsBehaviour;
using System;
using System.Collections.Generic;
using System.Linq;
using Level;
using ModulesUI.MobPortal;
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
        private List<MobBehaviour> _waveMobs;
        private int _currentMobWaveIndex;
        private int _currentMobIndex;

        public int WavesAmount { get; private set; }
        //public int MobsLeftThisWave { get; private set; }
        private int MobsTotalCountOnWave { get; set; }
        public bool IsInstantiated { get; private set; }

        public int TotalWaveMobQuantity => _currentMobWave.MobProperties.Sum(x => x.MobQuantity);

        private void Start()
        {
            _currentMobWaveIndex = 0;
            _currentMobWave = mobWaves[_currentMobWaveIndex];

            foreach (var mobWave in mobWaves)
            {
                foreach (var mobProperty in mobWave.MobProperties)
                {
                    for (int i = 0; i < mobProperty.MobQuantity; i++)
                    {
                        var mob = Instantiate(mobProperty.Mob);
                        mob.gameObject.SetActive(false);
                        MobPool.InstantiateMob(mob);
                    }
                }
            }

            WavesAmount = mobWaves.Length;
            IsInstantiated = true;
            infoPanel.LoadMobsInPortalPanel(_currentMobWave);
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
            
            _currentMobIndex = 0;
            _waveMobs = MobPool.GetMobsList(_currentMobWave);
            MobsTotalCountOnWave = _waveMobs.Count;
        }

        public void OnWaveEnded()
        {
            _currentMobWaveIndex++;
            if (_currentMobWaveIndex >= mobWaves.Length)
                return;
            _currentMobWave = mobWaves[_currentMobWaveIndex];
            infoPanel.LoadMobsInPortalPanel(_currentMobWave);
        }

        public void SpawnNextMob()
        {
            if (_currentMobIndex >= MobsTotalCountOnWave)
                return;

            var mob = _waveMobs[_currentMobIndex];
            mob.RespawnMobFromPool(mobSpawner);
            mob.ExecuteOnMobSpawn(gates.transform, this);
            _currentMobIndex++;
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
