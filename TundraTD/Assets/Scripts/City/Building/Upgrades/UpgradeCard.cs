using UnityEngine;
using UnityEngine.UI;

namespace City.Building.Upgrades
{
    public class UpgradeCard : MonoBehaviour
    {
        [SerializeField] private Image skillIcon;
        [SerializeField] private Text upgradeDescriptionTextfield;
        [SerializeField] private Button purchaseButton;

        public Image SkillIcon => skillIcon;
        public Text UpgradeDescriptionTextfield => upgradeDescriptionTextfield;
        public Button PurchaseButton => purchaseButton;
    }
}