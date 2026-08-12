using Assets._Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.SaveLoad.Data
{
    [Serializable]
    public class GameSaveData
    {
        public SettingsData SettingsData;
        public Dictionary<string, LevelData> LevelsData = new Dictionary<string, LevelData>(); // LevelController
        public Dictionary<string, AchievmentData> AchievmentsData; // AchievmentsController
        public ShopData ShopData; // ShopController
        public WalletData WalletData;
        public DateTime LastSaveTime;

        public GameSaveData(Dictionary<string, LevelData> levelsData,
            Dictionary<string, AchievmentData> achievmentsData, 
            ShopData shopData, 
            WalletData walletData,
            SettingsData settingsData,
            DateTime lastSaveTime)
        {
            LevelsData = levelsData;
            AchievmentsData = achievmentsData;
            ShopData = shopData;
            WalletData = walletData;
            SettingsData = settingsData;
            LastSaveTime = lastSaveTime;

            AchievmentData achData0 = new AchievmentData()
            {
                Id = "ACh_0",
                Name = "Старт №1",
                Description = "Начать уровень №1",
                IsUnlock = false,
                TargetValue = 1,
                IsRevardEnable = false
            };

            AchievmentData achData1 = new AchievmentData()
            {
                Id = "ACh_1",
                Name = "Старт №2",
                Description = "Начать уровень №2",
                IsUnlock = false,
                TargetValue = 1,
                IsRevardEnable = false
            };

            AchievmentData achData2 = new AchievmentData()
            {
                Id = "ACh_2",
                Name = "Старт №3",
                Description = "Начать уровень №3",
                IsUnlock = false,
                TargetValue = 1,
                IsRevardEnable = false
            };

            AchievmentData achData3 = new AchievmentData()
            {
                Id = "ACh_3",
                Name = "Финиш №1",
                Description = "Финишировать уровень №1",
                IsUnlock = false,
                TargetValue = 1,
                IsRevardEnable = false
            };

            AchievmentData achData4 = new AchievmentData()
            {
                Id = "ACh_4",
                Name = "Финиш №2",
                Description = "Финишировать уровень №2",
                IsUnlock = false,
                TargetValue = 1,
                IsRevardEnable = false
            };

            AchievmentData achData5 = new AchievmentData()
            {
                Id = "ACh_5",
                Name = "Финиш №3",
                Description = "Финишировать уровень №3",
                IsUnlock = false,
                TargetValue = 1,
                IsRevardEnable = false
            };

            AchievmentData achData6 = new AchievmentData()
            {
                Id = "ACh_6",
                Name = "Коллекционер монеток",
                Description = "Собрать 10 монеток",
                IsUnlock = false,
                TargetValue = 10,
                IsRevardEnable = false
            };

            AchievmentData achData7 = new AchievmentData()
            {
                Id = "ACh_7",
                Name = "Упс",
                Description = "Переродиться",
                IsUnlock = false,
                TargetValue = 1,
                IsRevardEnable = false
            };

            AchievmentsData.Add("ACh_0", achData0);
            AchievmentsData.Add("ACh_1", achData1);
            AchievmentsData.Add("ACh_2", achData2);
            AchievmentsData.Add("ACh_3", achData3);
            AchievmentsData.Add("ACh_4", achData4);
            AchievmentsData.Add("ACh_5", achData5);
            AchievmentsData.Add("ACh_6", achData6);
            AchievmentsData.Add("ACh_7", achData7);
        }
    }
}
