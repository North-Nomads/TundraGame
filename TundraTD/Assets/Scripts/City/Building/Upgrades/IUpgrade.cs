using Spells;
using UnityEngine;

namespace City.Building.Upgrades
{
    /// <summary>
    /// Describes the behaviour of any Upgrade in a game
    /// </summary>
    public interface IUpgrade
    {
        Sprite UpgradeShowcaseSprite { get; }
        BasicElement UpgradeTowerElement { get; }
        int PurchasePriceInTowerMenu { get; }
        int SpellPurchaseRequiredLevel { get; }
        string UpgradeDescriptionText { get; }
        
        void ExecuteOnUpgradeBought();
    }
}