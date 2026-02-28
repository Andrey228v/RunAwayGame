using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Input
{
    public class InputReader : IDisposable
    {
        private InputSystem_Actions _inputSystem;

        public event Action<Vector2> OnDirectionMoveChandged;
        public event Action<bool> OnMoved;
        public event Action OnJumped;

        public InputReader()
        {
            _inputSystem = new InputSystem_Actions();
            _inputSystem.Player.Enable();
            //_inputSystem.Player.Move.started += OnMove;
            _inputSystem.Player.Move.performed += OnMove;
            _inputSystem.Player.Move.canceled += OnMove;
            _inputSystem.Player.Jump.started += OnJump;
        }

        public void Dispose()
        {
            //_inputSystem.Player.Move.started -= OnMove;
            _inputSystem.Player.Move.performed -= OnMove;
            _inputSystem.Player.Move.canceled -= OnMove;
            _inputSystem.Player.Jump.started -= OnJump;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed == true)
            {
                OnDirectionMoveChandged?.Invoke(context.ReadValue<Vector2>());
                OnMoved?.Invoke(true);
            }
            else if (context.canceled == true)
            {
                OnDirectionMoveChandged?.Invoke(context.ReadValue<Vector2>());
                OnMoved?.Invoke(false);
            }
        }

        public void OnJump(InputAction.CallbackContext context) 
        {
            if(context.started == true)
            {
                OnJumped?.Invoke();
            }
        }
    }
}
