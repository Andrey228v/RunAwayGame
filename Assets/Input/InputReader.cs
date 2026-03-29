using Cysharp.Threading.Tasks;
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
        public event Action OnStartMove;
        public event Action OnStoped;
        //public event Action OnJumped;
        //public event Action OnJumpButtonDown;
        //public event Action OnJumpButtonUp;

        private bool _isJumpPressThisFrame = false;
        public bool IsJumpPress => _isJumpPressThisFrame;

        public InputReader()
        {
            _inputSystem = new InputSystem_Actions();
            _inputSystem.Player.Enable();
            _inputSystem.Player.Move.started += OnMove;
            _inputSystem.Player.Move.performed += OnMove;
            _inputSystem.Player.Move.canceled += OnMove;

            _inputSystem.Player.Jump.started += OnJump;
            _inputSystem.Player.Jump.performed += OnJump;
            _inputSystem.Player.Jump.canceled += OnJump;
        }

        public void Dispose()
        {
            _inputSystem.Player.Move.started -= OnMove;
            _inputSystem.Player.Move.performed -= OnMove;
            _inputSystem.Player.Move.canceled -= OnMove;
            _inputSystem.Player.Jump.started -= OnJump;
            _inputSystem.Player.Jump.performed -= OnJump;
            _inputSystem.Player.Jump.canceled -= OnJump;

            _inputSystem.Dispose();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.started == true)
            {
                OnStartMove?.Invoke();
            }
            else if (context.performed == true)
            {
                OnDirectionMoveChandged?.Invoke(context.ReadValue<Vector2>());
                OnMoved?.Invoke(true);
            }
            else if (context.canceled == true)
            {
                OnDirectionMoveChandged?.Invoke(context.ReadValue<Vector2>());
                OnMoved?.Invoke(false);
                OnStoped?.Invoke();
            }
        }

        public void OnJump(InputAction.CallbackContext context) 
        {
            if(context.started)
            {
                //OnJumped?.Invoke();
                _isJumpPressThisFrame = true;
            }
            else if( context.canceled)
            {
                //OnJumpButtonUp?.Invoke();
            }
        }

        public void ResetJump()  // Вызывать после прыжка
        {
            _isJumpPressThisFrame = false;
        }
    }
}
