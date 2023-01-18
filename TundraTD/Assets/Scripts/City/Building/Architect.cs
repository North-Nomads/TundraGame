using System;
using System.Linq;
using City.Building.Upgrades;
using Spells;
using UnityEngine;

namespace City.Building
{
    /// <summary>
    /// Manages the transaction to buy towers and upgrades
    /// </summary>
    public static class Architect
    {
        private static int _influencePoints;
        
        public static Transform CanvasesParent { get; set; }
        public static CityGatesUI InfluencePointsHolder { get; set; }
        public static TowerPlacementSlot[] PlacementSlots { get; set; } 
        public static ElementalTower[] ElementalTowerPrefabs { get; set; }
        private static int InfluencePoints
        {
            get => _influencePoints;
            set
            {
                if (value < 0)
                    throw new Exception("Influence Points now below zero");
                InfluencePointsHolder.UpdateInfluencePointsText(value.ToString());
                _influencePoints = value;  
            } 
        }

        public static void BuildNewTower(int slotID, BasicElement element)
        {
            var elementalTower = ElementalTowerPrefabs.FirstOrDefault(x => x.TowerElement == element);
            if (elementalTower is null)
                throw new Exception("Tower or slot is null");
            if (InfluencePoints < elementalTower.TowerPurchasePrice)
                return;

            var placementSlot = PlacementSlots.FirstOrDefault(x => x.SlotID == slotID);
            if (placementSlot is null)
                throw new Exception("Tower or slot is null");
            if (placementSlot.IsOccupied)
                return;
            
            placementSlot.BuildElementalTowerOnThisSlot(elementalTower);
            InfluencePoints -= elementalTower.TowerPurchasePrice;
            InfluencePointsHolder.UpdateInfluencePointsText(InfluencePoints.ToString());
        }

        public static bool CanUpgradeBeBought(IUpgrade upgrade)
        {
            Debug.Log("Upgrade");
            return InfluencePoints >= upgrade.Price;
        }

        public static void DEBUG_GetStartPoints()
        {
            InfluencePoints = 100;
        }

        public static void ProceedUpgradePurchase(IUpgrade upgrade)
        {
            InfluencePoints -= upgrade.Price;
        }
    }
}