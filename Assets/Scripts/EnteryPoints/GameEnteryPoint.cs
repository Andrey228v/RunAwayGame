using Assets.Input;
using Assets.Scripts.Player;
using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.StateMachines.Player;
using ECM2;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.EnteryPoints
{
    public class GameEnteryPoint : MonoBehaviour // Вместо MonoBehaviour протестировать IStartable....
    {
        private PlayerMovement _playerMovement;
        private PlayerRotator _playerRotator;
        private InputReader _inputReader;
        private PlayerGroundChecker _playerGroundChecker;
        private PlayerJumper _playerJumper;
        private Character _character;
        private AnimatorController _animatorController;
        private FallController _fallController;
        private PlayerData _playerData;
        private StartPoint _startPoint;

        private ISaveSystem _saveSystem;

        [Inject]
        public void Constructor(PlayerMovement playerMovement, 
            PlayerRotator playerRotator, InputReader inputReader,
            PlayerGroundChecker playerGroundChecker,
            PlayerJumper playerJumper, Character character, 
            AnimatorController animatorController, FallController fallController,
            PlayerData playerData, ISaveSystem saveSystem, StartPoint startPoint)
        {
            _playerMovement = playerMovement;
            _playerRotator = playerRotator;
            _inputReader = inputReader;
            _playerGroundChecker = playerGroundChecker;
            _playerJumper = playerJumper;
            _character = character;
            _animatorController = animatorController;
            _fallController = fallController;
            _saveSystem = saveSystem;
            _playerData = playerData;
            _startPoint = startPoint;
        }

        private void Start()
        {
            if (_saveSystem.HasKey("PlayerData"))
            {
                var loadedData = _saveSystem.Load<PlayerData>("PlayerData");
                _playerData.PlayerPosition = loadedData.PlayerPosition;
                _playerData.PlayerRotation = loadedData.PlayerRotation;

                _character.transform.SetLocalPositionAndRotation(_playerData.PlayerPosition, _playerData.PlayerRotation);
            }
            else
            {
                _character.transform.SetLocalPositionAndRotation(_startPoint.transform.position, new Quaternion(0, 0, 0, 0));
            }

                

            new PlayerStateMachine(_playerMovement, _playerRotator, _inputReader, _playerGroundChecker, _playerJumper, _animatorController, _fallController);
        }
    }
}
