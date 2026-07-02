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
        private IGameLogger _gameLogger;
        private Transform _objectParent;
        private readonly Dictionary<string, CoinModel> _objectModels;
        private readonly Dictionary<string, CoinView> _objectViewMap;

        public event Action<int> OnTake; // под вопросом...

        public CoinController(GamePoints points, IGameLogger gameLogger)
        {
            if (points == null)
                throw new ArgumentNullException(nameof(points), "GamePoints cannot be null");

            _objectParent = points.Coins;
            _objectModels = new Dictionary<string, CoinModel>();
            _objectViewMap = new Dictionary<string, CoinView>();

            InitializeCoins();
            _gameLogger = gameLogger;
        }

        public void Dispose()
        {
            foreach (var view in _objectViewMap.Values)
            {
                view.OnActivateObject -= CoinActivated;
            }

            foreach (var model in _objectModels.Values)
            {
                model.OnObjectStatusChange -= OnModelStatusChanged;
            }

            _objectModels.Clear();
            _objectViewMap.Clear();
        }

        private void InitializeCoins()
        {
            for (int i = 0; i < _objectParent.childCount; i++)
            {
                var coinView = _objectParent.GetChild(i).GetComponent<CoinView>();
                if (coinView == null) continue;

                // Генерируем уникальный ID
                string id = GenerateCoinId(i);

                // Создаём модель
                var model = new CoinModel(id);

                // Связываем View и Model
                coinView.SetId(id);
                coinView.OnActivateObject += CoinActivated;

                // Подписываем View на изменения Model
                model.OnObjectStatusChange += OnModelStatusChanged;

                // Сохраняем в словари
                _objectModels[id] = model;
                _objectViewMap[id] = coinView;

                // Применяем начальное состояние
                coinView.UpdateView(model.IsActivate);
            }
        }

        private string GenerateCoinId(int index)
        {
            //return Guid.NewGuid().ToString();
            return $"{_objectParent.name}_{index}";
        }

        public void CoinActivated(string id, bool status)
        {
            if (_objectModels.TryGetValue(id, out var model))
            {
                model.SetActivateStatus(status);

                OnTake?.Invoke(1);
            }
            else
            {
                _gameLogger.LogWarning($"Coin with ID {id} not found in models", "Service");
            }
        }

        private void OnModelStatusChanged(string id, bool isActivated)
        {
            if (_objectViewMap.TryGetValue(id, out var view))
            {
                view.UpdateView(isActivated);
            }
        }

        public void Finish(LevelData levelData)
        {
            ResetAllCoins();
        }

        public void Restart(LevelData levelData)
        {
            ResetAllCoins();
        }

        public void ResetAllCoins()
        {
            foreach (var model in _objectModels.Values)
            {
                model.Reset();
            }
        }

        public void Save(LevelData levelData)
        {
            if (levelData == null) return;

            levelData.Coins = new List<CoinData>();

            foreach (var model in _objectModels)
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
                Debug.Log("No coin data to load, using defaults");
                return;
            }

            foreach (var coinData in levelData.Coins)
            {
                if (_objectModels.TryGetValue(coinData.Id, out var model))
                {
                    model.SetActivateStatus(coinData.IsActivated);
                }
                else
                {
                    Debug.LogWarning($"Coin with ID {coinData.Id} not found in scene");
                }
            }

            Debug.Log($"Loaded {levelData.Coins.Count} coins");

            //var objectCount = _objectList.Count;

            //if (levelData.Coins == null || levelData.Coins.Count == 0)
            //{
            //    _objectData = new List<CoinData>();

            //    for (int i = 0; i < _objectList.Count; i++)
            //    {
            //        _objectData.Add(new CoinData { IsActivated = _objectList[i].IsActivated });
            //    }

            //    levelData.Coins = _objectData;
            //}
            //else
            //{
            //    for (int i = 0; i < objectCount; i++)
            //    {
            //        if (_objectList[i].IsActivated == false)
            //        {
            //            CoinView obj = _objectList[i];
            //            CoinData objData = levelData.Coins[i];
            //            obj.SetState(objData.IsActivated);
            //        }
            //    }
            //}
        }
    }
}
