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
    public class PlayerHUDEnteryPoint : IStartable, IDisposable
    {
        private Func<GamePanelController> _gamePanelFactory;
        private GamePanelController _gamePanelController;
        private SaveLoadService _saveLoadService;
        private GameCycleController _gameCycleController;
        private GameManager _gameManager;
        private LevelData _loadData;
        private IEnumerable<ISaveLoad> _saveLoads;
        private IEnumerable<IRestart> _restarted;
        private IEnumerable<IFinish> _finished;

        public PlayerHUDEnteryPoint(Func<GamePanelController> gamePanelFactory, 
            SaveLoadService saveService, GameCycleController gameCycleController,
            GameManager gameManager, IEnumerable<ISaveLoad> saveLoads,
            IEnumerable<IRestart> restarted, IEnumerable<IFinish> finished) 
        {
            _gamePanelFactory = gamePanelFactory;
            _saveLoadService = saveService;
            _gameCycleController = gameCycleController;
            _gameManager = gameManager;
            _saveLoads = saveLoads;
            _restarted = restarted;
            _finished = finished;
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
            InitGameCycleData();
        }

        private void InitEvents()
        {
            _gamePanelController.OnButtonSaveClick += _gameManager.SaveGameSignal;
            _gamePanelController.OnButtonLoadClick += _gameManager.LoadGameSignal;
        }

        private void InitSaveLoadData()
        {

            _loadData = _saveLoadService.GetLevelData();

            foreach (var data in _saveLoads)
            {
                data.Load(_loadData);
            }
        }

        private void InitGameCycleData()
        {
            _gameCycleController.AddFinishSub(_gamePanelController); // test
        }
    }
}
