using System;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Assets.Input
{
    public class InputReader : IDisposable
    {
        public Action OnLeftButtonClick;
        public event Action<Vector2> OnDirectionMoveChandged;
        public event Action OnMoved;
        public event Action OnStoped;

        private InputSystem_Actions _inputSystem;
        private InputAction _leftButtonMoveAction;
        private InputAction _rightButtonMoveAction;
        private PlayerInput _playerInput;

        public InputReader()
        {
            _inputSystem = new InputSystem_Actions();
            _inputSystem.Player.Enable();
            _inputSystem.Player.Move.performed += OnMove;
        }

        public void Dispose()
        {
            _inputSystem.Player.Move.performed -= OnMove;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Debug.Log("EVENT TEST");

            if (context.started == true)
            {
                OnDirectionMoveChandged?.Invoke(context.ReadValue<Vector2>());
                OnMoved?.Invoke();
            }
            else if (context.performed == true)
            {
                OnDirectionMoveChandged?.Invoke(context.ReadValue<Vector2>());
            }
            else if (context.canceled == true)
            {
                OnDirectionMoveChandged?.Invoke(context.ReadValue<Vector2>());
                OnStoped?.Invoke();
            }
        }
    }
}
