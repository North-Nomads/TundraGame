using City.Building.ElementPools;
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
        private IUpgrade[,] _towerUpgrades;
        private int _towerUpgradeLevel;
        
        public BasicElement TowerElement => towerElement;
        public int TowerPurchasePrice => towerPurchasePrice;

        public int TowerUpgradeLevel => _towerUpgradeLevel;

        private void Start()
        {
            _towerUpgradeLevel = 1;
            _towerUpgrades = SortUpgrades(TowerUpgrades.UpgradesMap[towerElement]);
            InitializeTowerUIOnTowerBuild();
        }

        private IUpgrade[,] SortUpgrades(IUpgrade[,] upgrades)
        {
            return upgrades;
        }

        private void OnMouseDown()
        {
            _elementalTowerUI.OpenTowerMenu();
        }

        private void InitializeTowerUIOnTowerBuild()
        {
            _elementalTowerUI = Instantiate(elementalTowerUIPrefab, Architect.CanvasesParent);
            _elementalTowerUI.SetLinkedTower(this);
            _elementalTowerUI.LoadUpgradesInTowerMenu(_towerUpgrades);
        }
        
        public void HandleUpgradePurchase(IUpgrade upgrade)
        {
            if (!Architect.CanUpgradeBeBought(upgrade))
                return;

            if (_towerUpgradeLevel != upgrade.RequiredLevel)
                return;
            
            upgrade.ExecuteOnUpgradeBought();
            Architect.ProceedUpgradePurchase(upgrade);
            _towerUpgradeLevel += 1;
            _elementalTowerUI.UpdateUpgradesPage();            
        }
    }
}