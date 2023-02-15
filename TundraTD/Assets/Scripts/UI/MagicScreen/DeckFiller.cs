using Spells;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MagicScreen
{
    /// <summary>
    /// A filler that connects the deck and the elements
    /// </summary>
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(AudioSource))]
    public class DeckFiller : MonoBehaviour
    {
        [SerializeField] private BasicElement element;
        [SerializeField] private UpperButtonElements buttonsHolder;
        private Image _elementIcon;
        private AudioSource _elementAudioSource;

        private void Start()
        {
            _elementIcon = GetComponent<Image>();
            _elementAudioSource = GetComponent<AudioSource>();
        }
        [SerializeField] private Image elementIcon;

        public void OnButtonClick()
        {
            _elementAudioSource.Play();
            var empty = System.Array.Find(buttonsHolder.ElementScripts, x => x.Element == BasicElement.None);
            if (empty == null) return;
            
            empty.ElementIcon.sprite = elementIcon.sprite;
            empty.Element = element;
        }
    }
}