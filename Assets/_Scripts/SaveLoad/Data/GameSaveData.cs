using Assets._Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.SaveLoad.Data
{
    [Serializable]
    public class GameSaveData
    {
        public Dictionary<string, LevelData> LevelsData = new Dictionary<string, LevelData>(); // LevelController
        public AchievmentsData AchievmentsData; // AchievmentsController
        public ShopData ShopData; // ShopController
        public WalletData WalletData;
        public DateTime LastSaveTime;

        public GameSaveData(Dictionary<string, LevelData> levelsData, 
            AchievmentsData achievmentsData, 
            ShopData shopData, 
            WalletData walletData, 
            DateTime lastSaveTime)
        {
            LevelsData = levelsData;
            AchievmentsData = achievmentsData;
            ShopData = shopData;
            WalletData = walletData;
            LastSaveTime = lastSaveTime;
        }

        public void ResetData(LevelConfig levelConfig)
        {
            foreach (var key in LevelsData.Keys) 
            {
                LevelsData[key].ResetData(levelConfig);
            }

            AchievmentsData.ResetData(levelConfig);
            ShopData.ResetData(levelConfig);
            WalletData.ResetData(levelConfig);

        }
    }
}
