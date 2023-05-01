using Building;
using Spells;
using Spells.SpellClasses;
using UnityEngine;

public class LightningRod : EnemyTower
{
    [SerializeField] private float effectRadius;
    protected override void HandleSpellCast(object sender, MagicSpell.SpellCastInfo e)
    {
        if((e.HitInfo.point - transform.position).magnitude < effectRadius && (sender is LightiningBoltSpell))
        {
            (sender as LightiningBoltSpell).OverrideStrike(e.HitInfo.point, transform.position);
        }
    }
}
