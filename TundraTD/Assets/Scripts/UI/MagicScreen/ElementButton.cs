﻿using Spells;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MagicScreen
{
    /// <summary>
    /// A filler that connects the deck and the elements
    /// </summary>
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(AudioSource))]
    public class ElementButton : MonoBehaviour
    {
        [SerializeField] private BasicElement element;
        [SerializeField] private DeckButtons buttonsHolder;
        [SerializeField] private Image elementIcon;
        private AudioSource _elementAudioSource;

        private void Start()
        {
            GetComponent<Image>();
            _elementAudioSource = GetComponent<AudioSource>();
        }

        public void OnButtonClick()
        {
            _elementAudioSource.PlayOneShot(_elementAudioSource.clip);
            PlayerDeck.DeckElements.Add(element);
            
            /*var empty = Array.Find(buttonsHolder.Buttons, x => x.Element == BasicElement.None);
            if (empty == null) return;
            
            empty.ElementIcon.sprite = elementIcon.sprite;
            empty.Element = element;*/
        }
    }
}