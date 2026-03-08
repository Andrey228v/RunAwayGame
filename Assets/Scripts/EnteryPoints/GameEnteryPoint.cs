using Assets.Scripts.Camera;
using Assets.Scripts.Player;
using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.StateMachines.Player;
using ECM2;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.EnteryPoints
{
    public class GameEnteryPoint : IStartable
    {
        private PlayerController _playerController;
        private PlayerStateMachineFactory _playerStateMachineFactory;
        private CameraController _cameraController;
        private Func<Character> _characterFactory;
        private ISaveSystem _saveSystem;
        private PlayerData _playerData;
        //private StartPoint _startPoint;
        private IObjectResolver _container;
        private List<CheckPoint> _checkPoints;
        private int _checkpointsCount;
        private GamePoints _gamePoints;

        public GameEnteryPoint(PlayerController playerController,
            PlayerStateMachineFactory playerStateMachineFactory, CameraController cameraController,
            Func<Character> characterFactory, PlayerData playerData, ISaveSystem saveSystem,
            IObjectResolver container, GamePoints gamePoints)
        {
            _playerController = playerController;
            _playerStateMachineFactory = playerStateMachineFactory;
            _cameraController = cameraController;
            _characterFactory = characterFactory;
            _saveSystem = saveSystem;
            _playerData = playerData;
            //_startPoint = startPoint;
            _container = container;
            _gamePoints = gamePoints;
        }

        public void Start()
        {
            InitPlayer();
            InitCheckPoints();

        }

        private void InitCheckPoints()
        {
            Transform checkPointsParent = _gamePoints.CheckPoints;

            _checkPoints = new List<CheckPoint>();

            for (int i = 0; i < checkPointsParent.childCount; i++)
            {
                CheckPoint checkpoint = checkPointsParent.GetChild(i).GetComponent<CheckPoint>();
                _checkPoints.Add(checkpoint);
            }

            _checkpointsCount = _checkPoints.Count;

            Debug.Log(_checkpointsCount);


        }

        private void InitPlayer()
        {
            Character character = _characterFactory();

            if (_saveSystem.HasKey("PlayerData"))
            {
                var loadedData = _saveSystem.Load<PlayerData>("PlayerData");
                _playerData.PlayerPosition = loadedData.PlayerPosition;
                _playerData.PlayerRotation = loadedData.PlayerRotation;

                character.transform.SetLocalPositionAndRotation(_playerData.PlayerPosition, _playerData.PlayerRotation);
            }
            else
            {
                character.transform.SetLocalPositionAndRotation(_gamePoints.StartPoint.transform.position, new Quaternion(0, 0, 0, 0));
            }

            _cameraController.SetTarget(character.transform);
            PlayerStateMachine playerStateMachine = _playerStateMachineFactory.Create(character, _cameraController);
            _playerController.SetCharacter(character);
            _playerController.SetPlayerStateMachine(playerStateMachine);
        }

    }
}
