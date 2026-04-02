using Assets._Scripts.EnteryPoints.Interfaces;
using Assets._Scripts.GameControllers;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using Assets.Scripts.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class PlayerHUDEnteryPoint : IStartable, IDisposable, IInitSaveLoad, IInitFinish, IInitRestart
    {
        private Func<GamePanelController> _gamePanelFactory;
        private GamePanelController _gamePanelController;
        private SaveLoadService _saveLoadService;
        //private LevelData _loadData;
        private GameManager _gameManager;
        private GameFinishController _finishController;
        private GameRestartController _gameRestartController;

        public IEnumerable<ISaveLoad> SaveLoads { get; private set; }

        public IEnumerable<IFinish> Finished { get; private set; }

        public IEnumerable<IRestart> Restarted { get; private set; }

        public PlayerHUDEnteryPoint(Func<GamePanelController> gamePanelFactory, 
            SaveLoadService saveService, GameFinishController gameFinishController,
            GameManager gameManager, IEnumerable<ISaveLoad> saveLoads,
            IEnumerable<IRestart> restarted, IEnumerable<IFinish> finished,
            GameRestartController gameRestartController) 
        {
            _gamePanelFactory = gamePanelFactory;
            _saveLoadService = saveService;
            _finishController = gameFinishController;
            _gameManager = gameManager;
            SaveLoads = saveLoads;
            Finished = finished;
            Restarted = restarted;
            _gameRestartController = gameRestartController;
        }

        public void Dispose()
        {
            Debug.Log("HUD Destoy");

            _gamePanelController.OnButtonSaveClick -= _gameManager.SaveGameSignal;
            _gamePanelController.OnButtonLoadClick -= _gameManager.LoadGameSignal;
        }

        public void Start()
        {
            _gamePanelController = _gamePanelFactory();
            
            InitEvents();
            InitSaveLoadData();
            InitFinishData();
            InitRestartData();
        }

        private void InitEvents()
        {
            _gamePanelController.OnButtonSaveClick += _gameManager.SaveGameSignal;
            _gamePanelController.OnButtonLoadClick += _gameManager.LoadGameSignal;
        }

        public void InitSaveLoadData()
        {

            //_loadData = _saveLoadService.GetLevelData();

            _saveLoadService.LoadPartLevelObject(SaveLoads);
        }

        public void InitFinishData()
        {
            _finishController.AddFinishSub(Finished);
            _finishController.AddFinishSub(_gamePanelController);
        }

        public void InitRestartData()
        {
            _gameRestartController.AddRestartSub(Restarted);
        }
    }
}
