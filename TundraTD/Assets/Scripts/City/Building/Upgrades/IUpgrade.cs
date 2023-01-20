using Spells;
using UnityEngine;

namespace City.Building.Upgrades
{
    /// <summary>
    /// Describes the behaviour of any Upgrade in a game
    /// </summary>
    public interface IUpgrade
    {
        Sprite Sprite { get; }
        BasicElement Element { get; }
        int Price { get; }
        int RequiredLevel { get; }
        string UpgradeDescriptionText { get; }
        
        void ExecuteOnUpgradeBought();
    }
}