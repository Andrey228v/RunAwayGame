using Assets._Scripts.ObjectsScripts.Points.CheckPoint;
using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.SaveLoad.Data.Interfaces;
using Assets._Scripts.Utilites.Loger;
using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.ObjectsScripts.Coins
{
    public class CoinController:  ISave, ILoad, IDieRestart, IFinish, IReset
    {
        private readonly IGameLogger _gameLogger;
        private readonly Transform _objectParent;
        private readonly CoinDictinaryModel _dictinaryModel;
        private readonly Dictionary<string, CoinView> _dictinaryView;

        public CoinController(GamePoints points, IGameLogger gameLogger, CoinDictinaryModel dictinaryModel)
        {
            if (points == null)
                throw new ArgumentNullException(nameof(points), "GamePoints cannot be null");

            _gameLogger = gameLogger;
            _objectParent = points.Coins;
            _dictinaryView = new Dictionary<string, CoinView>();
            _dictinaryModel = dictinaryModel;

            _dictinaryModel.OnObjectAdd += ObjectInit;
        }

        public void Dispose()
        {
            _dictinaryModel.OnObjectAdd -= ObjectInit;

            foreach(var view in _dictinaryView.Values)
            {
                view.OnActivateObject -= ObjectActivateView;
            }

            foreach(var model in _dictinaryModel.ObjectModelds.Values)
            {
                model.OnObjectStatusChange -= OnModelStatusChanged;
            }

            _dictinaryView.Clear();
        }

        public void Initialization(LevelData levelData, LevelConfig levelConfig)
        {
            if (levelData == null)
            {
                throw new ArgumentNullException(nameof(levelData), "gameSaveData cannot be null");
            }

            var listSaveData = levelData.Coins;

            for (int i = 0; i < _objectParent.childCount; i++)
            {
                if (_objectParent.GetChild(i).TryGetComponent<CoinView>(out var view))
                {
                    var id = view.Id;

                    CoinData data = null;

                    if (listSaveData.ContainsKey(id))
                    {
                        data = listSaveData[id];
                    }
                    else
                    {
                        data = new CoinData
                        {
                            Id = id,
                            IsActivated = false
                        };

                        if (listSaveData.TryAdd(id, data) == false)
                        {
                            throw new ArgumentNullException(nameof(levelData), "key Error");
                        }
                    }

                    view.OnActivateObject += ObjectActivateView;

                    _dictinaryModel.AddObject(data);
                    _dictinaryView[data.Id] = view;
                }
                else
                {
                    throw new ArgumentNullException(nameof(levelData), "view cannot be null/any");
                }
            }
        }

        private void ObjectInit(CoinModel model)
        {
            model.OnObjectStatusChange += OnModelStatusChanged;
        }

        public void ObjectActivateView(string id, bool status, Vector3 coords)
        {
            if (_dictinaryModel.ObjectModelds.TryGetValue(id, out var model))
            {
                model.SetActivateStatus(status);
                model.Take();
            }
            else
            {
                _gameLogger.LogWarning($"Coin with ID {id} not found in models", "Service");
            }
        }

        private void OnModelStatusChanged(string id, bool isActivated)
        {
            if (_dictinaryView.TryGetValue(id, out var view))
            {
                view.UpdateView(isActivated);
            }
        }

        public void Finish(LevelData levelData)
        {
            foreach (var model in _dictinaryModel.ObjectModelds.Values)
            {
                model.Reset();
            }
        }

        public void DieRestart(LevelData levelData)
        {
            //Тут пусто... а нужен ли этот метод пока не понятно...
        }

        public void ResetAllObjects(LevelConfig levelConfig)
        {
            foreach (var model in _dictinaryModel.ObjectModelds.Values)
            {
                model.Reset();
            }
        }

        public void Save(LevelData levelData)
        {
            if (levelData == null) return;

            foreach (var key in levelData.Coins.Keys)
            {
                if (_dictinaryModel.TryGetModel(key, out var model))
                {
                    levelData.Coins[key] = model.Data;
                }
                else
                {
                    throw new ArgumentNullException("ERROR KEY");
                }

            }

            _gameLogger.Log($"Saved {levelData.Coins.Count} coins", "Service");
        }

        public void Load(LevelData levelData)
        {
            if (levelData?.Coins == null || levelData.Coins.Count == 0)
            {
                _gameLogger.Log("No check point data to load, using defaults");
                return;
            }

            //под вопосом...
            foreach (var objectData in levelData.Coins)
            {
                if (_dictinaryModel.ObjectModelds.TryGetValue(objectData.Key, out var model))
                {
                    model.SetActivateStatus(objectData.Value.IsActivated);
                }
                else
                {
                    _gameLogger.LogWarning($"Check point with ID {objectData.Key} not found in scene");
                }
            }

            _gameLogger.Log($"Loaded {levelData.Coins.Count} coins");
        }
    }
}
