using Building;
using Spells;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningRod : EnemyTower
{
    [SerializeField] private float effectRadius;
    protected override void HandleSpellCast(object sender, MagicSpell.SpellCastInfo e)
    {
        if((e.HitInfo.point - transform.position).magnitude < effectRadius && (sender as MagicSpell).Element == BasicElement.Lightning)
        {
            Destroy(sender as MagicSpell);
        }
    }
}
