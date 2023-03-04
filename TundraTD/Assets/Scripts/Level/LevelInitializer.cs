using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using City;
using City.Building;
using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace Level
{
    /// <summary>
    /// Handles the order of loading and initializing objects on level
    /// </summary>
    public class LevelInitializer : MonoBehaviour
    {
        [SerializeField] private PauseMode pauseMode;
        [SerializeField] private int minWaveAward;
        [SerializeField] private int maxWaveAward;
        [SerializeField] private Transform canvasesParent;
        [SerializeField] private CityGatesUI influencePointsHolder;
        [SerializeField] private TowerPlacementSlot[] placementSlots;
        [SerializeField] private ElementalTower[] elementalTowerPrefabs;
        [SerializeField] private MagicSpell[] spellInitializers;

        private void Start()
        {
            if (placementSlots.Length == 0)
                throw new NullReferenceException("No slots were assigned");
            
            pauseMode.SetPause(false);
            InitializeArchitectValues();
            ResetMagicPools();
        }

        private void ResetMagicPools()
        {
            // TODO: add other pools
            var pools = new List<Type> { typeof(EarthPool), typeof(FirePool), typeof(WaterPool) };
            foreach (var prop in pools.SelectMany(pool => pool.GetProperties()))
                prop.SetValue(null, false);
        }

        private void InitializeArchitectValues()
        {
            Architect.ElementalTowerPrefabs = elementalTowerPrefabs;
            Architect.InfluencePointsHolder = influencePointsHolder;
            Architect.CanvasesHierarchyParent = canvasesParent;
            Architect.PlacementSlots = placementSlots;
            Architect.WaveCompletionMinInfluencePointsAward = minWaveAward;
            Architect.WaveCompletionMaxInfluencePointsAward = maxWaveAward;
            Grimoire.SpellInitializers = spellInitializers;
            
            // DEBUG: Temporary giving 100 points
            Architect.DEBUG_GetStartPoints();
        }
    }
}