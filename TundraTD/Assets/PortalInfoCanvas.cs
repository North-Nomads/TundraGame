using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalInfoCanvas : MonoBehaviour
{
    public Image[] MobCards;

    public void LoadImagesInCards(Sprite[] images)
    {
        for (int i = 0; i < MobCards.Length; i++)
        {
            MobCards[i].sprite = images[i];
        }
    }
}
