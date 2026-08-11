using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UI._1MenuWindow.Language
{
    public class LanguageViewMenu: MonoBehaviour, ILanguageViewMenu
    {
        [SerializeField] private Button _mainLanguageButton;
        [SerializeField] private Transform _buttonsParent;
        [SerializeField] private List<Button> _buttonsLanguages;

        public event Action<int> OnRusLanguageChoose;
        public event Action<int> OnUSALanguageChoose;
        public event Action<int> OnTurkeyLanguageChoose;

        //Данный класс сделан в простом стиле, без мысли что будет расширяться.
        private void Start()
        {
            _buttonsLanguages[0].onClick.AddListener(() => SetRusLanguage(0));
            _buttonsLanguages[1].onClick.AddListener(() => SetUSALanguage(1));
            _buttonsLanguages[2].onClick.AddListener(() => SetTurkeyLanguage(2));
        }

        private void OnDestroy()
        {
            _buttonsLanguages[0].onClick.RemoveListener(() => SetRusLanguage(0));
            _buttonsLanguages[1].onClick.RemoveListener(() => SetUSALanguage(1));
            _buttonsLanguages[2].onClick.RemoveListener(() => SetTurkeyLanguage(2));
        }

        public void UpdateView(bool isActivate)
        {
            if(isActivate == true)
            {
                _buttonsParent.gameObject.SetActive(true);
            }
            else
            {
                _buttonsParent.gameObject.SetActive(false);
            }
        }

        private void SetRusLanguage(int id)
        {
            OnRusLanguageChoose?.Invoke(id);
        }

        private void SetUSALanguage(int id)
        {
            OnUSALanguageChoose?.Invoke(id);
        }

        private void SetTurkeyLanguage(int id)
        {
            OnTurkeyLanguageChoose?.Invoke(id);
        }

    }
}
