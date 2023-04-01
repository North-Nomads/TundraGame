using UnityEngine;

namespace Spells
{
    public class FirestormSpell : MagicSpell
    {
        public override BasicElement Element => BasicElement.Fire | BasicElement.Air;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}