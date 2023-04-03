//using City.Building.Upgrades;
//using ModulesUI;
//using ModulesUI.Building;
//using Spells;
//using UnityEngine;
//using UnityEngine.EventSystems;

//namespace City.Building
//{
//    public class ElementalTower : MonoBehaviour
//    {
//        [SerializeField] private BasicElement towerElement;
//        [SerializeField] private int towerPurchasePrice;
//        [SerializeField] private ElementalTowerUI elementalTowerUIPrefab;

//        private ElementalTowerUI _elementalTowerUI;
//        private IUpgrade[,] _towerUpgrades;

//        public BasicElement TowerElement => towerElement;
//        public int TowerPurchasePrice => towerPurchasePrice;

//        public int TowerUpgradeLevel { get; private set; }

//        private void Start()
//        {
//            TowerUpgradeLevel = 1;
//            _towerUpgrades = TowerUpgrades.UpgradesMap[towerElement];
//            InitializeTowerUIOnTowerBuild();
//        }

//        private void OnMouseDown()
//        {
//            UIToggle.TryOpenCanvas(_elementalTowerUI);
//        }

//        private void InitializeTowerUIOnTowerBuild()
//        {
//            _elementalTowerUI = Instantiate(elementalTowerUIPrefab, Architect.CanvasesHierarchyParent);
//            _elementalTowerUI.SetLinkedTower(this);
//            _elementalTowerUI.LoadUpgradesInTowerMenu(_towerUpgrades);
//        }

//        public void HandleUpgradePurchase(IUpgrade upgrade)
//        {
//            if (!Architect.CanUpgradeBeBought(upgrade))
//                return;

//            if (TowerUpgradeLevel != upgrade.SpellPurchaseRequiredLevel)
//                return;

//            upgrade.ExecuteOnUpgradeBought();
//            Architect.ProceedUpgradePurchase(upgrade);
//            _elementalTowerUI.UpdateUpgradesPage();
//            TowerUpgradeLevel++;
//        }
//    }
//}