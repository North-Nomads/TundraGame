using UnityEngine;

namespace ModulesUI.MagicScreen
{
    public class UpperButtonElements : MonoBehaviour
    {
        public GameObject[] Buttons;

        public DeckButton[] ElementScripts { get; set; } = new DeckButton[5];

        private void Start()
        {
            for (int i = 0; i < Buttons.Length; i++)
            {
                ElementScripts[i] = Buttons[i].GetComponent<DeckButton>();
            }
        }
    }
}