using UnityEngine;

namespace City.Building.Upgrades
{
    public abstract class Upgrade
    {
        public abstract int Price { get; }
        public abstract int RequiredLevel { get; }
        public abstract Sprite Sprite { get; }
        
        public abstract void ExecuteOnUpgradeBought();
    }
}