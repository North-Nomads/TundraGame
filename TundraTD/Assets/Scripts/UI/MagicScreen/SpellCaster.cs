using Spells;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    [SerializeField] private UseElements buttonsHolder;
    BasicElement [] HoldSpells = new BasicElement[5];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnButtonClick()
    {
        for (int i = 0; i < 5; i++)
        {
            HoldSpells[i] = buttonsHolder.Buttons[i].GetComponent<DeleteSprite>().Elemental;
        }
        Grimoire.TurnElementsIntoSpell(HoldSpells);
    }
}
