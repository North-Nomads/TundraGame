using Spells;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MagicScreen
{
    /// <summary>
    /// A button that shows a part of player's current deck 
    /// </summary>
    public class DeckButton : MonoBehaviour
    {
        [SerializeField] private UpperButtonElements buttonsHolder;

        private Sprite _startIcon;

        public BasicElement Element { get; set; }

        public Image ElementIcon { get; set; }

        private void Start()
        {
            ElementIcon = GetComponent<Image>();
            _startIcon = ElementIcon.sprite;
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
                var nextButton = buttonsHolder.ElementScripts[index + 1];
                ElementIcon.sprite = nextButton.ElementIcon.sprite;
                Element = nextButton.Element;
                nextButton.OnButtonClick();
            }
        }

        public void Clear()
        {
            ElementIcon.sprite = _startIcon;
            Element = BasicElement.None;
        }

    }
}