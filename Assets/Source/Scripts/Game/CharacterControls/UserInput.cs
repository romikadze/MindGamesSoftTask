using System;
using UnityEngine;

namespace Source.Scripts.Game.CharacterControls
{
    public class UserInput : MonoBehaviour
    {
        public Action<Vector3> OnMove; 
        public Action OnAction; 
    }
}