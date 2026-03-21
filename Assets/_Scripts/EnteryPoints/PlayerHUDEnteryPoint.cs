using Assets._Scripts.UI._1MenuWindow;
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
            //Debug.Log("ZAPUSK PlayerHUDEnteryPoint");
            _gamePanelController = _gamePanelFactory();
        }

        public void Dispose()
        {

        }
    }
}
