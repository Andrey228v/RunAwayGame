using System;
using UnityEngine;

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
