using System;
using UnityEngine;

namespace Spells
{
    public class BallLightningSpell : MagicSpell
    {
        public override BasicElement Element => BasicElement.Fire | BasicElement.Lightning;

        [SerializeField] BallLightning ball;

        public override void ExecuteSpell(RaycastHit hitInfo)
        {
            ball.Detonated += DetonationHandler;
            ball.transform.position = hitInfo.point;
        }

        private void DetonationHandler(object sender, EventArgs e)
        {
            Destroy(gameObject);
        }
    }
}