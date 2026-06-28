using Assets._Scripts.GameControllers;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.ObjectsScripts.Camera;
using Assets._Scripts.ObjectsScripts.Player;
using Assets._Scripts.ObjectsScripts.Player.Factorys;
using Assets._Scripts.ObjectsScripts.StateMachines.Player;
using Assets._Scripts.SaveLoad.Service;
using Assets._Scripts.UI;
using Assets.Input;
using ECM2;
using System;
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
        private GameLoopController _gameLoopController;
        private GameSaveLoadService _gameSaveLoadService;

        public PlayerEnteryPoint(PlayerController playerController,
            PlayerStateMachineFactory playerStateMachineFactory,
            Func<Character> characterFactory, CameraController cameraController,
            BillboardManager billboardManager, Func<UnitInfoUIView> unitInfoUIFactory,
            WalletController walletController,
            GameLoopController gameLoopController,
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
        }

        public void Start()
        {
            _levelsController.SaveList.Add(_playerController);
            _levelsController.LoadList.Add(_playerController);
            _levelsController.RestartList.Add(_playerController);
            _levelsController.FinishList.Add(_playerController);

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
    }
}
