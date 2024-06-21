using UnityEngine;

namespace Source.Scripts.Game.ItemTransporting
{
    public interface IPickupItem : IHighlightItem
    {
        public void Pickup(Transform newParent, Vector3 newPosition);
    }
}