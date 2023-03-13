using System;
using City.Building;
using Spells;

namespace ModulesUI.Building
{
    public class TowerPurchaseMenu : TundraCanvas
    {
        public override CanvasGroup CanvasGroup => CanvasGroup.Building;
        public override CanvasGroup BlockList => CanvasGroup.MagicHUD | CanvasGroup.Portal | CanvasGroup.City | CanvasGroup.Building | CanvasGroup.Camera;
        
        private int _selectedSlot;
        
        public override void ExecuteOnOpening()
        {
            gameObject.SetActive(true);
        }

        public override void ExecuteOnClosing()
        {
            gameObject.SetActive(false);
        }

        private void Start()
        {
            gameObject.SetActive(false);
            UIToggle.AllCanvases.Add(this);
        }

        public void BuildTower(string elementName)
        {
            BasicElement element = (BasicElement)Enum.Parse(typeof(BasicElement), elementName, true);
            if (element == BasicElement.None)
                throw new ArgumentException("No element found for tower");
            Architect.BuildNewTower(_selectedSlot, element);
            ClosePurchaseMenu();
        }

        public void ClosePurchaseMenu()
        {
            UIToggle.HandleCanvasClosing(this);
        }

        public void SetPurchaseID(int slotID)
        {
            _selectedSlot = slotID;
        }
    }
}