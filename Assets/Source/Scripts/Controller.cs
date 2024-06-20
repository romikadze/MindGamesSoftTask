using UnityEngine;

namespace Source.Scripts
{
    [RequireComponent(typeof(UserInput))]
    [RequireComponent(typeof(Rigidbody))]
    public class Controller : MonoBehaviour
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
            _input.OnMove += Move;
            _input.OnAction += Action;
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

        private void Move(Vector3 direction)
        {
            _rigidbody.AddForce(direction * _speed);
        }

        private void Action()
        {
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