﻿using City.Building.ElementPools;
using Spells;
using UnityEngine;

namespace City.Building.Upgrades.FireTowerUpgrades
{
    public class FireEnemiesDamageUpgrade : IUpgrade
    {
        public BasicElement Element => BasicElement.Fire;
        public int Price => 30;
        public int RequiredLevel => 1;
        public string UpgradeDescriptionText => "MY DIFFERENT TEXT";
        public Sprite Sprite => Resources.Load<Sprite>("UpgradeIcons/Arcanist1");

        public void ExecuteOnUpgradeBought()
        {
            FirePool.DamageAgainstFireMultiplier += 1.05f;
        }
    }
}