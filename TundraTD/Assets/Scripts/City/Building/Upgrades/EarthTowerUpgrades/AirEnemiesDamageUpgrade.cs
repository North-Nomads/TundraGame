using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.EarthTowerUpgrades
{
    public class AirEnemiesDamageUpgrade : IUpgrade
    {
        public BasicElement UpgradeTowerElement => BasicElement.Earth;
        public int PurchasePriceInTowerMenu => 25;
        public int SpellPurchaseRequiredLevel => 1;
        public string UpgradeDescriptionText => "Increase damege on air mobs by 5%";
        public Sprite UpgradeShowcaseSprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            EarthPool.DamageAgainstElementMultipliers[BasicElement.Air] *= 1.05f;
        }
    }
}