using Assets.Scripts.SaveLoad.Data;
using System;
using UnityEngine;

namespace Assets._Scripts.ObjectsScripts.Points.CheckPoint
{
    public class CheckPointModel
    {
        private CheckPointData _data;

        public event Action<string, bool> OnObjectStatusChange;
        public event Action<int> OnTakeValue;
        public event Action<Vector3> OnTakePosition;
        public event Action OnTake;

        public bool IsActivate => _data.IsActivated;

        public CheckPointData Data => _data;

        public CheckPointModel(CheckPointData data)
        {
            _data = data;
        }

        public void SetActivateStatus(bool isActivate)
        {
            _data.IsActivated = isActivate;

            OnObjectStatusChange?.Invoke(_data.Id, _data.IsActivated); // изменяем view
        }

        public void Take(Vector3 coords)
        {
            OnTake?.Invoke(); // сохраняем
            OnTakePosition?.Invoke(coords); // устанавливаем точку последней позиции игрока.
            OnTakeValue?.Invoke(1); // для ачивок, что если мы делаем счёт.
        }

        public void Reset()
        {
            SetActivateStatus(false);
        }
    }
}
