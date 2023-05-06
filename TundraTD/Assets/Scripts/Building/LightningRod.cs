using Spells;
using Spells.SpellClasses;
using UnityEngine;

namespace Building
{
    public class LightningRod : EnemyTower
    {
        [SerializeField] private float effectRadius;
        protected override void HandleSpellCast(object sender, MagicSpell.SpellCastInfo e)
        {
            if((e.HitInfo.point - transform.position).magnitude < effectRadius && (sender is LightningBoltSpell))
            {
                (sender as LightningBoltSpell).OverrideStrike(e.HitInfo.point, transform.position);
            }
        }
    }
}
