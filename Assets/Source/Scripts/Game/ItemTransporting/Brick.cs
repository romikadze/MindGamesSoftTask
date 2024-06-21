using Unity.Netcode;
using UnityEngine;

namespace Source.Scripts.Game.ItemTransporting
{
    public class Brick : NetworkBehaviour, IValuableItem
    {
        [SerializeField] private Color _selectColor;
        [SerializeField] private ItemValue _brickType;
        private Color _startColor;
        private Material _material;

        private void Awake()
        {
            _material = GetComponent<MeshRenderer>().material;
            _startColor = _material.color;
        }

        public void Select()
        {
            _material.color = _selectColor;
        }

        public void DeSelect()
        {
            _material.color = _startColor;
        }

        public void Pickup(Transform newParent, Vector3 newPosition)
        {
            transform.position = newPosition;
            NetworkObject.TrySetParent(newParent);
        }

        public ItemValue GetItemValue()
        {
            return _brickType;
        }
    }
}