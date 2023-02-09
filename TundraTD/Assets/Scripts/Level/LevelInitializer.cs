using City;
using City.Building;
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
        [SerializeField] private GameObject[] spellPrefabs;

        private void Start()
        {
            pauseMode.SetPause(false);
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