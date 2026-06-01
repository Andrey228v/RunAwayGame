using Assets._Scripts.EventBusGame;
using Assets._Scripts.Loger;
using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.UI;
using Assets._Scripts.UI._1MenuWindow;
using Assets.Scripts.SaveLoad.Data;
using Assets.Scripts.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assets._Scripts.GameControllers.Wallets
{
    public class WalletController
    {
        //private int _coins;
        //private int _gobelets;
        private WalletModel _model;
        
        private EventBus _eventBus;
        private IGameLogger _gameLogger;

        private MenuTabs _menuView;
        private GamePanelController _gamePanelView;
        private UnitInfoUI _unitInfoUIView;


        //public int Coins => _coins;

        //public int Gobelets => _gobelets;

        public WalletController(EventBus eventBus, IGameLogger gameLogger)
        {
            _eventBus = eventBus;
            _gameLogger = gameLogger;
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<AddCoinsEvent>(OnCoinsChanged);
            _eventBus.Unsubscribe<AddGobeletsEvent>(OnGobeletsChanged);
        }

        public void Initialize()
        {
            //_coins = 0;
            //_gobelets = 0;

            _eventBus.Subscribe<AddCoinsEvent>(OnCoinsChanged);
            _eventBus.Subscribe<AddGobeletsEvent>(OnGobeletsChanged);
        }

        public void SaveAllServices(GameSaveData gameSaveData)
        {
            //gameSaveData.WalletData.Coins = _coins;
            //gameSaveData.WalletData.Gobelets = _gobelets;
        }

        public void LoadAllServices(GameSaveData gameSaveData)
        {
            if(gameSaveData.WalletData == null)
            {
                gameSaveData.WalletData = new WalletData();
            }

            //_coins = gameSaveData.WalletData.Coins;
            //_gobelets = gameSaveData.WalletData.Gobelets;
        }

        public void AddMenuView(MenuTabs menuView)
        {
            _menuView = menuView;
        }

        private void OnCoinsChanged(AddCoinsEvent args)
        {
            _gameLogger.Log("OnCoinsChanged", "Event");

            //_coins += args.CoinCount;


            _eventBus.Publish(new SaveGameEvent { });
            _eventBus.Publish(new UpdateUIEvent { });


        }

        private void OnGobeletsChanged(AddGobeletsEvent args)
        {
            _gameLogger.Log("OnGobeletsChanged", "Event");

            //_gobelets += args.GobeletCount;


            _eventBus.Publish(new SaveGameEvent { });
            _eventBus.Publish(new UpdateUIEvent { });
        }
    }
}
