using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades
{
    public class WaterEnemiesDamageUpgrade : IUpgrade
    {
        /*public override int Price => 80;
        public override int RequiredLevel => 1;
        public override Sprite Sprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public override void ExecuteOnUpgradeBought()
        {
            FirePool.DamageAgainstWaterMultiplier += 1.05f;
        }*/
        public BasicElement Element => BasicElement.Fire;
        public int Price => 50;
        public int RequiredLevel => 1;
        public Sprite Sprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            FirePool.DamageAgainstWaterMultiplier += 1.05f;
        }
    }
}