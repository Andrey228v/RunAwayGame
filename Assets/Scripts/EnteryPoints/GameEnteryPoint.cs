using Assets.Input;
using Assets.Scripts.Player;
using Assets.Scripts.StateMachines.Player;
using Cysharp.Threading.Tasks.Triggers;
using ECM2;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.EnteryPoints
{
    public class GameEnteryPoint : MonoBehaviour
    {
        private Test _test;
        private PlayerMovement _playerMovement;
        private PlayerRotator _playerRotator;
        private PlayerStateMachine _playerStateMachine;
        private InputReader _inputReader;
        private PlayerMoveDirectionCalculator _playerMoveDirectionCalculator;
        private PlayerGroundChecker _playerGroundChecker;
        private PlayerGravityController _playerGravityController;
        private PlayerJumper _playerJumper;
        private Character _character;
        private AnimatorController _animatorController;

        [Inject]
        public void Constructor(Test test, PlayerMovement playerMovement, 
            PlayerRotator playerRotator, InputReader inputReader, PlayerMoveDirectionCalculator playerMoveDirectionCalculator,
            PlayerGroundChecker playerGroundChecker, PlayerGravityController playerGravityController,
            PlayerJumper playerJumper, Character character, AnimatorController animatorController)
        {
            _test = test;
            _playerMovement = playerMovement;
            _playerRotator = playerRotator;
            _inputReader = inputReader;
            _playerMoveDirectionCalculator = playerMoveDirectionCalculator;
            _playerGroundChecker = playerGroundChecker;
            _playerGravityController = playerGravityController;
            _playerJumper = playerJumper;
            _animatorController = animatorController;
            _character = character;

            //character.AwakeAsync();
            
            
        }

        private void Start()
        {
            _playerStateMachine = new PlayerStateMachine(_playerMovement, _playerRotator, _inputReader, _playerMoveDirectionCalculator, _playerGroundChecker, _playerGravityController, _playerJumper, _character, _animatorController);
        }

    }
}
