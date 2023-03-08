using UnityEngine;
using UnityEngine.EventSystems;

namespace ModulesUI.MagicScreen
{
    public class HoldableDeckButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private const float PurgeThreshold = .5f;
        private float _holdingTime;
        private bool _isPurged;
        private bool _isHolding;

        private void Update()
        {
            if (_isHolding)
                _holdingTime += Time.deltaTime;

            if (_holdingTime >= PurgeThreshold && !_isPurged)
            {
                PurgeWholeDeck();
                _isPurged = true;
            }
            
        }

        private void PurgeWholeDeck()
        {
            PlayerDeck.DeckElements.Clear();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isHolding = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isHolding = false;
            _isPurged = false;
            _holdingTime = 0;
        }
    }
}
