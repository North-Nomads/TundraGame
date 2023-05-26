using Building;
using Spells;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

public class SpellAnalytics : EnemyTower
{
    protected override void HandleSpellCast(object sender, MagicSpell.SpellCastInfo e)
    {
        Analytics.CustomEvent(sender.GetType().ToString().Split('.').Last());
    }
}
