using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Source.Scripts
{
    public class LevelJudge : MonoBehaviour
    {
        [SerializeField] private Field _templateField;
        [SerializeField] private Field _targetField;
        [SerializeField] private Field _storageField;

        [SerializeField] private List<Brick> _brickPrefabs; 

        private void Awake()
        {
            _targetField.OnPlatePatternChange += CheckForLevelEnd;
        }

        private void Start()
        {
            StartLevel();
        }

        private void CheckForLevelEnd()
        {
            if (Field.MatchFields(_templateField, _targetField))
            {
                Debug.Log("Win");
                EndLevel();
            }
        }

        private void StartLevel()
        {
            BrickFactory factory = new BrickFactory();
            List<Brick> bricks = new List<Brick>();
            bricks = factory.ShuffleBricks(factory.GenerateBrickList(
                new KeyValuePair<Brick, int>(_brickPrefabs[0], 5),
                new KeyValuePair<Brick, int>(_brickPrefabs[1], 4)));
            
            _templateField.FillWithListOfBricks(bricks);
            
            bricks = factory.ShuffleBricks(bricks);
            _storageField.FillWithListOfBricks(bricks);
   
        }

        private void EndLevel()
        {
            _targetField.SetLocked(true);
        }
    }
}