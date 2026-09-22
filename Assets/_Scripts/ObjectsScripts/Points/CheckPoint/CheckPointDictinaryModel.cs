using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;

namespace Assets._Scripts.ObjectsScripts.Points.CheckPoint
{
    public class CheckPointDictinaryModel
    {
        private readonly Dictionary<string, CheckPointModel> _objectModels;

        public Dictionary<string, CheckPointModel> ObjectModelds => _objectModels;

        public CheckPointDictinaryModel()
        {
            _objectModels = new Dictionary<string, CheckPointModel>();
        }

        public CheckPointModel AddObject(CheckPointData data)
        {
            CheckPointModel model = new CheckPointModel(data);

            if(_objectModels.TryAdd(data.Id, model) == false)
                throw new ArgumentNullException("ERROR KEY");

            return model;
        }

        public bool TryGetModel(string id, out CheckPointModel model)
        {
            bool isFind = false;
            model = null;

            if (_objectModels.TryGetValue(id, out CheckPointModel value))
            {
                model = value;
                isFind = true;
            }

            return isFind;
        }
    }
}
