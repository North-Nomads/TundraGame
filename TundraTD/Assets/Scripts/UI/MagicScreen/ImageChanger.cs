using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spells;
using System.Linq;

namespace UI.MagicScreen
{
    public class ImageChanger : MonoBehaviour
    {
        [SerializeField] private BasicElement element;
        [SerializeField] private UpperButtonElements buttonsHolder;
        private Image elementIcon;

        private void Start()
        {
            elementIcon = GetComponent<Image>();
        }


        public void OnButtonClick()
        {
            var empty = buttonsHolder.ElementScripts.FirstOrDefault(x => x.Element == BasicElement.None);
            if (empty != null)
            {
                empty.ElementIcon.sprite = elementIcon.sprite;
                empty.Element = element;
            }
        }
    }
}