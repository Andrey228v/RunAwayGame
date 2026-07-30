using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.SaveLoad.Data.Interfaces;
using Assets.Scripts.SaveLoad.Data;
using UnityEngine;

namespace Assets._Scripts.ObjectsScripts.Points.CheckPoint
{
    public class LastCheckPointController : ISave, ILoad, IDieRestart, IFinish, IReset
    {
        private LastCheckPointModel _model;
        private LevelConfig _levelConfig;

        public void Dispose()
        {

        }

        //Шаг №1 Если не проинициализировано, то создаём новый.
        public void Initialization(LevelData levelData, LevelConfig levelConfig)
        {
            var data = levelData.LastCheckPointPosition;

            if(data == null)
            {
                data = new LastCheckPointData();
                data.position = levelConfig.StartPosition;
                data.rotation = levelConfig.PlayerStartRotation;
                levelData.LastCheckPointPosition = data;
            }

            _model = new LastCheckPointModel(data);

            //_model.SetTransorm(data);
            _levelConfig = levelConfig;
        }

        //Шаг №2 загружаем...
        public void Load(LevelData levelData)
        {
            _model.SetTransorm(levelData.LastCheckPointPosition);
        }


        public void SetData(Vector3 coords)
        {
            var data = new LastCheckPointData(); // под вопсросом...
            data.position = coords;
            _model.SetTransorm(data);
        }

        public void Finish(LevelData levelData)
        {
            //под вопросом...
            var data = new LastCheckPointData();
            data.position = _levelConfig.StartPosition;
            data.rotation = _levelConfig.PlayerStartRotation;

            _model.SetTransorm(data);
        }

        public void DieRestart(LevelData levelData)
        {
            _model.SetTransorm(levelData.LastCheckPointPosition);
        }

        public void ResetAllObjects(LevelConfig levelConfig)
        {
            //под вопросом...
            var data = new LastCheckPointData();
            data.position = _levelConfig.StartPosition;
            data.rotation = _levelConfig.PlayerStartRotation;

            _model.SetTransorm(data);
        }

        public void Save(LevelData levelData)
        {
            levelData.LastCheckPointPosition = _model.Position;
        }
    }
}
