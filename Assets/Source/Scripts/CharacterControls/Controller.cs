using Source.Scripts.Transporting;
using Unity.Netcode;
using UnityEngine;

namespace Source.Scripts.CharacterControls
{
    [RequireComponent(typeof(UserInput))]
    [RequireComponent(typeof(Rigidbody))]
    public class Controller : NetworkBehaviour
    {
        [SerializeField] private float _speed = 2;

        private UserInput _input;
        private Rigidbody _rigidbody;
        private IValuableItem _transportedPickupItem;
        private IPickupItemStorage _targetPickupItemStorage;
        private bool isTransporting;

        private void Awake()
        {
            _input = GetComponent<UserInput>();
            _rigidbody = GetComponent<Rigidbody>();
            _input.OnMove += MoveServerRpc;
            _input.OnAction += ActionServerRpc;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IPickupItemStorage pickupItemStorage))
                _targetPickupItemStorage = pickupItemStorage;
            if (other.TryGetComponent(out IHighlightItem highlight))
                highlight.Select();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IPickupItemStorage pickupItemStorage))
                if (_targetPickupItemStorage == pickupItemStorage)
                    _targetPickupItemStorage = null;
            if (other.TryGetComponent(out IHighlightItem highlight))
                highlight.DeSelect();
        }

        [ServerRpc(RequireOwnership = false)]
        private void MoveServerRpc(Vector3 direction)
        {
            if(!IsOwner)return;
                _rigidbody.AddForce(direction * _speed);
        }
        
        [ServerRpc(RequireOwnership = false)]
        private void ActionServerRpc()
        {
            if(!IsOwner)return;
            if (_targetPickupItemStorage == null) return;
            
            if (!isTransporting)
            {
                if (_targetPickupItemStorage.Drop(out IValuableItem pickupItem))
                {  
                    pickupItem.Pickup(transform, transform.position + Vector3.up);
                    _transportedPickupItem = pickupItem;
                    isTransporting = true;
                }
            }
            else if (isTransporting)
            {
                if (_targetPickupItemStorage.Store(_transportedPickupItem))
                {
                    _transportedPickupItem = null;
                    isTransporting = false;
                }
            }
        }
    }
}