using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.ObjectsScripts.Coins;
using Assets._Scripts.SaveLoad.Service;
using Assets.Scripts.Player;
using Assets.Scripts.Points;
using System;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class LevelEnteryPoint : IStartable, IDisposable
    {
        private CheckPointsController _checkPointsController;
        private CoinController _coinController;
        private LevelsController _levelsController;
        private GameSaveLoadService _gameSaveLoadService;
        private WalletController _walletController;

        public LevelEnteryPoint(GamePoints gamePoints,
            CheckPointsController checkPointsController, 
            CoinController coinController,
            GameSaveLoadService gameSaveLoadService,
            WalletController walletController,
            LevelsController levelsController)
        {
            _checkPointsController = checkPointsController;
            _coinController = coinController;
            _levelsController = levelsController;
            _gameSaveLoadService = gameSaveLoadService;
            _walletController = walletController;
        }

        public void Start()
        {
            //тут переделать. Мы в Лвл контроллер в список добавляем, но потом снова вызывает _сoinConroller.Init...
            _levelsController.AddInitialization(_coinController);
            _levelsController.AddSave(_coinController);
            _levelsController.AddLoad(_coinController);
            _levelsController.AddRestart(_coinController);
            _levelsController.AddFinish(_coinController);

            _levelsController.AddInitialization(_checkPointsController);
            _levelsController.AddSave(_checkPointsController);
            _levelsController.AddLoad(_checkPointsController);
            _levelsController.AddRestart(_checkPointsController);
            _levelsController.AddFinish(_checkPointsController);

            _coinController.Initialzation(_gameSaveLoadService.GameSaveData, _gameSaveLoadService.levelConfig);
            _checkPointsController.Initialzation(_gameSaveLoadService.GameSaveData, _gameSaveLoadService.levelConfig);

            _coinController.Load(_gameSaveLoadService.GameSaveData, _gameSaveLoadService.levelConfig);
            _checkPointsController.Load(_gameSaveLoadService.GameSaveData, _gameSaveLoadService.levelConfig);

            _coinController.OnTake += _walletController.AddConis;

        }

        public void Dispose()
        {
            _checkPointsController.Dispose();
            _coinController.Dispose();

            _coinController.OnTake -= _walletController.AddConis;
        }
    }
}
