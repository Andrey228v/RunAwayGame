using Assets.Input;
using ECM2;
using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerMovement : IDisposable
    {
        private Character _character;
        private readonly InputReader _inputReader;
        private PlayerMoveDirectionCalculator _playerMoveDirectionCalculator;

        private bool _isMove;
        private Vector3 _direction;
        private float _speed = 5f;
        
        public PlayerMovement(Character character, InputReader inputReader, PlayerMoveDirectionCalculator playerMoveDirectionCalculator)
        {
            _inputReader = inputReader;
            _character = character;
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
                _character.SetMovementDirection(_direction);
            }
        }

        private void SetIsMove(bool isMove) 
        {
            _isMove = isMove;
        }
    }
}
