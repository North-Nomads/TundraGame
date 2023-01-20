using City.Building.Upgrades;
using UnityEngine;
using UnityEngine.UI;

namespace City.Building
{
    public class ElementalTowerUI : MonoBehaviour
    {
        [SerializeField] private RectTransform[] upgradeLevels;
        [SerializeField] private Image[] upgradeLevelIndicators;
        [SerializeField] private UpgradeCard rightUpgradeCard;
        [SerializeField] private UpgradeCard leftUpgradeCard;
        
        private ElementalTower _elementalTower;
        private Sprite _achievedLevelIndicator;
        private Sprite _unachievedLevelIndicator;
        
        private void Start()
        {
            gameObject.SetActive(false);
            _achievedLevelIndicator = Resources.Load<Sprite>("UpgradeIcons/Green");
            _unachievedLevelIndicator = Resources.Load<Sprite>("UpgradeIcons/White");

            foreach (var levelIndicator in upgradeLevelIndicators)
                levelIndicator.sprite = _unachievedLevelIndicator;
                
            foreach (var upgradeLevel in upgradeLevels)
                upgradeLevel.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            if (_elementalTower is null)
                return;
            
            for (int i = 0; i < upgradeLevels.Length; i++)
            {
                if (i != _elementalTower.TowerUpgradeLevel - 1)
                    upgradeLevels[i].gameObject.SetActive(false);
                else
                    upgradeLevels[i].gameObject.SetActive(true);
            }
        }

        public void UpdateUpgradesPage()
        {
            upgradeLevels[_elementalTower.TowerUpgradeLevel - 1].gameObject.SetActive(true);
            upgradeLevels[_elementalTower.TowerUpgradeLevel - 2].gameObject.SetActive(false);
            upgradeLevelIndicators[_elementalTower.TowerUpgradeLevel - 2].sprite = _achievedLevelIndicator;
        }

        public void LoadUpgradesInTowerMenu(IUpgrade[,] upgrades)
        {
            for (int x = 0; x < upgrades.GetLength(0); x++)
            {
                for (int y = 0; y < upgrades.GetLength(1); y++)
                {
                    UpgradeCard card;
                    if ((x * upgrades.GetLength(0) + y) % 2 == 0)
                        card = rightUpgradeCard;
                    else
                        card = leftUpgradeCard;
                    
                    var upgrade = upgrades[x, y];
                    
                    // temporary skips null upgrades
                    if (upgrade is null)
                        continue;
                    
                    card.UpgradeDescriptionTextfield.text = upgrade.UpgradeDescriptionText;
                    card.SkillIcon.sprite = upgrade.Sprite;
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