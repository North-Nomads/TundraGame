using System;
using Level;
using UnityEngine;
using UnityEngine.EventSystems;

namespace City.Building
{
    /// <summary>
    /// A slot to build a new tower
    /// </summary>
    public class TowerPlacementSlot : MonoBehaviour
    {
        [SerializeField] private TowerPurchaseMenu purchaseMenu;
        [SerializeField] private int slotID;

        private Vector3 _bottomCentreBuildingAnchor;
        public bool IsOccupied { get; private set; }
        public int SlotID => slotID;

        public void BuildElementalTowerOnThisSlot(ElementalTower prefab)
        {
            // we define new spawn position higher than the anchor because unity defines axis in the center of a model
            var position = _bottomCentreBuildingAnchor + Vector3.up * prefab.transform.localScale.y / 2;
            Instantiate(prefab, position, Quaternion.identity);
            IsOccupied = true;
        }

        private void Start()
        {
            if (purchaseMenu is null)
                throw new NullReferenceException("No purchase menu was assigned for the placement slot");
            
            var position = transform.position;
            _bottomCentreBuildingAnchor = new Vector3(position.x, position.y + Mathf.Abs(transform.localScale.y / 2), position.z);
            IsOccupied = false;
        }

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            
            if (!PauseMode.IsGamePaused)
                CallPurchaseMenuOnEmptySlotClicked();
        }

        private void CallPurchaseMenuOnEmptySlotClicked()
        {
            purchaseMenu.gameObject.SetActive(true);
            purchaseMenu.SelectedSlotID = slotID;
        }
    }
}