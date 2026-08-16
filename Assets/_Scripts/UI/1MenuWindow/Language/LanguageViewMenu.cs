using Assets._Scripts.GameMVP.Language;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UI._1MenuWindow.Language
{
    public interface ILanguageView
    {
        void UpdateLanguageDisplay(LanguageType language);
        void UpdateVisibility(bool isVisible);
        event Action OnToggleClicked;
        event Action<LanguageType> OnLanguageSelected;
    }

    public class LanguageViewMenu: MonoBehaviour, ILanguageView
    {
        [SerializeField] private Button _mainLanguageButton;
        [SerializeField] private Transform _buttonsParent;
        [SerializeField] private List<Button> _languageButtons;

        public event Action OnToggleClicked;
        public event Action<LanguageType> OnLanguageSelected;

        private void Start()
        {
            InitializeButtons();
        }

        private void InitializeButtons()
        {
            for (int i = 0; i < _languageButtons.Count; i++)
            {
                var button = _languageButtons[i];
                var language = (LanguageType)i;
                button.onClick.AddListener(() => OnLanguageSelected?.Invoke(language));
            }

            _mainLanguageButton.onClick.AddListener(() => OnToggleClicked?.Invoke());
        }

        private void OnDestroy()
        {
            _mainLanguageButton.onClick.RemoveAllListeners();

            foreach (var button in _languageButtons)
            {
                button.onClick.RemoveAllListeners();
            }
        }

        public void UpdateLanguageDisplay(LanguageType language)
        {
            var targetButton = _languageButtons[(int)language];
            _mainLanguageButton.GetComponent<Image>().sprite = targetButton.GetComponent<Image>().sprite;
        }

        public void UpdateVisibility(bool isVisible)
        {
            _buttonsParent.gameObject.SetActive(isVisible);
        }
    }
}
