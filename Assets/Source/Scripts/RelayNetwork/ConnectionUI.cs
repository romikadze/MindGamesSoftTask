using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.RelayNetwork
{
    public class ConnectionUI : MonoBehaviour
    {
        [SerializeField] private InputField _inputField;
        private RelayConnection relayConnection;
        
        private void Start()
        {
            relayConnection = new RelayConnection();
        }
        
        public async void StartHost()
        {
            await relayConnection.StartHostWithRelay();
        }

        public async void StartClient()
        {
            await relayConnection.StartClientWithRelay(_inputField.text);
        }
    }
}