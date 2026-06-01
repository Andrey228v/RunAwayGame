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

        private Func<AchievementView> _achievmentsViewFactory;
        private IGameLogger _gameLogger;

        public event Action OnDestroyCellsView;

        public void OnDestroy()
        {
            OnDestroyCellsView?.Invoke();
        }

        [Inject]
        public void Construct(
            AchievmentsController achievmentsController, 
            Func<AchievementView> achievmentsViewFactory,
            IGameLogger gameLogger)
        {
            _achievmentsViewFactory = achievmentsViewFactory;
            _gameLogger = gameLogger;
            _gameLogger.Log("AchievmentsCellsView Construct", "Info");

            for (int i = 0; i < _cellsParent.transform.childCount; i++)
            {
                achievmentsController.AddCell(_cellsParent.transform.GetChild(i));

                var achView = _achievmentsViewFactory();
                achievmentsController.AddAchievmentView(achView, i);
            }
        }
    }
}
