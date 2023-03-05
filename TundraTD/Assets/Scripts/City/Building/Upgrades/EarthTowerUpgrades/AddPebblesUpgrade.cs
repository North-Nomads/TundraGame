using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.EarthTowerUpgrades
{
    public class AddPebblesUpgrade : IUpgrade
    {
        public BasicElement UpgradeTowerElement => BasicElement.Earth;
        public int PurchasePriceInTowerMenu => 100;
        public int SpellPurchaseRequiredLevel => 1;
        public string UpgradeDescriptionText => "Small pebbles hit and stun the enemies around the walls";
        public Sprite UpgradeShowcaseSprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            EarthPool.HasExplosivePebbles = true;
        }
    }
}