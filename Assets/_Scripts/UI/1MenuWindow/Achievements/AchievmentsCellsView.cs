using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.Loger;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Assets._Scripts.UI._1MenuWindow.Achievements
{
    public class AchievmentsCellsView : MonoBehaviour
    {
        [SerializeField] private GameObject _cellsParent;

        private List<Transform> _cells;
        private int _countAchievments = 0;
        private AchievmentsController _achievmentsController;
        private Func<AchievementView> _achievmentsViewFactory;

        private List<AchievementView> _achievementViews;
        private List<IAchievement> _subscribedAchievements;
        private IGameLogger _gameLogger;

        [Inject]
        public void Construct(AchievmentsController achievmentsController, 
            Func<AchievementView> achievmentsViewFactory,
            IGameLogger gameLogger)
        {
            _achievmentsController = achievmentsController;
            _achievmentsViewFactory = achievmentsViewFactory;
            _achievementViews = new List<AchievementView>();
            _cells = new List<Transform>();
            _subscribedAchievements = new List<IAchievement>();
            _gameLogger = gameLogger;

            _gameLogger.Log("AchievmentsCellsView Construct", "Info");

            for (int i = 0; i < _cellsParent.transform.childCount; i++)
            {
                _cells.Add(_cellsParent.transform.GetChild(i));
            }

            var models = _achievmentsController.GetAchievmentsModels();
            int cellIndex = 0;

            foreach (IAchievement achModel in models)
            {
                var achView = _achievmentsViewFactory();
                achView.Construct(achModel, _gameLogger);

                // Настраиваем позицию ячейки
                if (cellIndex < _cells.Count)
                {
                    achView.transform.SetParent(_cells[cellIndex], false);
                }

                AddAchievment(achView);
                _achievementViews.Add(achView);

                //здесь надо проверку будет сделать...
                achModel.OnUnlock += achView.Unlock;
                achModel.OnChanged += achView.UpdateProgress;
                _subscribedAchievements.Add(achModel);  // ← СОХРАНЯЕМ

                cellIndex++;
            }
        }

        public void OnDestroy()
        {
            _gameLogger.Log("AchievmentsCellsView Dispose", "Warning");

            for (int i = 0; i < _subscribedAchievements.Count; i++)
            {
                var achModel = _subscribedAchievements[i];
                var achView = _achievementViews[i];

                achModel.OnUnlock -= achView.Unlock;
                achModel.OnChanged -= achView.UpdateProgress;
            }

            _subscribedAchievements.Clear();
            _achievementViews.Clear();
        }

        private void Start()
        {
            foreach(var achievementView in _achievementViews)
            {
                achievementView.UpdateProgress();
            }
        }

        private void AddAchievment(AchievementView achievementView)
        {
            _gameLogger.Log("AchievmentsCellsView AddAchievment", "Info");

            if (_countAchievments < _cells.Count) 
            {
                achievementView.transform.SetParent(_cells[_countAchievments].transform, false);

                _countAchievments++;
            }
        }
    }
}
