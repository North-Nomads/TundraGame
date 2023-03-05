using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.WaterTowerUpgrades
{
    public class SnowUpgrade : IUpgrade
    {
        public Sprite UpgradeShowcaseSprite { get; } = Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public BasicElement UpgradeTowerElement => BasicElement.Water;

        public int PurchasePriceInTowerMenu => 350;

        public int SpellPurchaseRequiredLevel => 3;

        public string UpgradeDescriptionText => "Cast freezing snow instead of rain";

        public void ExecuteOnUpgradeBought()
        {
            WaterPool.CastSnowInsteadOfRain = true;
        }
    }
}