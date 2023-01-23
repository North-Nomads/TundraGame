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
        [SerializeField] private Transform canvasesParent;
        [SerializeField] private CityGatesUI influencePointsHolder;
        [SerializeField] private TowerPlacementSlot[] placementSlots; 
        [SerializeField] private ElementalTower[] elementalTowerPrefabs;
        [SerializeField] private GameObject[] spellPrefabs;

        private void Start()
        {
            Architect.ElementalTowerPrefabs = elementalTowerPrefabs;
            Architect.InfluencePointsHolder = influencePointsHolder;
            Architect.CanvasesParent = canvasesParent;
            Architect.PlacementSlots = placementSlots;
            Grimoire.SpellPrefabs = spellPrefabs;
            
            // DEBUG: Temporary giving 100 points
            Architect.DEBUG_GetStartPoints();
        }
    }
}