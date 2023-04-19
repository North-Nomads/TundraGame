using UnityEngine;

namespace Spells
{
    public class StormCloudSpell : MagicSpell
    {
        public override BasicElement Element => BasicElement.Lightning | BasicElement.Water;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}