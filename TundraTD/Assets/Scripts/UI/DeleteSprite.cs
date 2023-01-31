using Spells;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteSprite : MonoBehaviour
{
    private Image elementIcon;
    private Sprite startIcon;
    public BasicElement Elemental { get; set; }
    private void Start()
    {
        elementIcon = GetComponent<Image>();
        startIcon = elementIcon.sprite;
    }

    public void OnButtonClick()
    {
        elementIcon.sprite = startIcon;
        Elemental = BasicElement.None;
    }
}
