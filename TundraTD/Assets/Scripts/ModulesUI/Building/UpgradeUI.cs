using UnityEngine;
using UnityEngine.UI;

namespace ModulesUI.Building
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