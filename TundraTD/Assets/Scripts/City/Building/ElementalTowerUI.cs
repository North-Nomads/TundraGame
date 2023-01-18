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

        public void LoadUpgradesInTowerMenu(IUpgrade[,] upgrades)
        {
            int i = 0;
            for (int x = 0; x < upgrades.GetLength(0); x++)
            {
                for (int y = 0; y < upgrades.GetLength(1); y++)
                {
                    var upgradeUI = Instantiate(upgradeButtonPrefab, upgradeLevels[i].transform);
                    var upgrade = upgrades[x, y];
                    upgradeUI.Button.onClick.AddListener(() => _elementalTower.HandleUpgradePurchase(upgrade));
                    if (!(upgrade is null))
                        upgradeUI.UpgradeIcon.sprite = upgrade.Sprite;
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