using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.WaterTowerUpgrades
{
    public class SuperLightningUpgrade : IUpgrade
    {
        public Sprite UpgradeShowcaseSprite { get; } = Resources.Load<Sprite>("UpgradeIcons/Water/Shaman1");

        public BasicElement UpgradeTowerElement => BasicElement.Water;

        public int PurchasePriceInTowerMenu => 0;

        public int SpellPurchaseRequiredLevel => 1;

        public string UpgradeDescriptionText => "When cast lightning on wet enemies, it hits with triple power";

        public void ExecuteOnUpgradeBought()
        {
            WaterPool.AllowSuperLightning = true;
        }
    }
}