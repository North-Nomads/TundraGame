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
        private Vector3 _pointOnTap;
        private const int PlaceableLayer = 1 << 11 | 1 << 10;
        
        public void Start()
        {
            gameObject.SetActive(false);
            _camera = Camera.main;
            UIToggle.AllCanvases.Add(this);
        }

        private static void CastSpellOnPosition(RaycastHit hitInfo) => Grimoire.TurnElementsIntoSpell(hitInfo);

        public void Update()
        {
            if (Input.touchCount == 0)
                return;
            
            var playerTouch = Input.GetTouch(0);

            // Clicking over UI surface
            if (EventSystem.current.IsPointerOverGameObject(playerTouch.fingerId)) return;
            
            // Prevent executing spell right after finger lifting after clicking the element 
            if (playerTouch.phase == TouchPhase.Ended)
                return;
                
            var rayEnd = _camera.ScreenPointToRay(playerTouch.position);
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