using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.FireTowerUpgrades
{
    public class WaterResistUpgrade : IUpgrade
    {
        public BasicElement UpgradeTowerElement => BasicElement.Fire;
        public int PurchasePriceInTowerMenu => 0;
        public int SpellPurchaseRequiredLevel => 1;
        public string UpgradeDescriptionText => "Burning effect becomes water resistant";
        public Sprite UpgradeShowcaseSprite => Resources.Load<Sprite>("UpgradeIcons/Fire/Enchanter2");

        public void ExecuteOnUpgradeBought()
        {
            FirePool.HasWaterResist = true;
        }
    }
}