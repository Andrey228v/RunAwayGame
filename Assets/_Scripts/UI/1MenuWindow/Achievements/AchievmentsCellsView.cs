using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.UI._1MenuWindow.Achievements
{
    public class AchievmentsCellsView : MonoBehaviour
    {
        [SerializeField] private GameObject _cellsParent;

        private List<Transform> _cells;
        private int _countAchievments = 0;

        private void Awake()
        {
            _cells = new List<Transform>();

            for (int i = 0; i < _cellsParent.transform.childCount; i++) 
            {
                _cells.Add(_cellsParent.transform.GetChild(i));
            }
        }

        public void AddAchievment(AchievementView achievementView)
        {
            if (_countAchievments < _cells.Count) 
            {
                achievementView.transform.SetParent(_cells[_countAchievments].transform, false);

                _countAchievments++;
            }
        }
    }
}
