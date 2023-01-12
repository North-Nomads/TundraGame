using Spells;
using UnityEngine;

namespace City.Building
{
    public class ElementalTower : MonoBehaviour
    {
        [SerializeField] private BasicElement towerElement;
        [SerializeField] private int towerPurchasePrice; 
        [SerializeField] private ElementalTowerUI elementalTowerUIPrefab;
        
        private ElementalTowerUI _elementalTowerUI;
        private int _towerCurrentUpgradeLevel;
        private TowerPlacementSlot _towerSlot;
        // - _towerUpgrade: IUpgrade[][] // ступенчатый массив
        
        public TowerPlacementSlot TowerSlot { get; set; }
        public BasicElement TowerElement => towerElement;
        public int TowerPurchasePrice => towerPurchasePrice;

        // + HandleUpgradePurchase(IUpgrade upgrade):  void

        public void InstantiateTowerUI()
        {
            _elementalTowerUI = Instantiate(elementalTowerUIPrefab, TowerSlot.Architect.CanvasesParent);
        }
        
        private void OnMouseDown()
        {
            _elementalTowerUI.OpenTowerMenu();
        }
    }
}