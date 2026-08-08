using Assets._Scripts.GameControllers;
using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.GameMVP;
using Assets._Scripts.GameMVP.Achievments;
using Assets._Scripts.ObjectsScripts.Coins;
using Assets._Scripts.ObjectsScripts.Points.CheckPoint;
using Assets._Scripts.ObjectsScripts.Points.Finish;
using Assets._Scripts.SaveLoad.Service;
using Assets.Scripts.Points;
using System;
using System.Collections.Generic;
using UnityEngine;
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
        private GamePoints _gamePoints;
        private AchievmentDictinaryModel _achievmentDictinaryModel;

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
            LevelLoopService levelLoopService,
            LevelsController levelsController,
            AchievmentDictinaryModel achievmentDictinaryModel)
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
            _levelLoopService = levelLoopService;
            _gamePoints = gamePoints;
            _achievmentDictinaryModel = achievmentDictinaryModel;

            _levelLoopService.SaveDict.Add("CheckPointsController", _checkPointsController);
            _levelLoopService.SaveDict.Add("CoinController", _coinController);
            //_levelLoopService.SaveDict.Add("FinishController", _finishController);
            _levelLoopService.SaveDict.Add("LastCheckPointController", _lastCheckPointController);

            _levelLoopService.LoadDict.Add("CheckPointsController", _checkPointsController);
            _levelLoopService.LoadDict.Add("CoinController", _coinController);
            //_levelLoopService.LoadDict.Add("FinishController", _finishController);
            _levelLoopService.LoadDict.Add("LastCheckPointController", _lastCheckPointController);

            _levelLoopService.DieRestartDict.Add("CheckPointsController", _checkPointsController);
            _levelLoopService.DieRestartDict.Add("CoinController", _coinController);
            //_levelLoopService.DieRestartDict.Add("FinishController", _finishController);
            _levelLoopService.DieRestartDict.Add("LastCheckPointController", _lastCheckPointController);

            _levelLoopService.FinishDict.Add("LastCheckPointController", _lastCheckPointController);
            _levelLoopService.FinishDict.Add("CheckPointsController", _checkPointsController);
            _levelLoopService.FinishDict.Add("CoinController", _coinController);
            _levelLoopService.FinishDict.Add("FinishController", _finishController);

            _levelLoopService.ResetDict.Add("CheckPointsController", _checkPointsController);
            _levelLoopService.ResetDict.Add("CoinController", _coinController);
            //_levelLoopService.ResetDict.Add("FinishController", _finishController);
            _levelLoopService.ResetDict.Add("LastCheckPointController", _lastCheckPointController);
        }

        public void Start()
        {
            Transform objectParent = _gamePoints.Coins;

            _levelsController.SetLevelConfig(_levelConfig); // шаг №1 задание конфига.
            _levelsController.InitializationLevelData(_gameSaveLoadService.GameSaveData); // создаём lvldata если его нет для данного уровня.

            var levelData = _gameSaveLoadService.GameSaveData.LevelsData[_levelConfig.LevelName];

            _coinDictinaryModel.OnObjectAdd += CoinAddInDictinary;
            _checkPointDictinaryModel.OnObjectAdd += CheckPointAddInDictinary;

            _coinController.Initialization(levelData, _levelConfig);
            _checkPointsController.Initialization(levelData, _levelConfig);
            _lastCheckPointController.Initialization(levelData, _levelConfig);

            _finishModel.OnFinish += FinishLevel;

            _coinController.Load(levelData);
            _checkPointsController.Load(levelData);
            _lastCheckPointController.Load(levelData);

            //Под большим вопросом....
            if(_levelConfig.LevelName == "Lvl0")
            {
                if (_achievmentDictinaryModel.TryGetModel("ACh_0", out AchievmentModel model0))
                {
                    if (model0.Data.IsUnlock == false)
                    {
                        model0.ChangeCurrentProgress(1);
                    }
                }
            }
            else if( _levelConfig.LevelName == "Lvl1")
            {
                if (_achievmentDictinaryModel.TryGetModel("ACh_1", out AchievmentModel model1))
                {
                    if (model1.Data.IsUnlock == false)
                    {
                        model1.ChangeCurrentProgress(1);
                    }
                }
            }
            else if (_levelConfig.LevelName == "Lvl2")
            {
                if (_achievmentDictinaryModel.TryGetModel("ACh_2", out AchievmentModel model2))
                {
                    if (model2.Data.IsUnlock == false)
                    {
                        model2.ChangeCurrentProgress(1);
                    }
                }
            }

            //Под вопросом....
            if (_achievmentDictinaryModel.TryGetModel("ACh_3", out AchievmentModel model3))
            {
                if(model3.Data.IsUnlock == false)
                {
                    _finishController.OnFinishLvl0 += model3.ChangeCurrentProgress;
                }
            }

            if (_achievmentDictinaryModel.TryGetModel("ACh_4", out AchievmentModel model4))
            {
                if(model4.Data.IsUnlock == false)
                {
                    _finishController.OnFinishLvl1 += model4.ChangeCurrentProgress;
                }
            }

            if (_achievmentDictinaryModel.TryGetModel("ACh_5", out AchievmentModel model5))
            {
                if (model5.Data.IsUnlock == false)
                {
                    _finishController.OnFinishLvl2 += model5.ChangeCurrentProgress;
                }
            }

            if(_achievmentDictinaryModel.TryGetModel("ACh_6", out AchievmentModel model6))
            {
                if (model6.Data.IsUnlock == false)
                {
                    _walletController.OnAddCoin += model6.ChangeCurrentProgress;
                }
            }
        }

        public void Dispose()
        {
            _checkPointsController.Dispose();
            _coinController.Dispose();
            _finishController.Dispose();
            _lastCheckPointController.Dispose();

            _finishModel.OnFinish -= FinishLevel;
            _coinDictinaryModel.OnObjectAdd -= CoinAddInDictinary;
            _checkPointDictinaryModel.OnObjectAdd -= CheckPointAddInDictinary;

            foreach (var model in _coinDictinaryModel.ObjectModelds.Values)
            {
                model.OnTakeValue -= _walletController.AddConis;
                model.OnTake -= SaveLevel;
            }

            _levelLoopService.Dispose();
        }

        private void CoinAddInDictinary(CoinModel model)
        {
            model.OnTakeValue += _walletController.AddConis;
            model.OnTake += SaveLevel;
        }

        private void CheckPointAddInDictinary(CheckPointModel model)
        {
            model.OnTakePosition += _lastCheckPointController.SetData;
            model.OnTake += SaveLevel;
        }

        private void FinishLevel()
        {
            _gameLoopController.FinishLevel(_gameSaveLoadService.GameSaveData);
        }


        //Сохранаяем при взятии чекпоинта, монетки, завершении уровня.
        private void SaveLevel()
        {
            _gameLoopController.SaveAllServices(_gameSaveLoadService.GameSaveData);
        }
    }
}
