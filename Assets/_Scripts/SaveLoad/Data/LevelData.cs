using Assets._Scripts.SaveLoad.Data;
using Assets.Scripts.Points;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SaveLoad.Data
{
    [Serializable]
    public class LevelData
    {
        public bool IsLevelWasStarted; // Был ли уровень запущен до этого запуска ??
        public Vector3 LastCheckPointPosition;
        public PlayerData PlayerData;
        public List<CheckPointData> CheckPoints;
        public List<CoinData> Coins;

        public LevelData(bool isLevelWasStarted, 
            Vector3 lastCheckPointPosition, 
            PlayerData playerData, 
            List<CheckPointData> checkPoints, 
            List<CoinData> coins)
        {
            IsLevelWasStarted = isLevelWasStarted;
            LastCheckPointPosition = lastCheckPointPosition;
            PlayerData = playerData;
            CheckPoints = checkPoints;
            Coins = coins;
        }

        public void ResetData(LevelConfig levelConfig)
        {
            LastCheckPointPosition = levelConfig.StartPosition;
            PlayerData.ResetData(levelConfig);

            foreach (var checkPoint in CheckPoints) 
            {
                checkPoint.ResetData(levelConfig);
            }

            foreach(var coin in Coins)
            {
                coin.ResetData(levelConfig);
            }
        }
    }
}
