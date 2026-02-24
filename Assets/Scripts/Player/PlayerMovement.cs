using Assets.Input;
using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerMovement : IDisposable
    {
        private CharacterController _characterController;
        private readonly InputReader _inputReader;
        private PlayerMoveDirectionCalculator _playerMoveDirectionCalculator;

        private bool _isMove;
        private Vector3 _direction;
        private float _speed = 5f;
        
        public PlayerMovement(CharacterController characterController, InputReader inputReader, PlayerMoveDirectionCalculator playerMoveDirectionCalculator)
        {
            _inputReader = inputReader;
            _characterController = characterController;
            _isMove = false;
            _playerMoveDirectionCalculator = playerMoveDirectionCalculator;

            _inputReader.OnMoved += SetIsMove;
        }

        public void Dispose()
        {
            _inputReader.OnMoved -= SetIsMove;
        }

        public void Move()
        {
            if (_isMove)
            {
                _direction = _playerMoveDirectionCalculator.GetMoveDirection();
                _characterController.Move(_direction * _speed * Time.deltaTime);
            }
        }

        private void SetIsMove(bool isMove) 
        {
            _isMove = isMove;
        }
    }
}
