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
        private readonly CoinDictinaryModel _coinDictinaryModel;
        private readonly Dictionary<string, CoinView> _objectViewMap;

        //public event Action<int> OnTake; // под вопросом...

        public CoinController(GamePoints points, IGameLogger gameLogger, CoinDictinaryModel coinDictinaryModel)
        {
            if (points == null)
                throw new ArgumentNullException(nameof(points), "GamePoints cannot be null");

            _objectParent = points.Coins;
            _objectViewMap = new Dictionary<string, CoinView>();
            _gameLogger = gameLogger;
            _coinDictinaryModel = coinDictinaryModel;

            _coinDictinaryModel.OnCoinAdd += CoinInit;

            //InitializeCoins();
        }

        public void Dispose()
        {
            _coinDictinaryModel.OnCoinAdd -= CoinInit;

            foreach (var view in _objectViewMap.Values)
            {
                view.OnActivateObject -= CoinActivateView;
            }

            foreach( var model in _coinDictinaryModel.ObjectModelds.Values)
            {
                model.OnObjectStatusChange -= OnModelStatusChanged;
            }

            _coinDictinaryModel.Dispose();
            _objectViewMap.Clear();
        }

        public void InitializeCoins()
        {
            for (int i = 0; i < _objectParent.childCount; i++)
            {
                var coinView = _objectParent.GetChild(i).GetComponent<CoinView>();

                if (coinView == null) continue;

                string id = GenerateCoinId(i);

                coinView.SetId(id);
                coinView.OnActivateObject += CoinActivateView;

                _coinDictinaryModel.AddCoin(id);
                _objectViewMap[id] = coinView;
            }
        }

        private void CoinInit(CoinModel model)
        {
            model.OnObjectStatusChange += OnModelStatusChanged;

        }

        private string GenerateCoinId(int index)
        {
            //return Guid.NewGuid().ToString();
            return $"{_objectParent.name}_{index}";
        }

        public void CoinActivateView(string id, bool status)
        {
            if (_coinDictinaryModel.ObjectModelds.TryGetValue(id, out var model))
            {
                model.SetActivateStatus(status);

                //OnTake?.Invoke(1);
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
            foreach (var model in _coinDictinaryModel.ObjectModelds.Values)
            {
                model.Reset();
            }
        }

        public void Save(LevelData levelData)
        {
            if (levelData == null) return;

            levelData.Coins = new List<CoinData>();

            foreach (var model in _coinDictinaryModel.ObjectModelds)
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
                if (_coinDictinaryModel.ObjectModelds.TryGetValue(coinData.Id, out var model))
                {
                    model.SetActivateStatus(coinData.IsActivated);
                }
                else
                {
                    Debug.LogWarning($"Coin with ID {coinData.Id} not found in scene");
                }
            }

            Debug.Log($"Loaded {levelData.Coins.Count} coins");
        }
    }
}
