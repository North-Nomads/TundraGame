using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.FireTowerUpgrades
{
    public class MeteorLandingReduction : IUpgrade
    {
        public BasicElement Element => BasicElement.Fire;
        public int Price => 350;
        public int RequiredLevel => 3;
        public string UpgradeDescriptionText => "-2 seconds delay before meteorite impact";
        public Sprite Sprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            FirePool.MeteorLandingReduction -= 2;
        }
    }
}