using Assets._Scripts.EventBusGame;
using Assets._Scripts.SaveLoad.Service;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace Assets._Scripts.GameControllers.Achievments
{
    public class AchievmentsController : IStartable, IDisposable
    {
        private GameSaveLoadService _gameSaveLoadService;
        private GameSaveData _gameSaveData;
        private List<IAchievement> _achievments;
        private EventBus _eventBus;
        

        //public Dictionary<Type, ISaveLoadService> Services { get; }

        public AchievmentsController(EventBus eventBus) 
        {
            //_gameSaveLoadService = gameSaveLoadService;
            //_gameSaveData = gameSaveLoadService.GameSaveData;

            _eventBus = eventBus;

            var rewardType1 = new AchievmentsReward(_eventBus);
            rewardType1.AddRevard(new CoinReward(_eventBus, 10));

            var rewardType2 = new AchievmentsReward(_eventBus);
            rewardType2.AddRevard(new GobeletReward(_eventBus, 1));

            var rewardType3 = new AchievmentsReward(_eventBus);
            rewardType3.AddRevard(new CoinReward(_eventBus, 5));
            rewardType3.AddRevard(new GobeletReward(_eventBus, 2));

            _achievments = new List<IAchievement>
                    {
                        new AchievmentModel<StartLevel1>(_eventBus, "sLvl 1", "Start lvl 1", false, false, rewardType1),
                        new AchievmentModel<StartLevel2>(_eventBus, "sLvl 2", "Start lvl 2", false, false, rewardType2),
                        new AchievmentModel<StartLevel3>(_eventBus, "sLvl 3", "Start lvl 3", false, false, rewardType3),
                        new AchievmentModel<FinishLevel1>(_eventBus, "fLvl 1", "Finish lvl 1", false, false, rewardType2),
                        new AchievmentModel<FinishLevel2>(_eventBus, "fLvl 2", "Finish lvl 2", false, false, rewardType2),
                        new AchievmentModel<FinishLevel3>(_eventBus, "fLvl 3", "Finish lvl 3", false, false, rewardType3),
                    };
        }

        public void Start()
        {


            //if (_gameSaveData.AchievmentsModels.Count == 0 || _gameSaveData.AchievmentsModels == null)
            //{
            //    _gameSaveData.AchievmentsModels = _achievments;
            //}
            //else
            //{
            //    //тут надо дописать момент, что если мы изменяем список ачивок, то он должен пробигать по этом списку
            //    //сравнивать ID и перезаписывать ачивки, потому что получается так что список сейчас жёстко зафиксирован.
            //}
        }

        public void Dispose()
        {

        }

        public void Initialize()
        {

        }

        //public void AddSerice(ISaveLoadService service)
        //{

        //}

        public void SaveAllServices(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            //gameSaveData.AchievmentsData
        }

        public void LoadAllServices(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            
        }

        public List<IAchievement> GetAchievmentsModels()
        {
            return _achievments;
        }
    }
}
