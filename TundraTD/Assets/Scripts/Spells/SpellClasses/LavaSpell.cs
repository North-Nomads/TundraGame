using UnityEngine;

namespace Spells
{
	public class LavaSpell : MagicSpell
	{
        public override BasicElement Element => BasicElement.Fire;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}