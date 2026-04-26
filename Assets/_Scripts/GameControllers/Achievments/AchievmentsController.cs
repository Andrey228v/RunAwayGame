using Assets._Scripts.SaveLoad.Service;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace Assets._Scripts.GameControllers.Achievments
{
    public class AchievmentsController : IStartable, IDisposable //, ISaveLoadService
    {
        private GameSaveLoadService _gameSaveLoadService;
        private GameSaveData _gameSaveData;
        private List<AchievmentModel> _achievments;

        //public Dictionary<Type, ISaveLoadService> Services { get; }

        public AchievmentsController() 
        {
            //_gameSaveLoadService = gameSaveLoadService;
            //_gameSaveData = gameSaveLoadService.GameSaveData;

            //_achievments = new List<AchievmentModel>
            //        {
            //            new AchievmentModel("sLvl 1", "Start lvl 1", false, false),
            //            new AchievmentModel("sLvl 2", "Start lvl 2", false, false),
            //            new AchievmentModel("sLvl 3", "Start lvl 3", false, false),
            //            new AchievmentModel("fLvl 1", "Finish lvl 1", false, false),
            //            new AchievmentModel("fLvl 2", "Finish lvl 2", false, false),
            //            new AchievmentModel("fLvl 3", "Finish lvl 3", false, false),
            //        };
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
            
        }

        public void LoadAllServices(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            
        }
    }
}
