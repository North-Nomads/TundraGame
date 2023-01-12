using City.Building.Upgrades;
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
        private TowerPlacementSlot _towerSlot;
        private IUpgrade[][] _towerUpgrades;
        private int _towerCurrentUpgradeLevel;
        
        public TowerPlacementSlot TowerSlot { get; set; }
        public BasicElement TowerElement => towerElement;
        public int TowerPurchasePrice => towerPurchasePrice;

        // + HandleUpgradePurchase(IUpgrade upgrade):  void

        private void Start()
        {
            InstantiateUpgradesList();
        }

        private void InstantiateUpgradesList()
        {
            _towerUpgrades = TowerUpgrades.UpgradesMap[towerElement];
            _elementalTowerUI.LoadUpgradesInTowerMenu(_towerUpgrades);
        }

        private void OnMouseDown()
        {
            _elementalTowerUI.OpenTowerMenu();
        }
        
        public void InstantiateTowerUIMenu()
        {
            _elementalTowerUI = Instantiate(elementalTowerUIPrefab, TowerSlot.Architect.CanvasesParent);
        }
    }
}