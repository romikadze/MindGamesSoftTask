using UnityEngine;

namespace Source.Scripts.Game.CharacterControls
{
    public sealed class KeyboardInput : UserInput
    {
        [SerializeField] private KeyCode _moveForward;
        [SerializeField] private KeyCode _moveBack;
        [SerializeField] private KeyCode _moveRight;
        [SerializeField] private KeyCode _moveLeft;
        [SerializeField] private KeyCode _action;
        
        private void Update()
        {
            if(Input.GetKey(_moveForward)) OnMove.Invoke(Vector3.forward);
            if(Input.GetKey(_moveBack)) OnMove.Invoke(Vector3.back);
            if(Input.GetKey(_moveRight)) OnMove.Invoke(Vector3.right);
            if(Input.GetKey(_moveLeft)) OnMove.Invoke(Vector3.left);
            if(Input.GetKeyDown(_action)) OnAction.Invoke();
        }
    }
}
