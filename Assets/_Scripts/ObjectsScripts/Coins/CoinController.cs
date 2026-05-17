using Assets._Scripts.GameControllers;
using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.SaveLoad.Service;
using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.ObjectsScripts.Coins
{
    public class CoinController: IInitialzation, ISave, ILoad, IRestart, IFinish
    {
        private Transform _objectParent;
        private List<Coin> _objectList;
        private List<CoinData> _objectData;

        public event Action OnTake;

        public CoinController(GamePoints points)
        {
            if (points != null)
                _objectParent = points.Coins;
            else
                throw new ArgumentNullException(nameof(points), "CoinController parent cannot be null");


            _objectList = TransformToList(_objectParent);

        }

        public void Initialzation(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            _objectData = new List<CoinData>();

            for (int i = 0; i < _objectList.Count; i++)
            {
                _objectData.Add(new CoinData { IsActivated = _objectList[i].IsActivated });
            }
        }

        public void Dispose()
        {
            foreach (Coin obj in _objectList)
            {
                obj.Dispose();
                obj.OnActivated -= CoinActivated;
            }
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

        public void Finish(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            LevelData levelData = gameSaveData.LevelsData[levelConfig.LevelName]; // заглушка.
            Restart(levelData);
        }

        public void CoinActivated(Coin coin)
        {
            OnTake?.Invoke();
        }

        public void Restart(LevelData levelData)
        {
            foreach (var obj in _objectList)
            {
                obj.Deactivate();
            }
        }

        public void Save(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            var levelData = gameSaveData.LevelsData[levelConfig.LevelName];

            for (int i = 0; i < _objectList.Count; i++)
            {
                levelData.Coins[i] = new CoinData { IsActivated = _objectList[i].IsActivated };
            }
        }

        public void Load(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            var levelData = gameSaveData.LevelsData[levelConfig.LevelName];
            var objectCount = _objectList.Count;

            if (levelData.Coins == null || levelData.Coins.Count == 0)
            {
                //List<CoinData> objectData = new List<CoinData>();

                //for (int i = 0; i < _objectList.Count; i++)
                //{
                //    objectData.Add(new CoinData { IsActivated = _objectList[i].IsActivated });
                //}

                //levelData.Coins = objectData;
                levelData.Coins = _objectData;
            }
            else
            {
                for (int i = 0; i < objectCount; i++)
                {
                    if (_objectList[i].IsActivated == false)
                    {
                        Coin obj = _objectList[i];
                        CoinData objData = levelData.Coins[i];
                        obj.SetState(objData.IsActivated);
                    }
                }
            }
        }
    }
}
