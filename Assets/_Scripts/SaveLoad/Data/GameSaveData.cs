using Assets._Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.SaveLoad.Data
{
    [Serializable]
    public class GameSaveData
    {
        public Dictionary<string, LevelData> LevelsData = new Dictionary<string, LevelData>(); // LevelController
        public List<AchievmentData> AchievmentsData; // AchievmentsController
        public ShopData ShopData; // ShopController
        public WalletData WalletData;
        public DateTime LastSaveTime;

        public GameSaveData(Dictionary<string, LevelData> levelsData,
            List<AchievmentData> achievmentsData, 
            ShopData shopData, 
            WalletData walletData, 
            DateTime lastSaveTime)
        {
            LevelsData = levelsData;
            AchievmentsData = achievmentsData;
            ShopData = shopData;
            WalletData = walletData;
            LastSaveTime = lastSaveTime;

            AchievmentData achData1 = new AchievmentData()
            {
                Id = 0,
                Name = "sLvl 1",
                Description = "Start lvl 1",
                IsUnlock = false,
                IsRevardEnable = false
            };

            AchievmentData achData2 = new AchievmentData()
            {
                Id = 1,
                Name = "sLvl 2",
                Description = "Start lvl 2",
                IsUnlock = false,
                IsRevardEnable = false
            };

            AchievmentData achData3 = new AchievmentData()
            {
                Id = 2,
                Name = "sLvl 3",
                Description = "Start lvl 3",
                IsUnlock = false,
                IsRevardEnable = false
            };

            AchievmentData achData4 = new AchievmentData()
            {
                Id = 3,
                Name = "fLvl 1",
                Description = "Finish lvl 1",
                IsUnlock = false,
                IsRevardEnable = false
            };

            AchievmentData achData5 = new AchievmentData()
            {
                Id = 4,
                Name = "fLvl 2",
                Description = "Finish lvl 2",
                IsUnlock = false,
                IsRevardEnable = false
            };

            AchievmentData achData6 = new AchievmentData()
            {
                Id = 5,
                Name = "fLvl 3",
                Description = "Finish lvl 3",
                IsUnlock = false,
                IsRevardEnable = false
            };

            AchievmentData achData7 = new AchievmentData()
            {
                Id = 6,
                Name = "CollectGols",
                Description = "Collect 10 gold",
                IsUnlock = false,
                IsRevardEnable = false
            };

            AchievmentData achData8 = new AchievmentData()
            {
                Id = 7,
                Name = "Die",
                Description = "Die",
                IsUnlock = false,
                IsRevardEnable = false
            };

            AchievmentsData.Add(achData1);
            AchievmentsData.Add(achData2);
            AchievmentsData.Add(achData3);
            AchievmentsData.Add(achData4);
            AchievmentsData.Add(achData5);
            AchievmentsData.Add(achData6);
            AchievmentsData.Add(achData7);
            AchievmentsData.Add(achData8);
        }

        //public void ResetData()
        //{
        //    foreach (var key in LevelsData.Keys) 
        //    {
        //        //LevelsData[key].ResetData();
        //    }

        //    foreach(var ach in AchievmentsData)
        //    {
        //        ach.ResetData();
        //    }

        //    ShopData.ResetData();
        //    WalletData.ResetData();

        //}
    }
}
