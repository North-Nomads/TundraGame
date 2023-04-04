using UnityEngine;

namespace Spells
{
	public class CrystalSpell : MagicSpell
	{

        public override BasicElement Element => BasicElement.Lightning | BasicElement.Air;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}