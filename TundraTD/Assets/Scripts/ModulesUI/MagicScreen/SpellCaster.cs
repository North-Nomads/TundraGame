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

        private void CastSpellOnPosition(RaycastHit hitInfo) => Grimoire.TurnElementsIntoSpell(hitInfo);
        
        
        public void Update()
        {
            if (Input.touchCount == 0)
                return;
            
            var playerTouch = Input.GetTouch(0);

            // Clicking over level surface
            if (!(EventSystem.current.IsPointerOverGameObject(playerTouch.fingerId) || EventSystem.current.IsPointerOverGameObject(playerTouch.fingerId)))
            {
                // Prevent executing spell right after finger lifting after clicking the element 
                if (playerTouch.phase == TouchPhase.Ended)
                    return;
                
                Debug.Log("Executing spell");
                var rayEnd = _camera.ScreenPointToRay(playerTouch.position);
                if (!Physics.Raycast(rayEnd, out var hitInfo, float.PositiveInfinity, PlaceableLayer)) return;
            
                CastSpellOnPosition(hitInfo);
            }
        }
    }
}