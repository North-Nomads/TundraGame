using Spells;
using System;
using UnityEngine;

namespace City.Building
{
    public class TowerPurchaseMenu : MonoBehaviour
    {
        public int SelectedSlotID { get; set; }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void BuildTower(string elementName)
        {
            BasicElement element = (BasicElement)Enum.Parse(typeof(BasicElement), elementName, true);
            if (element == BasicElement.None)
                throw new ArgumentException("No element found for tower");
            Architect.BuildNewTower(SelectedSlotID, element);
        }

        public void ClosePurchaseMenu()
        {
            gameObject.SetActive(false);
        }
    }
}