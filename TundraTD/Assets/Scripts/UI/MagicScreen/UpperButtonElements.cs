using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MagicScreen
{
    public class UpperButtonElements : MonoBehaviour
    {
        public GameObject[] Buttons;

        public UpperButton[] ElementScripts { get; set; } = new UpperButton[5];

        private void Start()
        {
            for (int i = 0; i < Buttons.Length; i++)
            {
                ElementScripts[i] = Buttons[i].GetComponent<UpperButton>();
            }
        }
    }
}