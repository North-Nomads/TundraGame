using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JetBrains.Annotations;
using Spells;
using UnityEngine;

namespace ModulesUI.MagicScreen
{
    /// <summary>
    /// A static class that holds the player's deck and element icons
    /// </summary>
    public static class PlayerDeck
    {
        public static BasicElement CurrentMostElement;
        public static ObservableCollection<BasicElement> DeckElements { get; } = new ObservableCollection<BasicElement>();
        public static Dictionary<BasicElement, int> ElementsInDeck { get; } 
        public static Dictionary<BasicElement, Sprite> ElementIcons { get; }

        static PlayerDeck()
        {
            var loadedIcons = new Sprite[6];
            int i = 0;
            foreach (var element in (BasicElement[])Enum.GetValues(typeof(BasicElement)))
            {
                loadedIcons[i] = Resources.Load<Sprite>($"Elements/{element}");
                i++;
            }
            //var loadedIcons = Resources.LoadAll<Sprite>("Elements/");
            ElementIcons = new Dictionary<BasicElement, Sprite>
            {
                [BasicElement.None] = loadedIcons[0],
                [BasicElement.Fire] = loadedIcons[1],
                [BasicElement.Water] = loadedIcons[2],
                [BasicElement.Earth] = loadedIcons[3],
                [BasicElement.Lightning] = loadedIcons[4],
                [BasicElement.Air] = loadedIcons[5]
            };

            ElementsInDeck = new Dictionary<BasicElement, int>
            {
                [BasicElement.Air] = 0,
                [BasicElement.Earth] = 0,
                [BasicElement.Fire] = 0,
                [BasicElement.Lightning] = 0,
                [BasicElement.Water] = 0,
                [BasicElement.None] = 5
            };
        }
    }
    
}