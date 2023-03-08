using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.WaterTowerUpgrades
{
    public class WeaknessUpgrade : IUpgrade
    {
        public Sprite UpgradeShowcaseSprite { get; } = Resources.Load<Sprite>("UpgradeIcons/Water/Guardian8");

        public BasicElement UpgradeTowerElement => BasicElement.Water;

        public int PurchasePriceInTowerMenu => 0;

        public int SpellPurchaseRequiredLevel => 1;

        public string UpgradeDescriptionText => "Weaken enemies when cast";

        public void ExecuteOnUpgradeBought()
        {
            WaterPool.ApplyWeaknessOnEnemies = true;
        }
    }
}