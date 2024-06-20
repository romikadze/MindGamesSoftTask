using System;
using UnityEngine;

namespace Source.Scripts
{
    public class Plate : MonoBehaviour, IPickupItemStorage
    {
        public Action OnStorageChanged;
        
        [SerializeField] private Transform _storePoint;
        [SerializeField] private Transform _body;
        [SerializeField] private Color _selectColor;

        private IValuableItem _storedItem;
        private Color _startColor;
        private Material _material;
        private bool _isLocked;

        private void Awake()
        {
            _material = _body.GetComponent<MeshRenderer>().material;
            _startColor = _material.color;
        }

        public bool Store(IValuableItem pickupItem)
        {
            if (_isLocked) return false;
            if (_storedItem != null) return false;
            _storedItem = pickupItem;
            pickupItem.Pickup(_storePoint, _storePoint.position);
            OnStorageChanged?.Invoke();
            return true;
        }

        public bool Drop(out IValuableItem pickupItem)
        {
            if (_isLocked)
            {
                pickupItem = null;
                return false;
            }

            if (_storedItem == null)
            {
                pickupItem = null;
                return false;
            }

            pickupItem = _storedItem;
            _storedItem = null;
            OnStorageChanged?.Invoke();
            return true;
        }

        public void Select()
        {
            if (_isLocked) return;
            _material.color = _selectColor;
        }

        public void DeSelect()
        {
            if (_isLocked) return;
            _material.color = _startColor;
        }

        public void UpdateLockedStatus(bool value)
        {
            _isLocked = value;
        }

        public ItemValue GetStoredItemValue()
        {
            if (_storedItem == null) return ItemValue.None;
            return _storedItem.GetItemValue();
        }
    }
}