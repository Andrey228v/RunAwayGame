using Assets._Scripts.GameControllers;
using Assets._Scripts.SaveLoad.Data;
using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.ObjectsScripts.Coins
{
    public class CoinController : ISaveLoad, IDisposable, IRestart
    {
        private Transform _objectParent;
        private List<Coin> _objectList;

        public event Action OnTake;

        public void Dispose()
        {
            foreach (Coin obj in _objectList)
            {
                obj.Dispose();
                obj.OnActivated -= CoinActivated;
            }
        }

        public CoinController(GamePoints points)
        {
            if (points != null)
                _objectParent = points.Coins;
            else
                throw new ArgumentNullException(nameof(points), "CoinController parent cannot be null");


            _objectList = TransformToList(_objectParent);

        }

        public List<Coin> TransformToList(Transform objectsParent)
        {
            if (objectsParent == null)
                throw new ArgumentNullException(nameof(objectsParent), "checkPointsParent cannot be null");

            List<Coin> Coins = new List<Coin>();

            for (int i = 0; i < objectsParent.childCount; i++)
            {
                Coin coin = objectsParent.GetChild(i).GetComponent<Coin>();
                Coins.Add(coin);
                coin.OnActivated += CoinActivated;
            }

            return Coins;
        }

        public void CoinActivated(Coin coin)
        {
            OnTake?.Invoke();
        }

        public void Save(LevelData levelData)
        {
            for (int i = 0; i < _objectList.Count; i++)
            {
                levelData.Coins[i] = new CoinData {IsActivated = _objectList[i].IsActivated };
            }
        }

        public void Load(LevelData levelData, LevelConfig levelConfig)
        {
            var objectCount = _objectList.Count;

            if (levelData.Coins == null)
            {
                List<CoinData> objectData = new List<CoinData>();

                for (int i = 0; i < _objectList.Count; i++)
                {
                    objectData.Add(new CoinData { IsActivated = _objectList[i].IsActivated });
                }

                levelData.Coins = objectData;
            }
            else
            {
                for (int i = 0; i < objectCount; i++)
                {
                    Coin obj = _objectList[i];
                    CoinData objData = levelData.Coins[i];
                    obj.SetState(objData.IsActivated);
                }
            }
        }

        public void Restart()
        {
            foreach (var obj in _objectList)
            {
                obj.Deactivate();
            }
        }
    }
}
