using Assets.Input;
using Assets.Scripts.Player;
using Assets.Scripts.StateMachines.Player;
using ECM2;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.EnteryPoints
{
    public class GameEnteryPoint : MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        private PlayerRotator _playerRotator;
        private InputReader _inputReader;
        private PlayerGroundChecker _playerGroundChecker;
        private PlayerJumper _playerJumper;
        private Character _character;
        private AnimatorController _animatorController;
        private FallController _fallController;

        [Inject]
        public void Constructor(PlayerMovement playerMovement, 
            PlayerRotator playerRotator, InputReader inputReader,
            PlayerGroundChecker playerGroundChecker,
            PlayerJumper playerJumper, Character character, 
            AnimatorController animatorController, FallController fallController)
        {
            _playerMovement = playerMovement;
            _playerRotator = playerRotator;
            _inputReader = inputReader;
            _playerGroundChecker = playerGroundChecker;
            _playerJumper = playerJumper;
            _character = character;
            _animatorController = animatorController;
            _fallController = fallController;
        }

        private void Start()
        {
            new PlayerStateMachine(_playerMovement, _playerRotator, _inputReader, _playerGroundChecker, _playerJumper, _character, _animatorController, _fallController);
        }
    }
}
