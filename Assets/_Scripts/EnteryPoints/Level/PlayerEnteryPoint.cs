using Assets._Scripts.EnteryPoints.Interfaces;
using Assets._Scripts.GameControllers;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.SaveLoad.Service;
using Assets._Scripts.UI;
using Assets.Input;
using Assets.Scripts.Camera;
using Assets.Scripts.Player;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using Assets.Scripts.StateMachines.Player;
using ECM2;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class PlayerEnteryPoint : IStartable, IDisposable, IInitFinish, IInitRestart  // IInitSaveLoad
    {
        private PlayerController _playerController;
        private PlayerStateMachineFactory _playerStateMachineFactory;
        private CameraController _cameraController;
        private Func<Character> _characterFactory;
        //private SaveLoadService _saveLoadService;
        private LevelData _levelData;
        private GameFinishController _finishController;
        private GameRestartController _gameRestartController;
        private BillboardManager _billboardManager;
        private Func<UnitInfoUI> _unitInfoUIFactory;
        private GameSaveLoadService _gameSaveLoadService;
        private LevelsController _levelsController;

        private LevelConfig _levelConfig;

        //public IEnumerable<ISaveLoad> SaveLoads { get; private set; }

        public IEnumerable<IFinish> Finished { get; private set; }

        public IEnumerable<IRestart> Restarted { get; private set; }

        public PlayerEnteryPoint(PlayerController playerController, 
            PlayerStateMachineFactory playerStateMachineFactory, 
            Func<Character> characterFactory, CameraController cameraController,
            GameFinishController gameFinishController,
            GameRestartController gameRestartController,
            IEnumerable<IRestart> restarted, IEnumerable<IFinish> fineshed,
            BillboardManager billboardManager, Func<UnitInfoUI> unitInfoUIFactory,
            GameSaveLoadService gameSaveLoadService,
            LevelsController levelsController) 
        {
            _playerController = playerController;
            _playerStateMachineFactory = playerStateMachineFactory;
            _cameraController = cameraController;
            _characterFactory = characterFactory;
            //_saveLoadService = saveLoadService;
            _finishController = gameFinishController;
            _gameRestartController = gameRestartController;
            //SaveLoads = saveLoads;
            Finished = fineshed;
            Restarted = restarted;
            _billboardManager = billboardManager;
            _unitInfoUIFactory = unitInfoUIFactory;
            //_levelConfig = saveLoadService.LevelConfig;
            _gameSaveLoadService = gameSaveLoadService;
            _levelsController = levelsController;
        }

        public void Start()
        {
            //var saveLoadServise =  _gameSaveLoadService.GetService<LevelsController>();
            //saveLoadServise.AddSerice(_playerController);

            _levelsController.SetPlayerController(_playerController);

            InitSaveLoadData(_levelConfig);
            InitPlayer(_levelData, _cameraController, _characterFactory, //Переделать...
                        _playerStateMachineFactory, _playerController,
                        _unitInfoUIFactory, _billboardManager);
            InitFinishData();
            InitRestartData();
            InitEvents();

            //_saveLoadService.LoadPartLevelObject(SaveLoads, _levelConfig);
        }

        public void Dispose()
        {
            _billboardManager = null;
            _unitInfoUIFactory = null;
            _playerController.PlayerMB.OnDie -= DieRestartEntery;
        }

        private void InitEvents()
        {
            _playerController.PlayerMB.OnDie += DieRestartEntery;
        }

        public void InitSaveLoadData(LevelConfig levelConfig)
        {
            //_levelData = _saveLoadService.GetLevelData(levelConfig);
            //_saveLoadService.AddSaveLoadSub(_playerController); // зарегестрировали ISaveLoad надо подумать может передеать по другому...
        }

        public void InitFinishData()
        {
            _finishController.AddFinishSub(Finished);
        }

        public void InitRestartData()
        {
            _gameRestartController.AddRestartSub(Restarted);
        }

        private void InitPlayer(LevelData levelData, CameraController cameraController, 
            Func<Character> characterFactory, PlayerStateMachineFactory playerStateMachineFactory, PlayerController playerController,
            Func<UnitInfoUI> unitInfoUIFactory, BillboardManager billboardManager)
        {
            Character character = characterFactory();
            character.AddComponent<PlayerMB>(); // Тут подумать так ли делать ...

            cameraController.SetTarget(character.transform);
            InputReader inputReader = new InputReader();
            PlayerMoveDirectionCalculator playerMoveDirectionCalculator = new PlayerMoveDirectionCalculator(cameraController, inputReader);
            UnitStateMachine playerStateMachine = playerStateMachineFactory.Create(character, cameraController, inputReader, playerMoveDirectionCalculator);
            
            playerController.SetCharacter(character);
            playerController.SetPlayerStateMachine(playerStateMachine);

            UnitInfoUI unitInfoUI = unitInfoUIFactory();
            unitInfoUI.transform.SetParent(character.transform);

            billboardManager.AddUnitUI(unitInfoUI);
            billboardManager.SetDirectionCalculator(playerMoveDirectionCalculator);
            billboardManager.SetCameraController(cameraController);

            PlayerMB playerMB =  character.gameObject.GetComponent<PlayerMB>();
            playerController.SetPlayerMB(playerMB);
        }

        //Не правильно. Подумать потом как исправить. Надо переместить создание в контролле как в Бот контроллере.
        private void DieRestartEntery() 
        {
            //_levelData = _saveLoadService.GetLevelData(_levelConfig);
            //_playerController.DieRestart(_levelData);
            _gameSaveLoadService.DieRestart();
        }
    }
}
