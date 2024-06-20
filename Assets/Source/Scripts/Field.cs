using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts
{
    public class Field : MonoBehaviour
    {
        public Action OnPlatePatternChange;
        
        [SerializeField] private bool _isLocked;
        [SerializeField] private List<Plate> _plates;

        private void Start()
        {
            foreach (var plate in _plates)
            {
                if (_isLocked)
                {
                    plate.UpdateLockedStatus(_isLocked);
                }

                plate.OnStorageChanged += OnPlatePatternChange;
            }
        }
        
        private List<ItemValue> GetPlatePattern()
        {
            List<ItemValue> values = new List<ItemValue>();
            foreach (var plate in _plates)
            {
                values.Add(plate.GetStoredItemValue());
            }

            return values;
        }

        public void SetLocked(bool locked)
        {
            _isLocked = locked;
            
            foreach (var plate in _plates)
            {
                if (_isLocked)
                {
                    plate.UpdateLockedStatus(_isLocked);
                }
            }
        }
        
        public void FillWithListOfBricks(List<Brick> bricks)
        {
            int length = bricks.Count > _plates.Count ? _plates.Count : bricks.Count;
            for (int i = 0; i < length; i++)
            {
                _plates[i].Store(Instantiate(bricks[i]));
            }
        }

        public static bool MatchFields(Field field1, Field field2)
        {
            List<ItemValue> field1Values = field1.GetPlatePattern();
            List<ItemValue> field2Values = field2.GetPlatePattern();

            for (int i = 0; i < field1Values.Count; i++)
            {
                if (field1Values[i] != field2Values[i]) return false;
            }

            return true;
        }
    }
}