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

        public PlayerHUDEnteryPoint(Func<GamePanelController> gamePanelFactory, SaveLoadService saveService) 
        {
            _gamePanelFactory = gamePanelFactory;
            _saveLoadService = saveService;
        }

        public void Start()
        {
            _gamePanelController = _gamePanelFactory();
            _gamePanelController.OnButtonSaveClick += _saveLoadService.SaveLevelData;
            _gamePanelController.OnButtonLoadClick += _saveLoadService.LoadLevel;

        }

        public void Dispose()
        {
            Debug.Log("HUD Destoy");
            _gamePanelController.OnButtonSaveClick -= _saveLoadService.SaveLevelData;
            _gamePanelController.OnButtonLoadClick -= _saveLoadService.LoadLevel;
        }
    }
}
