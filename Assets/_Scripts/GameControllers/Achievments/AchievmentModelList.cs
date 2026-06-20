using Assets._Scripts.EventBusGame;
using Assets._Scripts.Loger;
using Assets._Scripts.SaveLoad.Data;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assets._Scripts.GameControllers.Achievments
{
    public class AchievmentModelList
    {
        private List<IAchievement> _modelsAchievments;
        private EventBus _eventBus;
        private IGameLogger _gameLoger;


        public AchievmentModelList(EventBus eventBus, IGameLogger gameLogger, List<AchievmentData> achievmentData)
        {
            _modelsAchievments = new List<IAchievement>();
            _eventBus = eventBus;
            _gameLoger = gameLogger;
            _modelsAchievments = CreateAchievementModels(_eventBus, _gameLoger, achievmentData);
        }

        public List<IAchievement> GetModel()
        {
            return _modelsAchievments;
        }

        private List<IAchievement> CreateAchievementModels(EventBus eventBus, IGameLogger gameLogger, List<AchievmentData> achievmentData)
        {
            var rewardType1 = new AchievmentsReward(eventBus);
            rewardType1.AddRevard(new CoinReward(eventBus, 10));

            var rewardType2 = new AchievmentsReward(eventBus);
            rewardType2.AddRevard(new GobeletReward(eventBus, 1));

            var rewardType3 = new AchievmentsReward(eventBus);
            rewardType3.AddRevard(new CoinReward(eventBus, 5));
            rewardType3.AddRevard(new GobeletReward(eventBus, 2));

            var modelsAchievments = new List<IAchievement>
                    {
                        new AchievmentModel<StartLevel1>(eventBus, achievmentData[0], rewardType1, gameLogger),
                        new AchievmentModel<StartLevel2>(eventBus, achievmentData[1], rewardType2, gameLogger),
                        new AchievmentModel<StartLevel3>(eventBus, achievmentData[2], rewardType3, gameLogger),
                        new AchievmentModel<FinishLevel1>(eventBus, achievmentData[3], rewardType2, gameLogger),
                        new AchievmentModel<FinishLevel2>(eventBus, achievmentData[4], rewardType2, gameLogger),
                        new AchievmentModel<FinishLevel3>(eventBus, achievmentData[5], rewardType3, gameLogger),
                        new AchievmentModel<CollectGoldEvent>(eventBus, achievmentData[6], rewardType1, gameLogger),
                        new AchievmentModel<DieEvent>(eventBus, achievmentData[7], rewardType2, gameLogger),
                    };

            return modelsAchievments;
        }
    }
}
