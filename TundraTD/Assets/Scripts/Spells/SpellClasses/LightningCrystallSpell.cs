using Spells;
using UnityEngine;

public class LightningCrystallSpell : MagicSpell
{
    public override BasicElement Element => BasicElement.Earth | BasicElement.Lightning;


    public override void ExecuteSpell(RaycastHit hitInfo)
    {
        gameObject.transform.position = hitInfo.point;
    }
}
