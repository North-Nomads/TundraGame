using Mobs.MobsBehaviour;
using System;
using System.Collections.Generic;
using System.Linq;
using Level;
using ModulesUI;
using ModulesUI.MobPortal;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mobs
{
    /// <summary>
    /// Spawns mobs and manages waves
    /// </summary>
    public class MobPortal : MonoBehaviour, IPointerClickHandler
    {
        [Header("Navigation")] 
        [SerializeField] private Transform routeParent;
        [Header("Other")]
        [SerializeField] private MobPool mobPool;
        [SerializeField] private PortalInfoCanvas infoPanel;
        [SerializeField] private MobWave[] mobWaves;

        private MobWave _currentMobWave;
        private List<MobBehaviour> _waveMobs;
        private int _currentMobWaveIndex;
        private int _currentMobIndex;

        public int WavesAmount { get; private set; }
        //public int MobsLeftThisWave { get; private set; }
        private int MobsTotalCountOnWave { get; set; }
        public bool IsInstantiated { get; private set; }

        public MobPool MobPool => mobPool;
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
                        mobPool.InstantiateMob(mob);
                    }
                }
            }

            WavesAmount = mobWaves.Length;
            IsInstantiated = true;
            infoPanel.LoadWaveInPanel(_currentMobWave);
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (LevelCornerman.IsInWaveMode)
                return;
            
            UIToggle.TryOpenCanvas(infoPanel);
        }
        
        public void EquipNextWave()
        {
            if (_currentMobWaveIndex >= mobWaves.Length)
                return;
            
            _currentMobIndex = 0;
            _waveMobs = mobPool.GetMobsList(_currentMobWave);
            MobsTotalCountOnWave = _waveMobs.Count;
        }

        public void OnWaveEnded()
        {
            _currentMobWaveIndex++;
            if (_currentMobWaveIndex >= mobWaves.Length)
                return;
            _currentMobWave = mobWaves[_currentMobWaveIndex];
            infoPanel.LoadWaveInPanel(_currentMobWave);
        }

        public void SpawnNextMob()
        {
            if (_currentMobIndex >= MobsTotalCountOnWave)
                return;

            var mob = _waveMobs[_currentMobIndex];
            mob.RespawnMobFromPool(mobPool.transform.position, routeParent.GetComponentsInChildren<Transform>());
            mob.ExecuteOnMobSpawn(this);
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
