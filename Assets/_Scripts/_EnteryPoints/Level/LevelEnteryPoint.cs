using Assets._Scripts.GameControllers;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.GameMVP;
using Assets._Scripts.ObjectsScripts.Coins;
using Assets._Scripts.ObjectsScripts.Points.CheckPoint;
using Assets._Scripts.ObjectsScripts.Points.Finish;
using Assets._Scripts.SaveLoad.Service;
using Assets.Scripts.Points;
using System;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    //Основная цель сделать так, чтобы уровень не зависил от частей его наполнения и они могли быть разной конфигурации.
    // для этого всё делает интерфейсами, выделяя в них только общее и вынося уже для каждого в своё.
    // Разные типы уровней будут обладать разными LevelEntryPoin. Благодаря этому их можно наполнять по разному.
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
        private CheckPointDictinaryModel _checkPointDictinaryModel;
        private LastCheckPointController _lastCheckPointController;
        private LevelLoopService _levelLoopService;

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
            CheckPointDictinaryModel checkPointDictinaryModel,
            LastCheckPointController lastCheckPointController,
            LevelLoopService _levelLoopService,
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
            _checkPointDictinaryModel = checkPointDictinaryModel;
            _finishModel = finishModel;
            _lastCheckPointController = lastCheckPointController;

            _levelLoopService.
        }

        public void Start()
        {
            _levelsController.SetLevelConfig(_levelConfig); // шаг №1 задание конфига.
            _levelsController.InitializationLevelData(_gameSaveLoadService.GameSaveData); // создаём lvldata если его нет для данного уровня.

            var levelData = _gameSaveLoadService.GameSaveData.LevelsData[_levelConfig.LevelName];

            _coinDictinaryModel.OnObjectAdd += CoinAdd;
            _checkPointDictinaryModel.OnObjectAdd += CheckPointAdd;

            _coinController.Initialization(levelData, _levelConfig);
            _checkPointsController.Initialization(levelData, _levelConfig);
            _lastCheckPointController.Initialization(levelData, _levelConfig);

            _finishModel.OnObjectStatusChange += FinishLevel;

            _coinController.Load(levelData);
            _checkPointsController.Load(levelData);
            _lastCheckPointController.Load(levelData);
        }

        public void Dispose()
        {
            _checkPointsController.Dispose();
            _coinController.Dispose();
            _finishController.Dispose();
            _lastCheckPointController.Dispose();

            _finishModel.OnObjectStatusChange -= FinishLevel;
            _coinDictinaryModel.OnObjectAdd -= CoinAdd;

            foreach (var model in _coinDictinaryModel.ObjectModelds.Values)
            {
                model.OnTakeValue -= _walletController.AddConis;
                model.OnTake -= SaveLevel;
            }

            //_levelLoopService
        }

        private void CoinAdd(CoinModel model)
        {
            model.OnTake += SaveLevel;
            model.OnTakeValue += _walletController.AddConis;
        }

        private void CheckPointAdd(CheckPointModel model)
        {
            model.OnTake += SaveLevel;
            model.OnTakePosition += _lastCheckPointController.SetData;
        }

        private void FinishLevel(bool isFinish)
        {
            var levelData = _gameSaveLoadService.GameSaveData.LevelsData[_levelConfig.LevelName];
            //_gameLoopController.FinishLevel(levelData); // здесь надо сделать levelLoopController;
        }


        //Сохранаяем при взятии чекпоинта, монетки, завершении уровня.
        private void SaveLevel()
        {
            var levelData = _gameSaveLoadService.GameSaveData.LevelsData[_levelConfig.LevelName];



            //_gameLoopController.SaveAllServices(levelData); // здесь надо сделать levelLoopController;
        }

    }
}
