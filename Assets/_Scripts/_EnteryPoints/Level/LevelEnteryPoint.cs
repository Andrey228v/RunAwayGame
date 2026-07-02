using Assets._Scripts.GameControllers;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.ObjectsScripts.Coins;
using Assets._Scripts.ObjectsScripts.Points.Finish;
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
        private GameLoopService _gameLoopController;
        private FinishModel _finishModel;
        private CoinDictinaryModel _coinDictinaryModel;


        public LevelEnteryPoint(GamePoints gamePoints,
            CheckPointsController checkPointsController, 
            CoinController coinController,
            GameSaveLoadService gameSaveLoadService,
            WalletController walletController,
            LevelConfig levelConfig,
            FinishController finishController,
            GameLoopService gameLoopController,
            FinishModel finishModel, 
            CoinDictinaryModel coinDictinaryModel,
            LevelsController levelsController)
        {
            _checkPointsController = checkPointsController;
            _coinController = coinController;
            _levelsController = levelsController;
            _gameSaveLoadService = gameSaveLoadService;
            _levelConfig = levelConfig;
            _walletController = walletController;
            _finishController = finishController;
            _gameLoopController = gameLoopController;
            _coinDictinaryModel = coinDictinaryModel;
            _finishModel = finishModel;
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

            _levelsController.FinishList.Add(_finishController);

            var levelData = _gameSaveLoadService.GameSaveData.LevelsData[_levelConfig.LevelName];
            _coinController.Load(levelData);
            _checkPointsController.Load(levelData);

            //_coinController.OnTake += _walletController.AddConis; // переделать ??
            _coinDictinaryModel.OnCoinAdd += CoinAdd;
            _coinController.InitializeCoins();
            _finishModel.OnObjectStatusChange += _gameLoopController.FinishLevel;
        }

        public void Dispose()
        {
            _levelsController.Dispose();
            _checkPointsController.Dispose();
            _coinController.Dispose();
            _finishController.Dispose();

            //_coinController.OnTake -= _walletController.AddConis;
            _finishModel.OnObjectStatusChange -= _gameLoopController.FinishLevel;
            _coinDictinaryModel.OnCoinAdd -= CoinAdd;

            foreach (var model in _coinDictinaryModel.ObjectModelds.Values)
            {
                model.OnTake -= _walletController.AddConis;
            }
        }

        public void CoinAdd(CoinModel model)
        {
            model.OnTake += _walletController.AddConis;
        }
    }
}
