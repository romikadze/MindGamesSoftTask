using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.RelayNetwork
{
    public class ConnectionUI : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _buttonHost;
        [SerializeField] private Button _buttonClient;
        [SerializeField] private RelayConnection _relayConnection;

        private void Awake()
        {
            _relayConnection.OnStartClient += DisableUiElements;
            _relayConnection.OnStartHost += DisableUiElements;
            _buttonHost.onClick.AddListener(async () =>
            {
                await _relayConnection.StartHostWithRelay();
            });
            
            _buttonClient.onClick.AddListener(async () =>
            {
                await _relayConnection.StartClientWithRelay(_inputField.text);
            });
        }

        private void DisableUiElements()
        {
            _buttonHost.gameObject.SetActive(false);
            _buttonClient.gameObject.SetActive(false);
            _inputField.gameObject.SetActive(false);
        }
    }
}