
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
        private Camera cam;
        private Vector3 _pointOnTap;
        private const int PlaceableLayer = 1 << 11 | 1 << 10;
        public void Start()
        {
            cam = Camera.main;
        }
        public void CastSpellOnPosition(Vector3 _positionOnMap)
        {
            Grimoire.TurnElementsIntoSpell(PlayerDeck.DeckElements.ToList(), _positionOnMap);
            PlayerDeck.DeckElements.Clear();
        }


        public void Update()
        {
            if (Input.touchCount != 1)
                return;
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Touch _playerTouch = Input.GetTouch(0);
                var rayEnd = cam.ScreenPointToRay(_playerTouch.position);
                if (Physics.Raycast(rayEnd, out var hitInfo, float.PositiveInfinity, PlaceableLayer))
                    _pointOnTap = hitInfo.point;
                    CastSpellOnPosition(_pointOnTap);
            }
            // Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            // OnButtonClick(ray.origin);
        }
    }
}