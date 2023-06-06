using System.Linq;
using Spells;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ModulesUI.MagicScreen
{
    /// <summary>
    /// A button orders grimoire to generate spell of current deck 
    /// </summary>
    public class SpellCaster : TundraCanvas
    {
        public override CanvasGroup CanvasGroup => CanvasGroup.MagicHUD;
        public override CanvasGroup BlockList => CanvasGroup.None;
        
        private Camera _camera;
        private const int PlaceableLayer = 1 << 11 | 1 << 10;
        private Touch _lastTouch;
        
        public void Start()
        {
            gameObject.SetActive(false);
            _camera = Camera.main;
            UIToggle.AllCanvases.Add(this);
        }

        private static void CastSpellOnPosition(RaycastHit hitInfo)
        {
            BasicElement core = PlayerDeck.DeckElements.FirstOrDefault() | PlayerDeck.DeckElements.ElementAtOrDefault(1);
            BasicElement addition = PlayerDeck.DeckElements.ElementAtOrDefault(2);
            var spell = MagicSpell.InstantiateSpellPrefab(core, addition);
            if (spell?.Cast(hitInfo) == true)
            {
                PlayerDeck.DeckElements.Clear();
            }
        }

        public void Update()
        {
            // Clicking over UI surface
            if (EventSystem.current.IsPointerOverGameObject())
            {
                _lastTouch.phase = TouchPhase.Ended;
                return;
            }

            if (Input.touchCount > 0)
            {
                _lastTouch = Input.GetTouch(0);
                return;
            }
            else if (Input.GetMouseButton(0))
            {
                _lastTouch = new Touch()
                {
                    position = Input.mousePosition
                };
                return;
            }

            // Prevent executing spell right after finger lifting after clicking the element 
            if (_lastTouch.phase == TouchPhase.Ended)
                return;
                
            var rayEnd = _camera.ScreenPointToRay(_lastTouch.position);
            if (!Physics.Raycast(rayEnd, out var hitInfo, float.PositiveInfinity, PlaceableLayer))
                return;
            
            CastSpellOnPosition(hitInfo);
        }

        public static void PerformDebugCast()
        {
            var rayEnd = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(rayEnd, out var hitInfo, float.PositiveInfinity, PlaceableLayer))
                return;
            
            CastSpellOnPosition(hitInfo);
        }
    }
}