using System;
using System.Linq;
using Spells;
using UnityEngine;

namespace City.Building
{
    /// <summary>
    /// Orders slots to build towers
    /// </summary>
    public class Architect : MonoBehaviour
    {
        [SerializeField] private Transform canvasesParent;
        [SerializeField] private int influencePoints; // temporary SF
        [SerializeField] private CityGatesUI influencePointsHolder;
        [SerializeField] private TowerPlacementSlot[] placementSlots; 
        [SerializeField] private ElementalTower[] elementalTowerPrefabs;

        public Transform CanvasesParent => canvasesParent;

        private void Start()
        {
            influencePointsHolder.UpdateInfluencePointsText(influencePoints.ToString());
        }

        public void BuildNewTower(int slotID, BasicElement element)
        {
            var elementalTower = elementalTowerPrefabs.FirstOrDefault(x => x.TowerElement == element);
            if (elementalTower is null)
                throw new Exception("Tower or slot is null");
            if (influencePoints < elementalTower.TowerPurchasePrice)
                return;

            var placementSlot = placementSlots.FirstOrDefault(x => x.SlotID == slotID);
            if (placementSlot is null)
                throw new Exception("Tower or slot is null");
            if (placementSlot.IsOccupied)
                return;
            
            placementSlot.BuildElementalTowerOnThisSlot(elementalTower);
            influencePoints -= elementalTower.TowerPurchasePrice;
            influencePointsHolder.UpdateInfluencePointsText(influencePoints.ToString());
        }
    }
}