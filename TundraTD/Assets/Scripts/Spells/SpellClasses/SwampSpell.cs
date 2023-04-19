using UnityEngine;

namespace Spells
{
	public class SwampSpell : MagicSpell
	{
        public override BasicElement Element => BasicElement.Earth | BasicElement.Water;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}