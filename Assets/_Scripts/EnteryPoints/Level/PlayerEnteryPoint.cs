using Assets._Scripts.GameControllers;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.SaveLoad.Service;
using Assets._Scripts.UI;
using Assets.Input;
using Assets.Scripts.Camera;
using Assets.Scripts.Player;
using Assets.Scripts.StateMachines.Player;
using ECM2;
using System;
using Unity.VisualScripting;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class PlayerEnteryPoint : IDisposable, ILevelInit // IStartable
    {
        private PlayerController _playerController;
        private PlayerStateMachineFactory _playerStateMachineFactory;
        private CameraController _cameraController;
        private Func<Character> _characterFactory;
        private BillboardManager _billboardManager;
        private Func<UnitInfoUI> _unitInfoUIFactory;
        private GameSaveLoadService _gameSaveLoadService;
        //private LevelsController _levelsController;

        public PlayerEnteryPoint(PlayerController playerController, 
            PlayerStateMachineFactory playerStateMachineFactory, 
            Func<Character> characterFactory, CameraController cameraController,
            BillboardManager billboardManager, Func<UnitInfoUI> unitInfoUIFactory,
            GameSaveLoadService gameSaveLoadService
            //LevelsController levelsController
            ) 
        {
            _playerController = playerController;
            _playerStateMachineFactory = playerStateMachineFactory;
            _cameraController = cameraController;
            _characterFactory = characterFactory;
            _billboardManager = billboardManager;
            _unitInfoUIFactory = unitInfoUIFactory;
            _gameSaveLoadService = gameSaveLoadService;
            //_levelsController = levelsController;
        }

        public void Initialize()
        {
            InitPlayer(_cameraController, _characterFactory, //Переделать...
                        _playerStateMachineFactory, _playerController,
                        _unitInfoUIFactory, _billboardManager);
            InitEvents();
        }

        public void Start()
        {
            ////_levelsController.SetPlayerController(_playerController);

            //InitPlayer(_cameraController, _characterFactory, //Переделать...
            //            _playerStateMachineFactory, _playerController,
            //            _unitInfoUIFactory, _billboardManager);
            //InitEvents();


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

        private void InitPlayer(CameraController cameraController, 
            Func<Character> characterFactory, 
            PlayerStateMachineFactory playerStateMachineFactory, 
            PlayerController playerController,
            Func<UnitInfoUI> unitInfoUIFactory, 
            BillboardManager billboardManager)
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
            _gameSaveLoadService.DieRestart();
        }


    }
}
