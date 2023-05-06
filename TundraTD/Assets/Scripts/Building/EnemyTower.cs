using Spells;
using UnityEngine;

namespace Building
{
	public abstract class EnemyTower : MonoBehaviour
    {
        private void Start()
        {
            MagicSpell.SpellCast += HandleSpellCast;
            ExecuteOnStart();
        }

        private void OnDestroy()
        {
            MagicSpell.SpellCast -= HandleSpellCast;
        }

        protected virtual void HandleSpellCast(object sender, MagicSpell.SpellCastInfo e)
        {

        }

        protected virtual void ExecuteOnStart() { }
    }
}