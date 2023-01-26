using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseElements : MonoBehaviour
{
    public GameObject[] Buttons;
    public Image[] elementIcons = new Image[5];

    private void Start()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            elementIcons[i] = Buttons[i].GetComponent<Image>();
        }
    }

}
