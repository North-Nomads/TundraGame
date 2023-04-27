using Spells;
using UnityEngine;

namespace Building
{
	public class EnemyTower : MonoBehaviour
	{
        private void Start()
        {
            MagicSpell.SpellCast += HandleSpellCast;
        }

        private void OnDestroy()
        {
            MagicSpell.SpellCast -= HandleSpellCast;
        }

        protected virtual void HandleSpellCast(object sender, MagicSpell.SpellCastInfo e)
        {

        }
    }
}