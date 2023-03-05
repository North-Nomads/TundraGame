using System;
using City;
using City.Building;
using Spells;
using UI.Pause;
using UnityEngine;

namespace Level
{
    /// <summary>
    /// Handles the order of loading and initializing objects on level
    /// </summary>
    public class LevelInitializer : MonoBehaviour
    {
        [Tooltip("An object that holds pause button and panel")]
        [SerializeField] private PauseParent pauseParent;
        [SerializeField] private int minWaveAward;
        [SerializeField] private int maxWaveAward;
        [SerializeField] private Transform canvasesParent;
        [SerializeField] private CityGatesUI influencePointsHolder;
        [SerializeField] private TowerPlacementSlot[] placementSlots;
        [SerializeField] private ElementalTower[] elementalTowerPrefabs;
        [SerializeField] private GameObject[] spellPrefabs;

        private void Start()
        {
            if (placementSlots.Length == 0)
                throw new NullReferenceException("No slots were assigned");

            PauseMode.ResetSubscribers();
            pauseParent.SubscribeToPauseMode();
            InitializeArchitectValues();
        }

        private void InitializeArchitectValues()
        {
            Architect.ElementalTowerPrefabs = elementalTowerPrefabs;
            Architect.InfluencePointsHolder = influencePointsHolder;
            Architect.CanvasesHierarchyParent = canvasesParent;
            Architect.PlacementSlots = placementSlots;
            Architect.WaveCompletionMinInfluencePointsAward = minWaveAward;
            Architect.WaveCompletionMaxInfluencePointsAward = maxWaveAward;

            Grimoire.SpellPrefabs = spellPrefabs;

            // DEBUG: Temporary giving 100 points
            Architect.DEBUG_GetStartPoints();
        }
    }
}