using UnityEngine;

namespace Spells
{
    public class BallLightningSpell : MagicSpell
    {
        public override BasicElement Element => BasicElement.Fire | BasicElement.Lightning;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}