using Spells;
using UnityEngine;

namespace City.Building.Upgrades
{
    /// <summary>
    /// Describes the behaviour of any Upgrade in a game
    /// </summary>
    public interface IUpgrade
    {
        BasicElement Element { get; }
        int Price { get; }
        int RequiredLevel { get; }
        Sprite Sprite { get; }
        
        void ExecuteOnUpgradeBought();
    }
}