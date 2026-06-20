using Assets._Scripts.EventBusGame;
using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.Loger;
using Assets._Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Assets._Scripts.UI._1MenuWindow.Achievements
{
    public class AchievmentsCellsView : MonoBehaviour
    {
        [SerializeField] private GameObject _cellsParent;

        public GameObject CellsParent => _cellsParent;

        public event Action OnDestroyCellsView;

        public void OnDestroy()
        {
            OnDestroyCellsView?.Invoke();
        }
    }
}
