using System;
using Source.Scripts.RelayNetwork;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private LevelJudge _levelJudge;
        [SerializeField] private RelayConnection _relayConnection;

        [SerializeField] private Button _spawnButton;

        private void Awake()
        {
            _relayConnection.OnStartClient += DisableSpawnButton;
            _spawnButton.onClick.AddListener(
                () =>
                {
                    _levelJudge.StartLevel();
                    DisableSpawnButton();
                });
        }

        private void DisableSpawnButton()
        {
            _spawnButton.gameObject.SetActive(false);
        }
    }
}