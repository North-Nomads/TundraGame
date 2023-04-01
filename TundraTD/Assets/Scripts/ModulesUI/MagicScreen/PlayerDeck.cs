using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Assets.Scripts.Spells;
using Spells;
using UnityEngine;

namespace ModulesUI.MagicScreen
{
    /// <summary>
    /// A static class that holds the player's deck and element icons
    /// </summary>
    public static class PlayerDeck
    {
        public static ObservableCollection<BasicElement> DeckElements { get; } = new ObservableCollection<BasicElement>();
        
        public static Dictionary<BasicElement, Sprite> ElementIcons { get; }

        // TODO: talk about implementation of this.
        public static ElementSlot[] ElementSlots { get; }

        static PlayerDeck()
        {
            ElementIcons = new Dictionary<BasicElement, Sprite>();
            foreach (var element in (BasicElement[])Enum.GetValues(typeof(BasicElement)))
                ElementIcons.Add(element, Resources.Load<Sprite>($"Elements/{element}"));
        }
    }
    
}