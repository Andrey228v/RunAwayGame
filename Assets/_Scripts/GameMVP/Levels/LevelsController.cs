using Assets._Scripts.GameMVP.Levels;
using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.SaveLoad.Data.Interfaces.Game;
using Assets._Scripts.Utilites.Loger;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.GameControllers.Levels
{
    public class LevelsController : ISaveGame, ILoadGame, IDieRestartGame, IFinishGame, IResetGame
    {
        private IGameLogger _gameLogger;
        private Transform _objectParent;
        private readonly LevelsDictinaryModel _dictinaryModel;
        private readonly Dictionary<string, LevelUIView> _dictinaryView;

        private LevelConfig _levelConfig;

        public LevelConfig Config => _levelConfig;

        public LevelsController(IGameLogger gameLogger, LevelsDictinaryModel dictinaryModel)
        {
            _gameLogger = gameLogger;
            _dictinaryModel = dictinaryModel;
            _dictinaryView = new Dictionary<string, LevelUIView>();
        }

        //Шаг №1. Добавляются вьюшки и добавляется информация в модель.
        public void AddMenuView(Transform parent)
        {
            _objectParent = parent;

            for (int i = 0; i < _objectParent.childCount; i++)
            {
                if (_objectParent.GetChild(i).TryGetComponent<LevelUIView>(out var view))
                {
                    var id = view.Id;

                    _dictinaryView.Add(id, view);

                    //Так ли, пускай пока так будет...
                    var data = new LevelData(
                        false,
                        new LastCheckPointData(),
                        new PlayerData(),
                        new Dictionary<string, CheckPointData>(),
                        new Dictionary<string, CoinData>()
                        );

                    _dictinaryModel.TryAddObject(id, data);
                    _dictinaryModel.TryGetModel(id, out var model);
                }
            }
        }

        //Шаг №2 инициализация словоря загрузки.
        //Инициализируется именно при запуске уровня. Правильно ли это или нет. Пока не знаю.
        //Запускать только в LevelEnteryPoint...
        public void Initialization(GameSaveData gameSaveData)
        {
            var levelsData = gameSaveData.LevelsData;
            var key = _levelConfig.LevelName;

            if (levelsData.ContainsKey(key) == false)
            {
                var newData = new LevelData(
                    false,
                    new LastCheckPointData(),
                    new PlayerData(),
                    new Dictionary<string, CheckPointData>(),
                    new Dictionary<string, CoinData>()
                    );

                gameSaveData.LevelsData.Add(key, newData);
            }
        }

        //Шаг №3
        //Загрукза в модель данных.
        public void Load(GameSaveData gameSaveData)
        {
            var levelData = gameSaveData.LevelsData;

            foreach (var key in _dictinaryView.Keys)
            {
                if (_dictinaryModel.TryGetModel(key, out var model))
                {
                    if (levelData.TryGetValue(key, out LevelData data))
                    {
                        model.SetData(levelData[key]);
                    }
                }
            }
        }


        public void DisposeMenuView()
        {
            foreach (var key in _dictinaryView.Keys)
            {
                var view = _dictinaryView[key];
                _dictinaryModel.TryGetModel(key, out var model);
                //view.OnTakeRewardButtonClick -= model.TakeReward;
            }

            _dictinaryView.Clear();
        }

        public void SetLevelConfig(LevelConfig levelConfig)
        {
            _levelConfig = levelConfig;
        }

        public void Save(GameSaveData gameSaveData)
        {
            foreach (var key in gameSaveData.LevelsData.Keys)
            {
                if (_dictinaryModel.TryGetModel(key, out var model))
                {
                    gameSaveData.LevelsData[key] = model.Data;
                }
            }
        }



        public void DieRestart(GameSaveData gameSaveData)
        {

        }

        public void Finish(GameSaveData gameSaveData) 
        {

        }

        public void Reset(GameSaveData gameSaveData)
        {

        }


    }
}
