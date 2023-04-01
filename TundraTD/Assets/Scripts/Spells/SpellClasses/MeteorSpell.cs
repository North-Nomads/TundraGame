using UnityEngine;

namespace Spells
{
	public class MeteorSpell : MagicSpell
	{
        public override BasicElement Element => BasicElement.Fire | BasicElement.Earth;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}