using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.Utilites.Loger;
using System.Collections.Generic;

namespace Assets._Scripts.GameControllers.Achievments
{
    public class AchievmentModelList
    {
        private List<IAchievement> _modelsAchievments;
        private IGameLogger _gameLoger;
        private WalletController _walletController;


        public AchievmentModelList(IGameLogger gameLogger, 
            List<AchievmentData> achievmentData,
            WalletController walletController)
        {
            _modelsAchievments = new List<IAchievement>();

            _gameLoger = gameLogger;
            _walletController = walletController;
            _modelsAchievments = CreateAchievementModels(_gameLoger, achievmentData, _walletController);
        }

        public List<IAchievement> GetModel()
        {
            return _modelsAchievments;
        }

        private List<IAchievement> CreateAchievementModels(IGameLogger gameLogger, 
            List<AchievmentData> achievmentData, WalletController walletController)
        {
            var rewardType1 = new AchievmentsReward();
            rewardType1.AddRevard(new CoinReward(10, walletController));

            var rewardType2 = new AchievmentsReward();
            rewardType2.AddRevard(new GobeletReward(1, walletController));

            var rewardType3 = new AchievmentsReward();
            rewardType3.AddRevard(new CoinReward(5, walletController));
            rewardType3.AddRevard(new GobeletReward(2, walletController));

            var modelsAchievments = new List<IAchievement>
                    {
                        //new AchievmentModel<StartLevel1>(eventBus, achievmentData[0], rewardType1, gameLogger),
                        //new AchievmentModel<StartLevel2>(eventBus, achievmentData[1], rewardType2, gameLogger),
                        //new AchievmentModel<StartLevel3>(eventBus, achievmentData[2], rewardType3, gameLogger),
                        //new AchievmentModel<FinishLevel1>(eventBus, achievmentData[3], rewardType2, gameLogger),
                        //new AchievmentModel<FinishLevel2>(eventBus, achievmentData[4], rewardType2, gameLogger),
                        //new AchievmentModel<FinishLevel3>(eventBus, achievmentData[5], rewardType3, gameLogger),
                        //new AchievmentModel<CollectGoldEvent>(eventBus, achievmentData[6], rewardType1, gameLogger),
                        //new AchievmentModel<DieEvent>(eventBus, achievmentData[7], rewardType2, gameLogger),
                    };

            return modelsAchievments;
        }
    }
}
