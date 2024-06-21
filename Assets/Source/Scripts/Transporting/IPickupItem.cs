using UnityEngine;

namespace Source.Scripts.Transporting
{
    public interface IPickupItem : IHighlightItem
    {
        public void Pickup(Transform newParent, Vector3 newPosition);
    }
}