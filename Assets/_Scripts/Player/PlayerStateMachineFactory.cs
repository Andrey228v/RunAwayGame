using Assets.Input;
using Assets.Scripts.Camera;
using Assets.Scripts.StateMachines.Player;
using ECM2;
using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerStateMachineFactory : IDisposable
    {
        private InputReader _inputReader;
        private PlayerMoveDirectionCalculator _playerMoveDirectionCalculator;
        private PlayerMovement _playerMovement;
        private PlayerRotator _playerRotator;
        private PlayerGroundChecker _playerGroundChecker;
        private PlayerJumper _playerJumper;
        private AnimatorController _animatorController;
        private FallController _fallController;

        public PlayerStateMachine Create(Character character, CameraController cameraController)
        {
            _inputReader = new InputReader();
            _playerMoveDirectionCalculator = new PlayerMoveDirectionCalculator(cameraController, _inputReader);
            _playerMovement = new PlayerMovement(character, _inputReader, _playerMoveDirectionCalculator);
            _playerRotator = new PlayerRotator(character, _playerMoveDirectionCalculator);
            _playerGroundChecker = new PlayerGroundChecker(character);
            _playerJumper = new PlayerJumper(character, _inputReader);
            _animatorController = new AnimatorController(character.animator);
            _fallController = new FallController(character);

            PlayerStateMachine playerStateMachine = new PlayerStateMachine(_playerMovement, _playerRotator, _inputReader, _playerGroundChecker, _playerJumper, _animatorController, _fallController);

            return playerStateMachine;
        }

        public void Dispose()
        {
            _animatorController.Dispose();
        }
    }
}
