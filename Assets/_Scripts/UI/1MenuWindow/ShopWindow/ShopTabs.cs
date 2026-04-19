using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Assets._Scripts.GameControllers.GameShop.Views
{
    public enum TabsName
    {
        Person,
        Wings,
        Traces
    }

    public class ShopTabs : MonoBehaviour
    {
        [Header("Tabs")]
        [SerializeField] private List<GameObject> _tabs;

        [Header("Buttons")]
        [SerializeField] private Button _personTabButton;
        [SerializeField] private Button _wingsTabButton;
        [SerializeField] private Button _traceTabButton;

        [Header("Cells")]
        [SerializeField] private GameObject _cellsPersons;
        [SerializeField] private GameObject _cellsWings;
        [SerializeField] private GameObject _cellsTraces;

        private GameObject _currentTab;
        private GameObject _previousTab;

        private SkinController _skinController;
        private FactorySkinsCells _factorySkinsCells;

        [Inject]
        public void Constructor(SkinController skinController, FactorySkinsCells factorySkinsCells)
        {
            _skinController = skinController;
            _factorySkinsCells = factorySkinsCells;
        }


        private void OnEnable()
        {
            _currentTab = _tabs[0];
            _previousTab = null;
        }

        private void Start()
        {
            SetupButtons();

            //INIT....
            //var _cellsPersonsList = _skinController.TransformToCells(_cellsPersons.transform);
            //var _cellsWingsList =  _skinController.TransformToCells(_wingsTabButton.transform);
            //var _cellsSkinList = _skinController.TransformToCells(_cellsTraces.transform);

            //foreach (var cell in _cellsPersonsList)
            //{

            //}




            ShowTab(TabsName.Person);
        }

        private void OnDestroy()
        {
            UnSetupButtons();
        }

        public void ShowTab(TabsName pageName)
        {
            _previousTab = _currentTab;

            if (pageName == TabsName.Person)
            {
                _currentTab = _tabs[0];
            }
            else if (pageName == TabsName.Wings)
            {
                _currentTab = _tabs[1];
            }
            else if (pageName == TabsName.Traces)
            {
                _currentTab = _tabs[2];
            }
            else
            {
                throw new Exception(); // так ли ....
            }

            _previousTab.SetActive(false);
            _currentTab.SetActive(true);
        }


        private void SetupButtons()
        {
            _personTabButton.onClick.AddListener(() => ShowTab(TabsName.Person));
            _wingsTabButton.onClick.AddListener(() => ShowTab(TabsName.Wings));
            _traceTabButton.onClick.AddListener(() => ShowTab(TabsName.Traces));
        }

        private void UnSetupButtons()
        {
            _personTabButton.onClick.RemoveAllListeners();
            _wingsTabButton.onClick.RemoveAllListeners();
            _traceTabButton.onClick.RemoveAllListeners();
        }
    }
}
