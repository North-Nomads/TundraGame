using Level;
using Spells;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ModulesUI.MagicScreen
{
    [RequireComponent(typeof(AudioSource))]
    public class HoldableElementButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private BasicElement element;
        private AudioSource _elementAudioSource;
        private const float AddListThreshold = .3f;
        private float _holdingTime;
        private bool _isFilled;
        private bool _isHolding;
        private bool _wasHolding;

        private void Start()
        {
            _elementAudioSource = GetComponent<AudioSource>();
            _elementAudioSource.volume = GameParameters.EffectsVolumeModifier;
        }

        private void Update()
        {
            if (_isHolding)
                _holdingTime += Time.deltaTime;

            if (_holdingTime >= AddListThreshold && !_isFilled)
            {
                var amount = 5 - PlayerDeck.DeckElements.Count;
                Debug.Log(PlayerDeck.DeckElements.Count);
                for (int i = 0; i < amount; i++)
                    PlayerDeck.DeckElements.Add(element);

                _holdingTime = 0;
                _isFilled = true;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _elementAudioSource.PlayOneShot(_elementAudioSource.clip);
            _isHolding = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isHolding = false;
            _isFilled = false;
            _holdingTime = 0;
        }
        
        public void AddElementToDeck()
        {
            if (PlayerDeck.DeckElements.Count == 5)
                return;
            
            PlayerDeck.DeckElements.Add(element);
        }
    }
}