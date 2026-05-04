using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.ObjectsScripts.Coins;
using Assets._Scripts.SaveLoad.Service;
using Assets.Scripts.Points;
using System;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class LevelEnteryPoint : IStartable, IDisposable
    {
        private CheckPointsController _checkPointsController;
        private CoinController _coinController;
        private LevelsController _levelsController;

        public LevelEnteryPoint(GamePoints gamePoints,
            CheckPointsController checkPointsController, 
            CoinController coinController,
            GameSaveLoadService gameSaveLoadService,
            LevelsController levelsController)
        {
            _checkPointsController = checkPointsController;
            _coinController = coinController;
            _levelsController = levelsController;
        }

        public void Start()
        {
            _levelsController.SetCoinController(_coinController);
            _levelsController.SetCheckPointsController(_checkPointsController);
        }

        public void Dispose()
        {
            _checkPointsController.Dispose();
            _coinController.Dispose();
        }
    }
}
