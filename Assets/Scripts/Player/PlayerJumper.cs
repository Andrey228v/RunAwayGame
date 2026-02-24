using Assets.Input;
using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerJumper : IDisposable
    {
        private int _forceX = 5;
        private int _forceY = 10;

        private CharacterController _characterController;
        private readonly InputReader _inputReader;
        private PlayerMoveDirectionCalculator _playerMoveDirectionCalculator;

        public PlayerJumper(CharacterController characterController, InputReader inputReader, PlayerMoveDirectionCalculator playerMoveDirectionCalculator)
        {
            _characterController = characterController;
            _inputReader = inputReader;
            _playerMoveDirectionCalculator = playerMoveDirectionCalculator;

            _inputReader.OnJumped += Jump;
        }

        public void Dispose()
        {
            _inputReader.OnJumped -= Jump;
        }

        public void Jump()
        {
            //_playerMoveDirectionCalculator.SetJumpForce(new Vector3(_forceX, _forceY, 0f));
        }
    }
}
