using UnityEngine;

namespace Spells
{
    public class RainSpell : MagicSpell
    {
        public override BasicElement Element => BasicElement.Water;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}