using Assets._Scripts.Loger;
using Assets._Scripts.ObjectsScripts.Coins;
using Assets._Scripts.SaveLoad.Data;
using Assets.Scripts.Player;
using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using System.Collections.Generic;
using VContainer.Unity;

namespace Assets._Scripts.GameControllers.Levels
{
    public class LevelsController : IStartable
    {
        private bool _isLevelWasStart;
        private PlayerController _playerController;
        private CoinController _coinController;
        private CheckPointsController _checkPointsController;
        private IGameLogger _gameLogger;

        private List<IInitialzation> _initList;
        private List<ISave> _saveList;
        private List<ILoad> _loadList;
        private List<IRestart> _restartList;
        private List<IFinish> _finishList;

        public LevelsController(IGameLogger gameLogger)
        {
            _isLevelWasStart = false;
            _gameLogger = gameLogger;

            _initList = new List<IInitialzation>();
            _saveList = new List<ISave>();
            _loadList = new List<ILoad>();
            _restartList = new List<IRestart>();
            _finishList = new List<IFinish>();
        }

        public void Start()
        {
            _isLevelWasStart = true;
        }

        public void Dispose()
        {
            //_playerController?.Dispose();
            //_coinController?.Dispose();
            //_checkPointsController?.Dispose();

            //_playerController = null;
            //_coinController = null;
            //_checkPointsController = null;

            _initList.Clear();
            _saveList.Clear();
            _loadList.Clear();
            _restartList.Clear();
            _finishList.Clear();
        }

        public void Initialize(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            // под вопросом...
            //_coinController.Initialize();

            foreach(IInitialzation init in _initList)
            {
                init.Initialzation(gameSaveData, levelConfig);
            }
        }

        //public void SetPlayerController(PlayerController playerController)
        //{
        //    _playerController = playerController;
        //    //_playerController?.LoadAllServices(gameSaveData, levelConfig);
        //}

        //public void SetCoinController(CoinController coinController) 
        //{
        //    _coinController = coinController;
        //}

        //public void SetCheckPointsController(CheckPointsController checkPointsController)
        //{
        //    _checkPointsController = checkPointsController;
        //}

        public void SaveAllServices(GameSaveData gameSaveData, LevelConfig levelConfig)
        {

            //if (_playerController == null || _coinController == null || _checkPointsController == null)
            //    return;

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

            //_playerController.Save(gameSaveData, levelConfig);
            //_coinController.Save(gameSaveData, levelConfig);
            //_checkPointsController.Save(gameSaveData, levelConfig);
        }

        public void LoadAllServices(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            if(levelConfig == null)
            {
                return;
            }

            var levelsData = gameSaveData.LevelsData;

            if (gameSaveData.LevelsData.TryGetValue(levelConfig.LevelName, out LevelData levelData) == false)
            {
                LevelData newLevelData = new LevelData(false, levelConfig.StartPosition, new PlayerData(), new List<CheckPointData>(), new List<CoinData>()) { };
                gameSaveData.LevelsData.Add(levelConfig.LevelName, levelData);
            }

            foreach (ILoad load in _loadList) 
            {
                load.Load(gameSaveData, levelConfig);
            }

            //if (_playerController == null || _coinController == null || _checkPointsController == null)
            //{
            //    _gameLogger.LogError("_plaer, _coin, _ckeck NULL");
            //}

            //_playerController.Initialzation(gameSaveData, levelConfig);
            //_coinController.Initialzation(gameSaveData, levelConfig);
            //_checkPointsController.Initialize(gameSaveData, levelConfig);

            //_playerController.Load(gameSaveData, levelConfig);
            //_coinController.Load(gameSaveData, levelConfig);
            //_checkPointsController.LoadAllServices(gameSaveData, levelConfig);
        }

        public void DieRestart(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            var LevelData = gameSaveData.LevelsData[levelConfig.LevelName];

            foreach(IRestart restart in _restartList)
            {
                restart.Restart(LevelData);
            }

            //_playerController.DieRestart(LevelData);
        }

        public void LoadLevel()
        {

        }

        public void FinishLevel(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            //_playerController.FinishGame(gameSaveData, levelConfig);
            //_coinController.FinishGame();
            //_checkPointsController.FinishGame();

            foreach (IFinish finish in _finishList) 
            {
                finish.Finish(gameSaveData, levelConfig);
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
    }
}
