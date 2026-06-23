using Assets._Scripts.ObjectsScripts.Camera;
using Assets._Scripts.ObjectsScripts.Player;
using Assets._Scripts.ObjectsScripts.StateMachines.Player;
using Assets.Input;
using ECM2;

namespace Assets._Scripts.ObjectsScripts.Player.Factorys
{
    public class PlayerStateMachineFactory
    {
        private PlayerMovement _playerMovement;
        private PlayerRotator _playerRotator;
        private PlayerGroundChecker _playerGroundChecker;
        private PlayerJumper _playerJumper;
        private AnimatorController _animatorController;
        private FallController _fallController;

        public UnitStateMachine Create(Character character, CameraController cameraController, 
            InputReader inputReader, PlayerMoveDirectionCalculator playerMoveDirectionCalculator)
        {
            _playerMovement = new PlayerMovement(character, inputReader, playerMoveDirectionCalculator);
            _playerRotator = new PlayerRotator(character, playerMoveDirectionCalculator);
            _playerGroundChecker = new PlayerGroundChecker(character);
            _playerJumper = new PlayerJumper(character);
            _animatorController = new AnimatorController(character.animator);
            _fallController = new FallController(character);

            UnitStateMachine playerStateMachine = new UnitStateMachine(_playerMovement, _playerRotator, inputReader, _playerGroundChecker, _playerJumper, _animatorController, _fallController);

            return playerStateMachine;
        }
    }
}
