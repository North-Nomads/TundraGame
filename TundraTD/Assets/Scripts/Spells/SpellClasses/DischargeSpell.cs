using UnityEngine;

namespace Spells
{
	public class DischargeSpell : MagicSpell
	{
        public override BasicElement Element => BasicElement.Lightning;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}