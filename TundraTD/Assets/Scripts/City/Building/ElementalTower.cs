using Spells;
using UnityEngine;

namespace City.Building
{
    [RequireComponent(typeof(ElementalTowerUI))]
    public class ElementalTower : MonoBehaviour
    {
        [SerializeField] private BasicElement towerElement;
        [SerializeField] private ElementalTowerUI elementalTowerUI;
        [SerializeField] private int towerPurchasePrice;


        private int _towerCurrentUpgradeLevel;
        // - _towerUpgrade: IUpgrade[][] // ступенчатый массив
        
        public BasicElement TowerElement => towerElement;
        public int TowerPurchasePrice => towerPurchasePrice;

        // + HandleUpgradePurchase(IUpgrade upgrade):  void
    }
}