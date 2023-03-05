using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.WaterTowerUpgrades
{
    public class BarrierUpgrade : IUpgrade
    {
        public Sprite UpgradeShowcaseSprite { get; } = Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public BasicElement UpgradeTowerElement => BasicElement.Water;

        public int PurchasePriceInTowerMenu => 200;

        public int SpellPurchaseRequiredLevel => 2;

        public string UpgradeDescriptionText => "Create a rigid barrier which doesn't allow mobs to escape the rain";

        public void ExecuteOnUpgradeBought()
        {
            WaterPool.CreateBarrier = true;
        }
    }
}