using Assets._Scripts.GameControllers.Achievments;
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

        [Inject]
        public void Construct(AchievmentsController achievmentsController, Func<AchievementView> achievmentsViewFactory)
        {
            _achievmentsController = achievmentsController;
            _achievmentsViewFactory = achievmentsViewFactory;
            _achievementViews = new List<AchievementView>();
            _cells = new List<Transform>();

            for (int i = 0; i < _cellsParent.transform.childCount; i++)
            {
                _cells.Add(_cellsParent.transform.GetChild(i));
            }

            var models = _achievmentsController.GetAchievmentsModels();

            foreach (AchievmentModel ach in models)
            {
                var achView = _achievmentsViewFactory();
                achView.Construct(ach);
                AddAchievment(achView);
                _achievementViews.Add(achView);
            }
        }


        private void Awake()
        {
            


        }

        private void AddAchievment(AchievementView achievementView)
        {
            if (_countAchievments < _cells.Count) 
            {
                achievementView.transform.SetParent(_cells[_countAchievments].transform, false);

                _countAchievments++;
            }
        }
    }
}
