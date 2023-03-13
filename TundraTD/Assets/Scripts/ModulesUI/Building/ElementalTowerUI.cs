using System;
using City.Building;
using City.Building.Upgrades;
using Spells;
using UnityEngine;
using UnityEngine.UI;

namespace ModulesUI.Building
{
    /// <summary>
    /// UI component of any elemental tower
    /// </summary>
    public class ElementalTowerUI : TundraCanvas
    {
        public override CanvasGroup CanvasGroup => CanvasGroup.Building;
        public override CanvasGroup BlockList => CanvasGroup.MagicHUD | CanvasGroup.Portal | CanvasGroup.City;
        
        [SerializeField] private TowerUpgradeLevel[] upgradeLevels;
        [SerializeField] private Image upgradeLevelIndicator;
        [SerializeField] private Text allUpgradesBoughtPage;
        [SerializeField] private Text canvasTitle;

        private ElementalTower _elementalTower;

        private void Start()
        {
            gameObject.SetActive(false);
            allUpgradesBoughtPage.gameObject.SetActive(false);
            UIToggle.AllCanvases.Add(this);

            foreach (var upgradeLevel in upgradeLevels)
                upgradeLevel.gameObject.SetActive(false);

            upgradeLevelIndicator.fillAmount = 0;
        }

        private void OnEnable()
        {
            if (_elementalTower == null)
                return;

            for (int i = 0; i < upgradeLevels.Length; i++)
                upgradeLevels[i].gameObject.SetActive(i == _elementalTower.TowerUpgradeLevel - 1);
        }

        public void UpdateUpgradesPage()
        {
            if (_elementalTower.TowerUpgradeLevel == upgradeLevels.Length)
                allUpgradesBoughtPage.gameObject.SetActive(true);
            else
                upgradeLevels[_elementalTower.TowerUpgradeLevel].gameObject.SetActive(true);

            upgradeLevels[_elementalTower.TowerUpgradeLevel - 1].gameObject.SetActive(false);
            upgradeLevelIndicator.fillAmount = (float)_elementalTower.TowerUpgradeLevel / upgradeLevels.Length;
        }

        public void LoadUpgradesInTowerMenu(IUpgrade[,] upgrades)
        {
            var xSize = upgrades.GetLength(0);
            var ySize = upgrades.GetLength(1);
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    // Choose card using the parity of the current index
                    var card = (x * xSize + y) % 2 == 0
                        ? upgradeLevels[x].RightCard
                        : upgradeLevels[x].LeftCard;

                    var upgrade = upgrades[x, y];
                    
                    if (upgrade is null)
                        throw new NullReferenceException($"Upgrade in dictionary on [{x}, {y}] is null");

                    card.UpgradeDescriptionTextfield.text = upgrade.UpgradeDescriptionText;
                    card.SkillIcon.sprite = upgrade.UpgradeShowcaseSprite;
                    card.PurchaseButton.onClick.AddListener(() => _elementalTower.HandleUpgradePurchase(upgrade));
                }
            }
        }

        public void SetLinkedTower(ElementalTower tower)
        {
            var name = Enum.GetName(typeof(BasicElement), tower.TowerElement);
            _elementalTower = tower;
            canvasTitle.text = $"{name} tower".ToUpper();
        }

        public void OpenTowerMenu()
        {
            gameObject.SetActive(true);
        }

        public void CloseTowerMenu()
        {
            gameObject.SetActive(false);
        }
    }
}