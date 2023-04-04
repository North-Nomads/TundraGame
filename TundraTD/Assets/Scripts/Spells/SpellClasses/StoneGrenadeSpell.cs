using UnityEngine;

namespace Spells
{
    public class StoneGrenadeSpell : MagicSpell
    {
        public override BasicElement Element => BasicElement.Earth | BasicElement.Air;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}