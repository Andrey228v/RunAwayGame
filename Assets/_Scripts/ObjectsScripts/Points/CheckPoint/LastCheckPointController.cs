using Assets._Scripts.SaveLoad.Data.Interfaces;
using Assets.Scripts.SaveLoad.Data;
using UnityEngine;

namespace Assets._Scripts.ObjectsScripts.Points.CheckPoint
{
    public class LastCheckPointController : ISave, ILoad, IDieRestart, IFinish, IReset
    {
        private LastCheckPointModel _model;
        private LevelConfig _levelConfig;


        public LastCheckPointController(LastCheckPointModel model) 
        {
            _model = model; 
        }

        public void Dispose()
        {

        }

        public void Initialization(LevelData levelData, LevelConfig levelConfig)
        {
            _model.SetTransorm(levelConfig.StartPosition);
            _levelConfig = levelConfig;
        }

        public void SetData(Vector3 coords)
        {
            _model.SetTransorm(coords);
        }

        public void Finish(LevelData levelData)
        {
            _model.SetTransorm(_levelConfig.StartPosition);
        }

        public void DieRestart(LevelData levelData)
        {

        }

        public void ResetAllObjects(LevelConfig levelConfig)
        {
            _model.SetTransorm(_levelConfig.StartPosition);
        }

        public void Save(LevelData levelData)
        {
            levelData.LastCheckPointPosition = _model.Position;
        }

        public void Load(LevelData levelData)
        {
            _model.SetTransorm(levelData.LastCheckPointPosition);
        }
    }
}
