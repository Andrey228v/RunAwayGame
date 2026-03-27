using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UI._2GameHUD
{
    public class GameWinPanel : MonoBehaviour, IPanel
    {
        [Header("Buttons")]
        [SerializeField] private Button _backToMenuButton;
        [SerializeField] private Button _reloudButton;

        public event Action OnBackToMenuButtonClick;
        public event Action OnReloudButtonClick;

        public bool IsVisible { get; private set; }

        public string Name {  get;  set; } 

#if UNITY_EDITOR
        public void OnValidate()
        {
            if (_backToMenuButton == null)
            {
                Debug.LogError($"{gameObject.name}: _backToMenuButton is not set!", this);
            }

            if (_reloudButton == null)
            {
                Debug.LogError($"{gameObject.name}: _reloudButton is not set!", this);
            }
        }
#endif

        private void OnEnable()
        {
            _backToMenuButton.onClick.AddListener(ClickBackToMenu);
            _reloudButton.onClick.AddListener(ReloudLevel);
        }

        private void OnDisable()
        {
            _backToMenuButton.onClick.RemoveListener(ClickBackToMenu);
            _reloudButton.onClick.RemoveListener(ReloudLevel);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            IsVisible = true;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            IsVisible = false;
        }

        private void ClickBackToMenu()
        {
            OnBackToMenuButtonClick?.Invoke();
        }

        private void ReloudLevel()
        {
            OnReloudButtonClick?.Invoke();
        }
    }
}
