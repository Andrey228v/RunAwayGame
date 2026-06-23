using Assets._Scripts.EventBusGame;
using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.Utilites.Loger;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using System.Collections.Generic;
using VContainer.Unity;

namespace Assets._Scripts.GameControllers.Levels
{
    public class LevelsController : IStartable
    {
        private bool _isLevelWasStart;
        private IGameLogger _gameLogger;
        private EventBus _eventBus;

        private List<IInitialzation> _initList;
        private List<ISave> _saveList;
        private List<ILoad> _loadList;
        private List<IRestart> _restartList;
        private List<IFinish> _finishList;

        private LevelConfig _levelConfig;

        public LevelsController(IGameLogger gameLogger, EventBus eventBus)
        {
            _isLevelWasStart = false;
            _gameLogger = gameLogger;

            _initList = new List<IInitialzation>();
            _saveList = new List<ISave>();
            _loadList = new List<ILoad>();
            _restartList = new List<IRestart>();
            _finishList = new List<IFinish>();
            _eventBus = eventBus;
        }

        public void Start()
        {
            _isLevelWasStart = true;

            _eventBus.Subscribe<ChooseLevelEvent>(OnSetLevelConfig);
        }

        public void Dispose()
        {
            _initList.Clear();
            _saveList.Clear();
            _loadList.Clear();
            _restartList.Clear();
            _finishList.Clear();

            _eventBus.Unsubscribe<ChooseLevelEvent>(OnSetLevelConfig);
        }

        public void Initialize(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            //Тут работает так, что _initList пустой при инициализации...
            foreach(IInitialzation init in _initList)
            {
                init.Initialzation(gameSaveData, levelConfig);
            }
        }

        public void SaveAllServices(GameSaveData gameSaveData, LevelConfig levelConfig)
        {

            if (gameSaveData.LevelsData.TryGetValue(levelConfig.LevelName, out LevelData levelData))
            {
                levelData.IsLevelWasStarted = _isLevelWasStart;
            }
            else
            {
                LevelData newLevelData = new LevelData(false, 
                    levelConfig.StartPosition, 
                    new PlayerData(), 
                    new List<CheckPointData>(), 
                    new List<CoinData>()){ };

                gameSaveData.LevelsData.Add(levelConfig.LevelName, newLevelData);
            }

            foreach(ISave save in _saveList)
            {
                save.Save(gameSaveData, levelConfig);
            }
        }

        public void LoadAllServices(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            if(levelConfig == null)
            {
                return;
            }

            if (gameSaveData.LevelsData.TryGetValue(levelConfig.LevelName, out LevelData levelData) == false)
            {
                LevelData newLevelData = new LevelData(false, levelConfig.StartPosition, new PlayerData(), new List<CheckPointData>(), new List<CoinData>()) { };
                gameSaveData.LevelsData.Add(levelConfig.LevelName, newLevelData);
            }

            foreach (ILoad load in _loadList) 
            {
                load.Load(gameSaveData, levelConfig);
            }
        }

        public void DieRestart(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            var LevelData = gameSaveData.LevelsData[levelConfig.LevelName];

            foreach(IRestart restart in _restartList)
            {
                restart.Restart(LevelData);
            }
        }

        public void LoadLevel()
        {

        }

        public void FinishLevel(GameSaveData gameSaveData, LevelConfig levelConfig, LevelCompletedEvent args)
        {
            foreach (IFinish finish in _finishList) 
            {
                finish.Finish(gameSaveData, levelConfig);
            }

            if (args.lvlId == "0") // переделать. Это не тут должно быть
            {
                _eventBus.Publish(new FinishLevel1() { Progress = 1 });
            }
            else if (args.lvlId == "1")
            {
                _eventBus.Publish(new FinishLevel2() { Progress = 1 });
            }
            else if (args.lvlId == "2")
            {
                _eventBus.Publish(new FinishLevel3() { Progress = 1 });
            }
        }

        public void AddInitialization(IInitialzation init)
        {
            _initList.Add(init);
        }

        public void AddSave(ISave save)
        {
            _saveList.Add(save);
        }

        public void AddLoad(ILoad load)
        {
            _loadList.Add(load);
        }

        public void AddRestart(IRestart restart)
        {
            _restartList.Add(restart);
        }

        public void AddFinish(IFinish finish)
        {
            _finishList.Add(finish);
        }

        public void SetLevelConfig(LevelConfig levelConfig)
        {
            _levelConfig = levelConfig;
        }

        public void OnSetLevelConfig(ChooseLevelEvent args)
        {
            _gameLogger.Log("GameSaveLoadService set Level Config", "Service");
            _levelConfig = args.levelConfig;
        }
    }
}
