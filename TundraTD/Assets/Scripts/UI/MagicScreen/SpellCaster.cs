using System.Linq;
using Spells;
using UnityEngine;

namespace UI.MagicScreen
{
    /// <summary>
    /// A button orders grimoire to generate spell of current deck 
    /// </summary>
    public class SpellCaster : MonoBehaviour
    {
        public void OnButtonClick(Vector3 _positionOnMap)
        {
            Grimoire.TurnElementsIntoSpell(PlayerDeck.DeckElements.ToList(), _positionOnMap);
            PlayerDeck.DeckElements.Clear();
        }


        //For debug purposes only. Remove it before pulling
        public void Update()
        {
            
            if (Input.touchCount == 1 && ((Input.GetTouch(0).position.x < 320 || Input.GetTouch(0).position.x > 750) || ((Input.GetTouch(0).position.x > 320 || Input.GetTouch(0).position.x < 750) && Input.GetTouch(0).position.y > 100)))
            {
                print(Input.GetTouch(0).position);
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                OnButtonClick(ray.origin);
            }
        }
    }
}