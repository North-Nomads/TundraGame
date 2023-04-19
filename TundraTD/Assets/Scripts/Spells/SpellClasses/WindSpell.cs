using UnityEngine;

namespace Spells
{
    public class WindSpell : MagicSpell
    {
        public override BasicElement Element => BasicElement.Air;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}