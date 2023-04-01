using UnityEngine;

namespace Spells
{
	public class SpikesSpell : MagicSpell
	{

        public override BasicElement Element => BasicElement.Earth;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}