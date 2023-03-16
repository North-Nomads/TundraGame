using System;
using System.Collections.Generic;
using System.Linq;
using City;
using City.Building;
using City.Building.ElementPools;
using ModulesUI;
using ModulesUI.MagicScreen;
using ModulesUI.Pause;
using ModulesUI.PlayerHUD;
using Spells;
using UnityEngine;


namespace Level
{
    /// <summary>
    /// Handles the order of loading and initializing objects on level
    /// </summary>
    public class LevelInitializer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [Tooltip("An object that holds pause button and panel")]
        [SerializeField] private PauseParent pauseParent;
        [SerializeField] private int minWaveAward;
        [SerializeField] private int maxWaveAward;
        [SerializeField] private Transform canvasesParent;
        [SerializeField] private CityGatesUI influencePointsHolder;
        [SerializeField] private TowerPlacementSlot[] placementSlots;
        [SerializeField] private ElementalTower[] elementalTowerPrefabs;
        [SerializeField] private MagicSpell[] spellInitializers;
        [SerializeField] private MobPool[] mobPools;
        
        
        private void Start()
        {
            if (placementSlots.Length == 0)
                throw new NullReferenceException("No slots were assigned");

            InitializePauseMode();
            InitializeArchitectValues();
            ResetMagicPools();
            PlayerDeck.DeckElements.Clear();
            ResetMobPools();
        }

        private void ResetMobPools()
        {
            foreach (var mobPool in mobPools)
            {
                mobPool.ResetValues();
            }
        }

        private void ResetMagicPools()
        {
            // TODO: add other pools
            var pools = new List<Type> { typeof(EarthPool), typeof(FirePool), typeof(WaterPool) };
            foreach (var prop in pools.SelectMany(pool => pool.GetProperties()))
                prop.SetValue(null, false);
        }

        private void InitializePauseMode()
        {
            PauseMode.ResetSubscribers();
            pauseParent.SubscribeToPauseMode();
            pauseParent.PauseCanvas.SetImmortalAudioSource(audioSource);
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