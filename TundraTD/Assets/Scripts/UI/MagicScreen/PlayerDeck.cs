using System.Collections.Generic;
using System.Collections.ObjectModel;
using Spells;
using UnityEngine;

namespace UI.MagicScreen
{
    /// <summary>
    /// A static class that holds the player's deck and element icons
    /// </summary>
    public static class PlayerDeck
    {
        public static ObservableCollection<BasicElement> DeckElements { get; } = new ObservableCollection<BasicElement>();
        public static Dictionary<BasicElement, Sprite> ElementIcons { get; }

        static PlayerDeck()
        {
            var loadedIcons = Resources.LoadAll<Sprite>("Elements/");
            ElementIcons = new Dictionary<BasicElement, Sprite>
            {
                [BasicElement.Air] = loadedIcons[1],
                [BasicElement.Earth] = loadedIcons[2],
                [BasicElement.Fire] = loadedIcons[3],
                [BasicElement.Lightning] = loadedIcons[4],
                [BasicElement.Water] = loadedIcons[5],
                [BasicElement.None] = null
            };
        }
    }
    
}