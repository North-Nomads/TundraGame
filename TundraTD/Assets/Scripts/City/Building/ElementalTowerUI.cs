using City.Building.Upgrades;
using UnityEngine;
using UnityEngine.UI;

namespace City.Building
{
    public class ElementalTowerUI : MonoBehaviour
    {
        [SerializeField] private UpgradeUI upgradeButtonPrefab;
        [SerializeField] private GridLayoutGroup[] upgradeLevels;
        private ElementalTower _elementalTower;
        
        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void LoadUpgradesInTowerMenu(IUpgrade[][] upgradesLists)
        {
            int i = 0;
            foreach (var upgradesList in upgradesLists)
            {
                var level = upgradeLevels[i];
                foreach (var upgrade in upgradesList)
                {
                    var upgradeUI = Instantiate(upgradeButtonPrefab, level.transform);
                    upgradeUI.Button.onClick.AddListener(() => _elementalTower.HandleUpgradePurchase(upgrade));
                    upgradeUI.UpgradeIcon.sprite = upgrade.Sprite;
                }
                i++;
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