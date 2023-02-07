using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.EarthTowerUpgrades
{
    public class EarthEnemiesDamageUpgrade : IUpgrade
    {
        public BasicElement UpgradeTowerElement => BasicElement.Earth;
        public int PurchasePriceInTowerMenu => 25;
        public int SpellPurchaseRequiredLevel => 1;
        public string UpgradeDescriptionText => "Increase damege on earth mobs by 5%";
        public Sprite UpgradeShowcaseSprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            EarthPool.DamageAgainstElementMultipliers[BasicElement.Earth] *= 1.05f;
        }
    }
}