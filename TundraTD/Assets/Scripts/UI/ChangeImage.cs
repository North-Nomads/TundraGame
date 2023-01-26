using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
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
            if (buttonsHolder.elementIcons[i].sprite == buttonsHolder.elementIcons[i + 1].sprite)
            {
                buttonsHolder.elementIcons[i].sprite = elementIcon.sprite;
                break;
            }
        }
    }
}
