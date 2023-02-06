using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.EarthTowerUpgrades
{
    public class TimeStunEnemiesUpgrade : IUpgrade
    {
        public BasicElement UpgradeTowerElement => BasicElement.Earth;
        public int PurchasePriceInTowerMenu => 350;
        public int SpellPurchaseRequiredLevel => 3;
        public string UpgradeDescriptionText => "Increase time stun of enemies by 0.5 second";
        public Sprite UpgradeShowcaseSprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            EarthPool.TimeStunEnemies += 0.5f;
        }
    }
}

