using System;
using System.Collections.Generic;
using System.Linq;
using City;
using Mobs.MobsBehaviour;
using UnityEngine;
using UnityEngine.UI;

namespace Mobs
{
    /// <summary>
    /// Spawns mobs and manages waves
    /// </summary>
    public class MobPortal : MonoBehaviour
    {
        public PortalInfoCanvas InfoPanel;

        [SerializeField]
        private CityGates gates;

        [SerializeField]
        private Transform mobSpawner;

        [SerializeField]
        private MobWave[] mobWaves;

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
            _allWaveMobs = new List<MobBehaviour>();
            WavesAmount = mobWaves.Length;
            IsInstantiated = true;
            Debug.Log($"WavesAmount: {WavesAmount}");
        }

        private void OnMouseDown()
        {
            Sprite[] mBox = new Sprite[8];
            var count = 0;
            foreach (var mob in _currentMobWave.MobProperties)
            {
                mBox[count] = mob.Mob.MobModel.MobSprite;
                count++;
            }
            InfoPanel.gameObject.SetActive(true);
            InfoPanel.LoadImagesInCards(mBox);
        }

        public void EquipNextWave()
        {
            if (_currentMobWaveIndex >= mobWaves.Length)
                return;
            _allWaveMobs.Clear();
            _currentMobIndex = 0;

            _currentMobWave = mobWaves[_currentMobWaveIndex];
            foreach (var property in _currentMobWave.MobProperties)
                for (int i = 0; i < property.MobQuantity; i++)
                    _allWaveMobs.Add(property.Mob);

            MobsLeftThisWave = _allWaveMobs.Count;
            MobsTotalCountOnWave = MobsLeftThisWave;

            _currentMobWaveIndex++;
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

            _currentMobIndex++;
        }

        public void NotifyPortalOnMobDeath()
        {
            MobsLeftThisWave--;
        }

        [Serializable]
        private class MobWave
        {
            [SerializeField]
            private MobProperty[] mobProperties;

            public MobProperty[] MobProperties => mobProperties;
        }

        [Serializable]
        private class MobProperty
        {
            [SerializeField]
            private MobBehaviour mob;

            [SerializeField]
            private int mobQuantity;

            public MobBehaviour Mob => mob;
            public int MobQuantity => mobQuantity;
        }
    }
}
