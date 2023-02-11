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
        private float _slotHeight;
        private Vector3 _bottomCentreBuildingAnchor;
        public bool IsOccupied { get; private set; }
        public int SlotID => slotID;

        public void BuildElementalTowerOnThisSlot(ElementalTower prefab)
        {
            // we define new spawn position higher than the anchor because unity defines axis in the center of a model
            Instantiate(prefab, _bottomCentreBuildingAnchor, Quaternion.identity);
            IsOccupied = true;
        }

        private void Start()
        {
            if (purchaseMenu is null)
                throw new NullReferenceException("No purchase menu was assigned for the placement slot");


            var meshRenderers = GetComponentsInChildren<MeshRenderer>();
            _slotHeight = meshRenderers[0].bounds.size.y - meshRenderers[1].bounds.size.y;
            
            var position = transform.position;
            _bottomCentreBuildingAnchor = new Vector3(position.x, position.y + Mathf.Abs(_slotHeight / 2), position.z);
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