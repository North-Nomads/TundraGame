using Spells;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MagicScreen
{
    public class UpperButton : MonoBehaviour
    {
        [SerializeField] private UpperButtonElements buttonsHolder;

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
            int index = Array.IndexOf(buttonsHolder.ElementScripts, this);
            if (index == 4)
            {
                Clear();
            }
            else
            {
                var NextButton = buttonsHolder.ElementScripts[index + 1];
                ElementIcon.sprite = NextButton.ElementIcon.sprite;
                Element = NextButton.Element;
                NextButton.OnButtonClick();
            }
        }

        public void Clear()
        {
            ElementIcon.sprite = startIcon;
            Element = BasicElement.None;
        }

    }
}