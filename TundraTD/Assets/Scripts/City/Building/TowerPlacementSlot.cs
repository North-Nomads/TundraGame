using System;
using Level;
using ModulesUI;
using ModulesUI.Building;
using UnityEngine;
using UnityEngine.EventSystems;

namespace City.Building
{
    /// <summary>
    /// A slot to build a new tower
    /// </summary>
    public class TowerPlacementSlot : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TowerPurchaseMenu purchaseMenu;
        [SerializeField] private int slotID;
        [SerializeField] private float levelTowerSizeModifier = 1;
        private float _slotHeight;
        private Vector3 _bottomCentreBuildingAnchor;
        private AudioSource _soundEffect;
        private Collider _hitCollider;
        public bool IsOccupied { get; private set; }
        public int SlotID => slotID;

        public void BuildElementalTowerOnThisSlot(ElementalTower prefab)
        {
            // we define new spawn position higher than the anchor because unity defines axis in the center of a model
            var tower = Instantiate(prefab, _bottomCentreBuildingAnchor, Quaternion.identity);
            tower.transform.localScale *= levelTowerSizeModifier;
            _soundEffect.Play();
            IsOccupied = true;
            _hitCollider.enabled = false;
        }

        private void Start()
        {
            if (purchaseMenu is null)
                throw new NullReferenceException("No purchase menu was assigned for the placement slot");

            _hitCollider = GetComponent<Collider>();

            var meshRenderers = GetComponentsInChildren<MeshRenderer>();
            _slotHeight = meshRenderers[0].bounds.size.y - meshRenderers[1].bounds.size.y;
            
            var position = transform.position;
            _bottomCentreBuildingAnchor = new Vector3(position.x, position.y + Mathf.Abs(_slotHeight / 2), position.z);
            _soundEffect = GetComponent<AudioSource>();
            _soundEffect.volume = GameParameters.EffectsVolumeModifier;
            IsOccupied = false;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!UIToggle.TryOpenCanvas(purchaseMenu)) return;
            
            purchaseMenu.SetPurchaseID(slotID);
        }
    }
}