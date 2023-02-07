using UnityEngine;

namespace City.Building.Upgrades
{
    public class TowerUpgradeLevel : MonoBehaviour
    {
        [SerializeField] private UpgradeCard leftCard;
        [SerializeField] private UpgradeCard rightCard;

        public UpgradeCard LeftCard => leftCard;
        public UpgradeCard RightCard => rightCard;
    }
}