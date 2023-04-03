using Assets.Scripts.Spells;
using Level;
using Spells;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ModulesUI.MagicScreen
{
    [RequireComponent(typeof(AudioSource))]
    public class HoldableElementButton : MonoBehaviour
    {
        [SerializeField] private BasicElement element;
        [SerializeField] private Image elementImage;
        private ElementCharge _charge;
        private AudioSource _elementAudioSource;

        private void Start()
        {
            _elementAudioSource = GetComponent<AudioSource>();
            _elementAudioSource.volume = GameParameters.EffectsVolumeModifier;
            _charge = new ElementCharge();
        }

        private void FixedUpdate()
        {
            _charge.DoTick(Time.deltaTime);
            elementImage.fillAmount = _charge.FillAmount;
        }

        public void AddElementToDeck()
        {
            if (PlayerDeck.DeckElements.Count == 3)
                return;
            _charge.CurrentCharges--;
            PlayerDeck.DeckElements.Add(element);
        }
    }
}