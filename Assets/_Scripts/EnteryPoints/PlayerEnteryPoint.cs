using Assets.Scripts.Camera;
using Assets.Scripts.Player;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using Assets.Scripts.StateMachines.Player;
using ECM2;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class PlayerEnteryPoint : IStartable, IDisposable
    {
        private PlayerController _playerController;
        private PlayerStateMachineFactory _playerStateMachineFactory;
        private CameraController _cameraController;
        private Func<Character> _characterFactory;
        private SaveLoadService _saveLoadService;
        private IEnumerable<ISaveLoad> _saveLoads;
        private LevelData _loadData;
        private LevelConfig _levelConfig;

        public PlayerEnteryPoint(PlayerController playerController, 
            PlayerStateMachineFactory playerStateMachineFactory, 
            Func<Character> characterFactory, CameraController cameraController, 
            SaveLoadService saveLoadService,
            IEnumerable<ISaveLoad> saveLoads) 
        {
            _playerController = playerController;
            _playerStateMachineFactory = playerStateMachineFactory;
            _cameraController = cameraController;
            _characterFactory = characterFactory;
            _saveLoadService = saveLoadService;
            _saveLoads = saveLoads;
        }

        public void Start()
        {
            _loadData = _saveLoadService.GetLevelData();
            _levelConfig = _saveLoadService.GetLevelConfig();

            _saveLoadService.SetLevelObjects(_saveLoads);
            _loadData = InitPlayer(_loadData);
            _saveLoadService.SaveLevelIntoGame(_loadData); // Под вопросом...
            _saveLoadService.LoadLevel();
        }

        public void Dispose()
        {
            _saveLoadService.ClearSaveLoadList();
        }

        private LevelData InitPlayer(LevelData levelData)
        {
            Character character = _characterFactory();

            if (levelData.PlayerData == null)
            {
                var playerData = new PlayerData
                {
                    PlayerPosition = _levelConfig.StartPosition,
                    PlayerRotation = Quaternion.Euler(_levelConfig.StartRotationEuler),
                };

                levelData.PlayerData = playerData;
            }

            _cameraController.SetTarget(character.transform);
            PlayerStateMachine playerStateMachine = _playerStateMachineFactory.Create(character, _cameraController);
            _playerController.SetCharacter(character);
            _playerController.SetPlayerStateMachine(playerStateMachine);

            return levelData;
        }
    }
}
