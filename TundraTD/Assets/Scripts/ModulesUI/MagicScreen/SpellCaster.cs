
using System.Linq;
using Spells;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ModulesUI.MagicScreen
{
    /// <summary>
    /// A button orders grimoire to generate spell of current deck 
    /// </summary>
    public class SpellCaster : MonoBehaviour
    {
        private Camera _camera;
        private Vector3 _pointOnTap;
        private const int PlaceableLayer = 1 << 11 | 1 << 10;
        public void Start()
        {
            _camera = Camera.main;
        }

        private void CastSpellOnPosition(RaycastHit hitInfo)
        {
            Grimoire.TurnElementsIntoSpell(hitInfo);
            PlayerDeck.DeckElements.Clear();
        }
        
        public void Update()
        {
            if (Input.touchCount != 1 || EventSystem.current.IsPointerOverGameObject())
                return;
            
            var playerTouch = Input.GetTouch(0);
            var rayEnd = _camera.ScreenPointToRay(playerTouch.position);
            if (!Physics.Raycast(rayEnd, out var hitInfo, float.PositiveInfinity, PlaceableLayer)) return;
            
            CastSpellOnPosition(hitInfo);
        }
    }
}