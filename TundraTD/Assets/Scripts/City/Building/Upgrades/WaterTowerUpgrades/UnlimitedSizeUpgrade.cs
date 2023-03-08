using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.WaterTowerUpgrades
{
    public class UnlimitedSizeUpgrade : IUpgrade
    {
        public Sprite UpgradeShowcaseSprite { get; } = Resources.Load<Sprite>("UpgradeIcons/Water/Cryomancer17");

        public BasicElement UpgradeTowerElement => BasicElement.Water;

        public int PurchasePriceInTowerMenu => 200;

        public int SpellPurchaseRequiredLevel => 2;

        public string UpgradeDescriptionText => "Rain doesn't have size limits anymore";

        public void ExecuteOnUpgradeBought()
        {
            WaterPool.UnlimitedRadius = true;
        }
    }
}