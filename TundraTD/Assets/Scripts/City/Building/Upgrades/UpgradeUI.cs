using UnityEngine;
using UnityEngine.UI;

namespace City.Building.Upgrades
{
    public class UpgradeUI : MonoBehaviour
    {
        public Button Button { get; private set; }

        public Image UpgradeIcon { get; private set; }

        private void Awake()
        {
            UpgradeIcon = GetComponentInChildren<Image>();
            Button = GetComponent<Button>();
        }
    }
}