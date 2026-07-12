using Assets._Scripts.GameMVP.Achievments;
using Assets._Scripts.GameMVP.Levels;
using Assets._Scripts.SaveLoad.Data.Interfaces.Game;
using Assets._Scripts.Utilites.Loger;
using Assets.Scripts.SaveLoad.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.GameControllers.Levels
{
    public class LevelsController : IInitGame, ISaveGame, ILoadGame, IDieRestartGame, IFinishGame, IResetGame
    {
        private IGameLogger _gameLogger;
        private Transform _objectParent;
        private readonly LevelsDictinaryModel _dictinaryModel;
        private readonly Dictionary<string, LevelUIView> _dictinaryView;

        private LevelConfig _levelConfig;

        public LevelConfig Config => _levelConfig;

        public LevelsController(IGameLogger gameLogger, LevelsDictinaryModel dictinaryModel)
        {
            _gameLogger = gameLogger;
            _dictinaryModel = dictinaryModel;
        }

        public void Initialization()
        {


            //if (gameSaveData == null)
            //{
            //    throw new ArgumentNullException(nameof(gameSaveData), "gameSaveData cannot be null");
            //}

            //if (gameSaveData.LevelsData.TryGetValue(_levelConfig.LevelName, out LevelData levelData) == false)
            //{
            //    LevelData newLevelData = new LevelData
            //        (
            //            false,
            //            _levelConfig.StartPosition,
            //            new PlayerData()
            //            {
            //                PlayerPosition = _levelConfig.StartPosition,
            //                PlayerRotation = _levelConfig.PlayerStartRotation
            //            },
            //            new Dictionary<string, CheckPointData>(),
            //            new Dictionary<string, CoinData>()
            //        ); { };

            //    gameSaveData.LevelsData.Add(_levelConfig.LevelName, newLevelData);
            //}
        }

        public void SetLevelConfig(LevelConfig levelConfig)
        {
            _levelConfig = levelConfig;
        }

        public void Save(GameSaveData gameSaveData)
        {

        }

        public void Load(GameSaveData gameSaveData) 
        {

        }

        public void DieRestart(GameSaveData gameSaveData)
        {

        }

        public void Finish(GameSaveData gameSaveData) 
        {

        }

        public void Reset(GameSaveData gameSaveData)
        {

        }
    }
}
