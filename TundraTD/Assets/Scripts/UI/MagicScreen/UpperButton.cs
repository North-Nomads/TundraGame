using Spells;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MagicScreen
{
    public class UpperButton : MonoBehaviour
    {
        private Sprite startIcon;

        public BasicElement Element { get; set; }

        public Image ElementIcon { get; set; }

        private void Start()
        {
            ElementIcon = GetComponent<Image>();
            startIcon = ElementIcon.sprite;
        }

        public void OnButtonClick()
        {
            ElementIcon.sprite = startIcon;
            Element = BasicElement.None;
        }
    }
}