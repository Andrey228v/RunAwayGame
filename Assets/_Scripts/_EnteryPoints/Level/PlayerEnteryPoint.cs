using Assets._Scripts.GameControllers;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.ObjectsScripts.Camera;
using Assets._Scripts.ObjectsScripts.Player;
using Assets._Scripts.ObjectsScripts.Player.Factorys;
using Assets._Scripts.ObjectsScripts.StateMachines.Player;
using Assets._Scripts.SaveLoad.Data.Interfaces;
using Assets._Scripts.SaveLoad.Service;
using Assets._Scripts.UI;
using Assets.Input;
using Assets.Scripts.SaveLoad.Data;
using ECM2;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class PlayerEnteryPoint : IStartable, IDisposable
    {
        private PlayerController _playerController;
        private PlayerStateMachineFactory _playerStateMachineFactory;
        private CameraController _cameraController;
        private Func<Character> _characterFactory;
        private BillboardManager _billboardManager;
        private Func<UnitInfoUIView> _unitInfoUIFactory;
        private LevelsController _levelsController;
        private WalletController _walletController;
        private GameLoopService _gameLoopController;
        private GameSaveLoadService _gameSaveLoadService;

        private readonly List<IInit> _initList; // каждый подэлемент должен сам инициализировать то, что будет. 
        private readonly List<ISave> _saveList;
        private readonly List<ILoad> _loadList;
        private readonly List<IDieRestart> _restartList;
        private readonly List<IFinish> _finishList;
        private readonly List<IReset> _resetList;

        public PlayerEnteryPoint(PlayerController playerController,
            PlayerStateMachineFactory playerStateMachineFactory,
            Func<Character> characterFactory, CameraController cameraController,
            BillboardManager billboardManager, Func<UnitInfoUIView> unitInfoUIFactory,
            WalletController walletController,
            GameLoopService gameLoopController,
            GameSaveLoadService gameSaveLoadService,
            LevelsController levelsController
            ) 
        {
            _playerController = playerController;
            _playerStateMachineFactory = playerStateMachineFactory;
            _cameraController = cameraController;
            _characterFactory = characterFactory;
            _billboardManager = billboardManager;
            _unitInfoUIFactory = unitInfoUIFactory;
            _walletController = walletController;
            _levelsController = levelsController;
            _gameLoopController = gameLoopController;
            _gameSaveLoadService = gameSaveLoadService;

            _saveList = new List<ISave>();
            _loadList = new List<ILoad>();
            _restartList = new List<IDieRestart>();
            _finishList = new List<IFinish>();
            _resetList = new List<IReset>();
        }

        public void Start()
        {
            _saveList.Add(_playerController);
            _loadList.Add(_playerController);
            _restartList.Add(_playerController);
            _finishList.Add(_playerController);

            InitPlayer(_cameraController, _characterFactory, //Переделать...
                        _playerStateMachineFactory, _playerController,
                        _unitInfoUIFactory, _billboardManager, _walletController);

            var levelConfig = _levelsController.Config;
            var levelData = _gameSaveLoadService.GameSaveData.LevelsData[levelConfig.LevelName];
            _playerController.Load(levelData);

            _playerController.PlayerMB.OnDie += _gameLoopController.DieRestart;
        }

        public void Dispose()
        {
            _billboardManager = null;
            _unitInfoUIFactory = null;
            _playerController.PlayerMB.OnDie -= _gameLoopController.DieRestart;
        }

        private void InitPlayer(CameraController cameraController, 
            Func<Character> characterFactory, PlayerStateMachineFactory playerStateMachineFactory, PlayerController playerController,
            Func<UnitInfoUIView> unitInfoUIFactory, BillboardManager billboardManager, WalletController walletController)
        {
            Character character = characterFactory();
            character.AddComponent<PlayerMB>(); // Тут подумать так ли делать ...

            cameraController.SetTarget(character.transform);
            InputReader inputReader = new InputReader();
            PlayerMoveDirectionCalculator playerMoveDirectionCalculator = new PlayerMoveDirectionCalculator(cameraController, inputReader);
            UnitStateMachine playerStateMachine = playerStateMachineFactory.Create(character, cameraController, inputReader, playerMoveDirectionCalculator);
            
            playerController.SetCharacter(character);
            playerController.SetPlayerStateMachine(playerStateMachine);

            UnitInfoUIView unitInfoUI = unitInfoUIFactory();
            unitInfoUI.transform.SetParent(character.transform);

            billboardManager.AddUnitUI(unitInfoUI);
            billboardManager.SetDirectionCalculator(playerMoveDirectionCalculator);
            billboardManager.SetCameraController(cameraController);

            PlayerMB playerMB =  character.gameObject.GetComponent<PlayerMB>();
            playerController.SetPlayerMB(playerMB);

            walletController.AddUnitInfoUIView(unitInfoUI);
        }

        public void SaveAllServices(GameSaveData gameSaveData)
        {
            var levelData = _gameSaveLoadService.GameSaveData.LevelsData[_levelsController.Config.LevelName];

            if (gameSaveData == null)
            {
                throw new ArgumentNullException(nameof(gameSaveData), "gameSaveData cannot be null");
            }

            foreach (ISave save in _saveList)
            {
                save.Save(levelData);
            }
        }

        public void LoadAllServices(LevelData levelData)
        {
            if (levelData == null)
            {
                throw new ArgumentNullException(nameof(levelData), "levelData cannot be null");
            }

            if (_levelsController.Config == null)
            {
                return;
            }

            foreach (ILoad load in _loadList)
            {
                load.Load(levelData);
            }
        }

        public void DieRestart(GameSaveData gameSaveData)
        {
            var levelData = _gameSaveLoadService.GameSaveData.LevelsData[_levelsController.Config.LevelName];

            foreach (IDieRestart restart in _restartList)
            {
                restart.DieRestart(levelData);
            }
        }

        public void FinishLevel(LevelData levelData)
        {
            if (levelData == null)
            {
                throw new ArgumentNullException(nameof(levelData), "gameSaveData cannot be null");
            }

            foreach (IFinish finish in _finishList)
            {
                finish.Finish(levelData);
            }
        }

        public void ResetLevel(LevelData levelData, LevelConfig levelConfig)
        {
            if (levelData == null)
            {
                throw new ArgumentNullException(nameof(levelData), "gameSaveData cannot be null");
            }

            foreach (IReset reset in _resetList)
            {
                reset.ResetAllObjects(levelConfig);
            }
        }
    }
}
