using City;
using City.Building;
using UnityEngine;

namespace Level
{
    /// <summary>
    /// Handles the order of loading and initializing objects on level
    /// </summary>
    public class LevelInitializer : MonoBehaviour
    {
        [SerializeField] private int minWaveAward;
        [SerializeField] private int maxWaveAward;
        [SerializeField] private Transform canvasesParent;
        [SerializeField] private CityGatesUI influencePointsHolder;
        [SerializeField] private TowerPlacementSlot[] placementSlots; 
        [SerializeField] private ElementalTower[] elementalTowerPrefabs;

        private void Start()
        {
            InitializeArchitectValues();
        }

        private void InitializeArchitectValues()
        {
            Architect.ElementalTowerPrefabs = elementalTowerPrefabs;
            Architect.InfluencePointsHolder = influencePointsHolder;
            Architect.CanvasesParent = canvasesParent;
            Architect.PlacementSlots = placementSlots;
            Architect.MinPointsAward = minWaveAward;
            Architect.MaxPointsAward = maxWaveAward;

            // DEBUG: Temporary giving 100 points
            Architect.DEBUG_GetStartPoints();
        }
    }
}