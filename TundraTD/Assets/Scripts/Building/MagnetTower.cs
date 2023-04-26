using Spells;

namespace Building
{
    public class MagnetTower : EnemyTower
    {
        public float InteractionSize;

        protected override void HandleSpellCast(object sender, MagicSpell.SpellCastInfo e)
        {
            var spell = (MagicSpell)sender;
            //If the spell cast point is nearby from the magnet and spell is in "blacklist", we cancel it.
            if (((e.HitInfo.point - transform.position).sqrMagnitude < InteractionSize * InteractionSize &&
                spell.Element == BasicElement.Fire) ||
                spell.Element == (BasicElement.Water | BasicElement.Earth) ||
                spell.Element == BasicElement.Earth ||
                spell.Element == (BasicElement.Earth | BasicElement.Lightning))
            {
                e.Cancel = true;
            }
        }
    }
}