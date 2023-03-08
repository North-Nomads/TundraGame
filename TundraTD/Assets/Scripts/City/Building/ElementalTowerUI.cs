using City.Building.Upgrades;
using System;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

namespace City.Building
{
    /// <summary>
    /// UI component of any elemental tower
    /// </summary>
    public class ElementalTowerUI : MonoBehaviour
    {
        [SerializeField] private TowerUpgradeLevel[] upgradeLevels;
        [SerializeField] private Image upgradeLevelIndicator;
        [SerializeField] private Text allUpgradesBoughtPage;

        private ElementalTower _elementalTower;

        private void Start()
        {
            gameObject.SetActive(false);
            allUpgradesBoughtPage.gameObject.SetActive(false);

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
            _elementalTower = tower;
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