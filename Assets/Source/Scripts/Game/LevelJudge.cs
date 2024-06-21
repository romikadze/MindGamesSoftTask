using System.Collections.Generic;
using Source.Scripts.Game.CharacterControls;
using Source.Scripts.Game.ItemTransporting;
using Source.Scripts.RelayNetwork;
using Unity.Netcode;
using UnityEngine;

namespace Source.Scripts.Game
{
    public class LevelJudge : NetworkBehaviour
    {
        [SerializeField] private Field _templateField;
        [SerializeField] private Field _targetField;
        [SerializeField] private Field _storageField;
        [SerializeField] private List<Brick> _brickPrefabs;
        [SerializeField] private RelayConnection _relayConnection;
        [SerializeField] private Controller _playerPrefab;

        private void Awake()
        {
            _targetField.OnPlatePatternChange += CheckForLevelEnd;
            _relayConnection.OnStartHost += SpawnPlayer;
        }

        private void CheckForLevelEnd()
        {
            if (Field.MatchFields(_templateField, _targetField))
            {
                EndLevelServerRpc();
            }
        }
        
        private void SpawnPlayer()
        {
            if (NetworkManager.Singleton.IsServer)
            {
                Controller playerInstance = Instantiate(_playerPrefab);
                playerInstance.GetComponent<NetworkObject>().Spawn();   
            }
        }
        
        [ServerRpc(RequireOwnership = false)]
        private void EndLevelServerRpc()
        {
            EndLevelClientRpc();
            _targetField.SetLocked(true);
        }

        [ClientRpc]
        private void EndLevelClientRpc()
        {
            Debug.Log("Win");
        }
        
        public void StartLevel()
        {
            BrickFactory factory = new BrickFactory();
            List<Brick> bricks = new List<Brick>();
            bricks = factory.ShuffleBricks(factory.GenerateBrickList(
                new KeyValuePair<Brick, int>(_brickPrefabs[0], 5),
                new KeyValuePair<Brick, int>(_brickPrefabs[1], 4)));

            _templateField.FillWithListOfBricks(bricks);
            _templateField.SetLocked(true);

            bricks = factory.ShuffleBricks(bricks);
            _storageField.FillWithListOfBricks(bricks);
        }
    }
}