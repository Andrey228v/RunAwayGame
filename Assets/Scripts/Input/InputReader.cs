using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Input
{
    public class InputReader : IDisposable
    {
        public Action OnLeftButtonClick;
        public event Action<Vector2> OnDirectionMoveChandged;
        public event Action<bool> OnMoved;
        //public event Action OnStoped;

        private InputSystem_Actions _inputSystem;
        private InputAction _leftButtonMoveAction;
        private InputAction _rightButtonMoveAction;
        private PlayerInput _playerInput;

        public InputReader()
        {
            _inputSystem = new InputSystem_Actions();
            _inputSystem.Player.Enable();
            _inputSystem.Player.Move.started += OnMove;
            _inputSystem.Player.Move.performed += OnMove;
            _inputSystem.Player.Move.canceled += OnMove;
        }

        public void Dispose()
        {
            _inputSystem.Player.Move.started -= OnMove;
            _inputSystem.Player.Move.performed -= OnMove;
            _inputSystem.Player.Move.canceled -= OnMove;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Debug.Log("EVENT TEST");

            if (context.started == true)
            {
                //OnDirectionMoveChandged?.Invoke(context.ReadValue<Vector2>());
                //OnMoved?.Invoke(true);
            }
            else if (context.performed == true)
            {
                OnDirectionMoveChandged?.Invoke(context.ReadValue<Vector2>());
                OnMoved?.Invoke(true);
                Debug.Log(context.ReadValue<Vector2>());
            }
            else if (context.canceled == true)
            {
                OnDirectionMoveChandged?.Invoke(context.ReadValue<Vector2>());
                OnMoved?.Invoke(false);
            }
        } 
    }
}
