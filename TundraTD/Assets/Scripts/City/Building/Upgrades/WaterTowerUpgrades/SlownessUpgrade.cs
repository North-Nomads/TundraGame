using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.WaterTowerUpgrades
{
    public class SlownessUpgrade : IUpgrade
    {
        public Sprite UpgradeShowcaseSprite { get; } = Resources.Load<Sprite>("UpgradeIcons/Water/Enchanter18");

        public BasicElement UpgradeTowerElement => BasicElement.Water;

        public int PurchasePriceInTowerMenu => 350;

        public int SpellPurchaseRequiredLevel => 3;

        public string UpgradeDescriptionText => "Rain makes mud which make enemies slower";

        public void ExecuteOnUpgradeBought()
        {
            WaterPool.AdditionalSlowness = true;
        }
    }
}