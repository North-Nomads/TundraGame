using City.Building.Upgrades;
using UnityEngine;
using UnityEngine.UI;

namespace City.Building
{
    public class ElementalTowerUI : MonoBehaviour
    {
        [SerializeField] private GameObject upgradeButtonPrefab;
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
                    Instantiate(upgradeButtonPrefab, level.transform);
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