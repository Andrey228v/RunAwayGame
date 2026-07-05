using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.Utilites.Loger;
using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.ObjectsScripts.Coins
{
    public class CoinController:  ISave, ILoad, IRestart, IFinish
    {
        private readonly IGameLogger _gameLogger;
        private readonly Transform _objectParent;
        private readonly CoinDictinaryModel _dictinaryModel;
        private readonly Dictionary<string, CoinView> _dictinaryView;

        public CoinController(GamePoints points, IGameLogger gameLogger, CoinDictinaryModel dictinaryModel)
        {
            if (points == null)
                throw new ArgumentNullException(nameof(points), "GamePoints cannot be null");

            _objectParent = points.Coins;
            _dictinaryView = new Dictionary<string, CoinView>();
            _gameLogger = gameLogger;
            _dictinaryModel = dictinaryModel;

            _dictinaryModel.OnObjectAdd += ObjectInit;
        }

        public void Dispose()
        {
            _dictinaryModel.OnObjectAdd -= ObjectInit;

            foreach (var view in _dictinaryView.Values)
            {
                view.OnActivateObject -= ObjectActivateView;
                view.OnActivateObject -= TakeObject;

            }

            foreach( var model in _dictinaryModel.ObjectModelds.Values)
            {
                model.OnObjectStatusChange -= OnModelStatusChanged;
            }

            _dictinaryView.Clear();
        }

        public void Initialization(LevelData levelData)
        {
            if (levelData == null)
            {
                throw new ArgumentNullException(nameof(levelData), "gameSaveData cannot be null");
            }

            for (int i = 0; i < _objectParent.childCount; i++)
            {
                var view = _objectParent.GetChild(i).GetComponent<CoinView>();

                if (view == null) continue;

                string id = GenerateCoinId(i);

                view.SetId(id);
                view.OnActivateObject += ObjectActivateView;
                view.OnActivateObject += TakeObject;

                _dictinaryModel.AddObject(id);
                _dictinaryView[id] = view;
            }
        }

        private void ObjectInit(CoinModel model)
        {
            model.OnObjectStatusChange += OnModelStatusChanged;
        }

        private string GenerateCoinId(int index)
        {
            return $"coin_{index}";
        }

        public void ObjectActivateView(string id, bool status)
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

        public void TakeObject(string id, bool status)
        {
            //if (_dictinaryModel.ObjectModelds.TryGetValue(id, out var model))
            //{
            //    model.Take();
            //}
            //else
            //{
            //    _gameLogger.LogWarning($"Coin with ID {id} not found in models", "Service");
            //}
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
            ResetAllObjects();
        }

        public void Restart(LevelData levelData)
        {
            ResetAllObjects();
        }

        public void ResetAllObjects()
        {
            foreach (var model in _dictinaryModel.ObjectModelds.Values)
            {
                model.Reset();
            }
        }

        public void Save(LevelData levelData)
        {
            if (levelData == null) return;

            levelData.Coins = new List<CoinData>();

            foreach (var model in _dictinaryModel.ObjectModelds)
            {
                levelData.Coins.Add(new CoinData
                {
                    Id = model.Key,
                    IsActivated = model.Value.IsActivate
                });
            }

            _gameLogger.Log($"Saved {levelData.Coins.Count} coins", "Service");
        }

        public void Load(LevelData levelData)
        {
            if (levelData?.Coins == null || levelData.Coins.Count == 0)
            {
                _gameLogger.Log("No coin data to load, using defaults");
                return;
            }

            foreach (var coinData in levelData.Coins)
            {
                if (_dictinaryModel.ObjectModelds.TryGetValue(coinData.Id, out var model))
                {
                    model.SetActivateStatus(coinData.IsActivated);
                }
                else
                {
                    _gameLogger.LogWarning($"Coin with ID {coinData.Id} not found in scene");
                }
            }

            _gameLogger.Log($"Loaded {levelData.Coins.Count} coins");
        }
    }
}
