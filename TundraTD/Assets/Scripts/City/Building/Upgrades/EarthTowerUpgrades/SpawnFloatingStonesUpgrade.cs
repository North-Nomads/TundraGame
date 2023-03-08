using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.EarthTowerUpgrades
{
    public class SpawnFloatingStonesUpgrade : IUpgrade
    {
        public BasicElement UpgradeTowerElement => BasicElement.Earth;
        public int PurchasePriceInTowerMenu => 350;
        public int SpellPurchaseRequiredLevel => 3;
        public string UpgradeDescriptionText => "Spawns floating stones that ram into the ground and hit mobs nearby";
        public Sprite UpgradeShowcaseSprite => Resources.Load<Sprite>("UpgradeIcons/Earth/Geomancer2");

        public void ExecuteOnUpgradeBought()
        {
            EarthPool.HasFloatingStones = true;
        }
    }
}