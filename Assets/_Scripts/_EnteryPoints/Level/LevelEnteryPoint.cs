using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.ObjectsScripts.Coins;
using Assets._Scripts.ObjectsScripts.Points;
using Assets._Scripts.SaveLoad.Service;
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
        private LevelConfig _levelConfig;
        private FinishController _finishController;

        public LevelEnteryPoint(GamePoints gamePoints,
            CheckPointsController checkPointsController, 
            CoinController coinController,
            GameSaveLoadService gameSaveLoadService,
            WalletController walletController,
            LevelConfig levelConfig,
            FinishController finishController,
            LevelsController levelsController)
        {
            _checkPointsController = checkPointsController;
            _coinController = coinController;
            _levelsController = levelsController;
            _gameSaveLoadService = gameSaveLoadService;
            _levelConfig = levelConfig;
            _walletController = walletController;
            _finishController = finishController;
        }

        public void Start()
        {
            //тут переделать. Мы в Лвл контроллер в список добавляем, но потом снова вызывает _сoinConroller.Init...
            _levelsController.SetLevelConfig(_levelConfig);
            _levelsController.Initialization(_gameSaveLoadService.GameSaveData);
            _levelsController.SetLevelData(_gameSaveLoadService.GameSaveData, _levelConfig);

            _levelsController.SaveList.Add(_coinController);
            _levelsController.LoadList.Add(_coinController);
            _levelsController.RestartList.Add(_coinController);
            _levelsController.FinishList.Add(_coinController);

            _levelsController.SaveList.Add(_checkPointsController);
            _levelsController.LoadList.Add(_checkPointsController);
            _levelsController.RestartList.Add(_checkPointsController);
            _levelsController.FinishList.Add(_checkPointsController);

            _levelsController.SaveList.Add(_finishController);
            _levelsController.LoadList.Add(_finishController);
            _levelsController.RestartList.Add(_finishController);
            _levelsController.FinishList.Add(_finishController);

            var levelData = _gameSaveLoadService.GameSaveData.LevelsData[_levelConfig.LevelName];
            _coinController.Load(levelData);
            _checkPointsController.Load(levelData);
            _finishController.Load(levelData);

            _coinController.OnTake += _walletController.AddConis;

        }

        public void Dispose()
        {
            _levelsController.Dispose();
            _checkPointsController.Dispose();
            _coinController.Dispose();
            _finishController.Dispose();

            _coinController.OnTake -= _walletController.AddConis;
        }
    }
}
