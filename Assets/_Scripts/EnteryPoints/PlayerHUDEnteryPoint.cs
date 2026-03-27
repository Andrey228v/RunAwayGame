using Assets._Scripts.GameControllers;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.UI;
using System;
using UnityEngine;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class PlayerHUDEnteryPoint : IStartable, IDisposable
    {
        private Func<GamePanelController> _gamePanelFactory;
        private GamePanelController _gamePanelController;
        private SaveLoadService _saveLoadService;
        //private GameCycleController _gameCycleController;

        public PlayerHUDEnteryPoint(Func<GamePanelController> gamePanelFactory, 
            SaveLoadService saveService) 
        {
            _gamePanelFactory = gamePanelFactory;
            _saveLoadService = saveService;

            //GameCycleController gameCycleController
            //_gameCycleController = gameCycleController;
        }

        public void Start()
        {
            _gamePanelController = _gamePanelFactory();
            _gamePanelController.OnButtonSaveClick += _saveLoadService.SaveLevelData;
            _gamePanelController.OnButtonLoadClick += _saveLoadService.LoadLevel;
            //_gamePanelController.OnRestartButtonClick += _gameCycleController.RestartNotifySubs; // ???

        }

        public void Dispose()
        {
            Debug.Log("HUD Destoy");
            _gamePanelController.OnButtonSaveClick -= _saveLoadService.SaveLevelData;
            _gamePanelController.OnButtonLoadClick -= _saveLoadService.LoadLevel;
            //_gamePanelController.OnRestartButtonClick -= _gameCycleController.RestartNotifySubs; // ???
        }
    }
}
