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
    public class PlayerHUDEnteryPoint : IStartable, IDisposable, IInitFinish, IInitRestart //IInitSaveLoad
    {
        private Func<GamePanelController> _gamePanelFactory;
        private GamePanelController _gamePanelController;
        //private SaveLoadService _saveLoadService;
        private GameManager _gameManager;
        private GameFinishController _finishController;
        private GameRestartController _gameRestartController;

        private LevelConfig _levelConfig;

        //public IEnumerable<ISaveLoad> SaveLoads { get; private set; }

        public IEnumerable<IFinish> Finished { get; private set; }

        public IEnumerable<IRestart> Restarted { get; private set; }

        public PlayerHUDEnteryPoint(Func<GamePanelController> gamePanelFactory, 
            GameFinishController gameFinishController,
            GameManager gameManager,
            IEnumerable<IRestart> restarted, IEnumerable<IFinish> finished,
            GameRestartController gameRestartController) 
        {
            _gamePanelFactory = gamePanelFactory;
            //_saveLoadService = saveLoadService;
            _finishController = gameFinishController;
            _gameManager = gameManager;
            //SaveLoads = saveLoads;
            Finished = finished;
            Restarted = restarted;
            _gameRestartController = gameRestartController;
            //_levelConfig = saveLoadService.LevelConfig;
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
            InitSaveLoadData(_levelConfig);
            InitFinishData();
            InitRestartData();
        }

        private void InitEvents()
        {
            _gamePanelController.OnButtonSaveClick += _gameManager.SaveGameSignal;
            _gamePanelController.OnButtonLoadClick += _gameManager.LoadGameSignal;
        }

        public void InitSaveLoadData(LevelConfig levelConfig)
        {
            //_saveLoadService.LoadPartLevelObject(SaveLoads, levelConfig);
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
