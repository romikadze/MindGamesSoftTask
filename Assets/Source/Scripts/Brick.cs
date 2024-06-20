using UnityEngine;

namespace Source.Scripts
{
    public class Brick : MonoBehaviour, IValuableItem
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
            transform.parent = newParent;
        }

        public ItemValue GetItemValue()
        {
            return _brickType;
        }
    }
}