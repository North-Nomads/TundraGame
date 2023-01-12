using System;
using Spells;
using UnityEngine;

namespace City.Building
{
    public class TowerPurchaseMenu : MonoBehaviour
    {
        [SerializeField] private Architect architect; 
        public int SelectedTowerID { get; set; }
        
        private void Start()
        {
            gameObject.SetActive(false);
        }
        
        public void BuildTower(string elementName)
        {
            BasicElement element = TransformStringIntoBasicElement(elementName);
            if (element == BasicElement.None)
                throw new Exception("No element found for tower"); 
            architect.BuildNewTower(SelectedTowerID, element);
            
            BasicElement TransformStringIntoBasicElement(string rawElementName)
            {
                var loweredElementName = rawElementName.ToLower();
                switch (loweredElementName)
                {
                    case "water":
                        return BasicElement.Water;
                    case "earth":
                        return BasicElement.Earth;
                    case "fire":
                        return BasicElement.Fire;
                    case "lightning":
                        return BasicElement.Lightning;
                    case "air":
                        return BasicElement.Air;
                    default:
                        return BasicElement.None;
                }
            }
        }

        public void ClosePurchaseMenu()
        {
            gameObject.SetActive(false);
        }
    }
}