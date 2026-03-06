using Assets.Input;
using Assets.Scripts.SaveLoad;
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
        private ISaveSystem _saveSystem;
        private PlayerData _playerData;
        
        public bool IsMove => _isMove;

        public PlayerMovement(Character character, InputReader inputReader, PlayerMoveDirectionCalculator playerMoveDirectionCalculator,
            ISaveSystem saveSystem, PlayerData playerData)
        {
            _inputReader = inputReader;
            _character = character;
            _isMove = false;
            _playerMoveDirectionCalculator = playerMoveDirectionCalculator;
            _saveSystem = saveSystem;
            _playerData = playerData;

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
