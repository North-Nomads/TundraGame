using City.Building.Upgrades;
using UnityEngine;
using UnityEngine.UI;

namespace City.Building
{
    public class ElementalTowerUI : MonoBehaviour
    {
        [SerializeField] private UpgradeUI upgradeButtonPrefab;
        [SerializeField] private GridLayoutGroup[] upgradeLevels;

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
                    upgradeUI.Button.onClick.AddListener(() => ElementalTower.HandleUpgradePurchase(upgrade));
                    var sprite = Resources.Load<Sprite>("UpgradeIcons/Arcanist1");
                    upgradeUI.UpgradeIcon.sprite = sprite;
                }
                i += 1;
            }
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