using Spells;
using UnityEngine;
using UnityEngine.UI;

namespace ModulesUI.MagicScreen
{
    /// <summary>
    /// A filler that connects the deck and the elements
    /// </summary>
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(AudioSource))]
    public class ElementButton : MonoBehaviour
    {
        [SerializeField] private BasicElement element;
        private AudioSource _elementAudioSource;

        private void Start()
        {
            GetComponent<Image>();
            _elementAudioSource = GetComponent<AudioSource>();
        }

        public void OnButtonClick()
        {
            if (PlayerDeck.DeckElements.Count == 5)
                return;
            
            _elementAudioSource.PlayOneShot(_elementAudioSource.clip);
            PlayerDeck.DeckElements.Add(element);
        }
    }
}