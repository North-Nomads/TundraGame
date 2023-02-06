using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.FireTowerUpgrades
{
    public class MeteorLandingStunUpgrade : IUpgrade
    {
        public BasicElement Element => BasicElement.Fire;
        public int Price => 700;
        public int RequiredLevel => 4;
        public string UpgradeDescriptionText => "When a meteor lands, a deafening wave appears";
        public Sprite Sprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            FirePool.HasLandingStun = true;
        }
    }
}