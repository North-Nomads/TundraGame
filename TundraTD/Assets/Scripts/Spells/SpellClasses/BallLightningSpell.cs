using System;
using UnityEngine;

namespace Spells
{
    public class BallLightningSpell : MagicSpell
    {
        public override BasicElement Element => BasicElement.Fire | BasicElement.Lightning;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            gameObject.transform.position = hitInfo.point;
        }
    }
}