using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.FireTowerUpgrades
{
    public class MeteorLandingLavaPoolUpgrade : IUpgrade
    {
        public BasicElement Element => BasicElement.Fire;
        public int Price => 350;
        public int RequiredLevel => 3;
        public string UpgradeDescriptionText => "Meteor leaves behind a lava crater";
        public Sprite Sprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            FirePool.HasLandingLavaPool = true;
        }
    }
}