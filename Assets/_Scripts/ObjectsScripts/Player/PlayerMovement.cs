using Assets.Input;
using ECM2;
using System;
using UnityEngine;

namespace Assets._Scripts.ObjectsScripts.Player
{
    public class PlayerMovement
    {
        private Character _character;
        private readonly InputReader _inputReader;
        private PlayerMoveDirectionCalculator _playerMoveDirectionCalculator;
       
        private bool _isMove;
        private Vector3 _direction;
        
        public bool IsMove => _isMove;

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
            _direction = _playerMoveDirectionCalculator.GetMoveDirection();
            _character.SetMovementDirection(_direction);
        }

        public void Stop()
        {
            _character.SetMovementDirection(Vector3.zero);
        }

        private void SetIsMove(bool isMove) 
        {
            _isMove = isMove;
        }
    }
}
