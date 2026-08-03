using Assets._Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.SaveLoad.Data
{
    [Serializable]
    public class GameSaveData
    {
        public Dictionary<string, LevelData> LevelsData = new Dictionary<string, LevelData>(); // LevelController
        public Dictionary<string, AchievmentData> AchievmentsData; // AchievmentsController
        public ShopData ShopData; // ShopController
        public WalletData WalletData;
        public DateTime LastSaveTime;

        public GameSaveData(Dictionary<string, LevelData> levelsData,
            Dictionary<string, AchievmentData> achievmentsData, 
            ShopData shopData, 
            WalletData walletData, 
            DateTime lastSaveTime)
        {
            LevelsData = levelsData;
            AchievmentsData = achievmentsData;
            ShopData = shopData;
            WalletData = walletData;
            LastSaveTime = lastSaveTime;

            AchievmentData achData0 = new AchievmentData()
            {
                Id = "ACh_0",
                Name = "sLvl 1",
                Description = "Start lvl 1",
                IsUnlock = false,
                TargetValue = 1,
                IsRevardEnable = false
            };

            AchievmentData achData1 = new AchievmentData()
            {
                Id = "ACh_1",
                Name = "sLvl 2",
                Description = "Start lvl 2",
                IsUnlock = false,
                TargetValue = 1,
                IsRevardEnable = false
            };

            AchievmentData achData2 = new AchievmentData()
            {
                Id = "ACh_2",
                Name = "sLvl 3",
                Description = "Start lvl 3",
                IsUnlock = false,
                TargetValue = 1,
                IsRevardEnable = false
            };

            AchievmentData achData3 = new AchievmentData()
            {
                Id = "ACh_3",
                Name = "fLvl 1",
                Description = "Finish lvl 1",
                IsUnlock = false,
                TargetValue = 1,
                IsRevardEnable = false
            };

            AchievmentData achData4 = new AchievmentData()
            {
                Id = "ACh_4",
                Name = "fLvl 2",
                Description = "Finish lvl 2",
                IsUnlock = false,
                TargetValue = 1,
                IsRevardEnable = false
            };

            AchievmentData achData5 = new AchievmentData()
            {
                Id = "ACh_5",
                Name = "fLvl 3",
                Description = "Finish lvl 3",
                IsUnlock = false,
                TargetValue = 1,
                IsRevardEnable = false
            };

            AchievmentData achData6 = new AchievmentData()
            {
                Id = "ACh_6",
                Name = "CollectGols",
                Description = "Collect 10 gold",
                IsUnlock = false,
                TargetValue = 10,
                IsRevardEnable = false
            };

            AchievmentData achData7 = new AchievmentData()
            {
                Id = "ACh_7",
                Name = "Die",
                Description = "Die",
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
