using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spells;

public class ChangeImage : MonoBehaviour
{
    [SerializeField] private BasicElement element;
    [SerializeField] private UseElements buttonsHolder;
    private Image elementIcon;
    private Image startIcon;
    
    private void Start()
    {
        elementIcon = GetComponent<Image>();
        startIcon = elementIcon;
    }

    
    public void OnButtonClick()
    {

        for (int i = 0; i < 5; i++)
        {
            if (buttonsHolder.Buttons[i].GetComponent<DeleteSprite>().Elemental == BasicElement.None)
            {
                buttonsHolder.elementIcons[i].sprite = elementIcon.sprite;
                buttonsHolder.Buttons[i].GetComponent<DeleteSprite>().Elemental = element;
                break;
            }
        }
    }
}
