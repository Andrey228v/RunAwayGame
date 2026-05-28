using Assets._Scripts.EventBusGame;
using Assets._Scripts.SaveLoad.Data;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assets._Scripts.GameControllers.Wallets
{
    public class WalletController
    {
        private int _coins;
        private int _gobelets;
        private EventBus _eventBus;

        public int Coins => _coins;

        public int Gobelets => _gobelets;

        public WalletController(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<AddCoinsEvent>(OnCoinsChanged);
            _eventBus.Unsubscribe<AddGobeletsEvent>(OnGobeletsChanged);
        }

        public void Initialize()
        {
            _coins = 0;
            _gobelets = 0;

            _eventBus.Subscribe<AddCoinsEvent>(OnCoinsChanged);
            _eventBus.Subscribe<AddGobeletsEvent>(OnGobeletsChanged);
        }

        public void SaveAllServices(GameSaveData gameSaveData)
        {
            gameSaveData.WalletData.Coins = _coins;
            gameSaveData.WalletData.Gobelets = _gobelets;
        }

        public void LoadAllServices(GameSaveData gameSaveData)
        {
            if(gameSaveData.WalletData == null)
            {
                gameSaveData.WalletData = new WalletData();
            }

            _coins = gameSaveData.WalletData.Coins;
            _gobelets = gameSaveData.WalletData.Gobelets;
        }

        private void OnCoinsChanged(AddCoinsEvent args)
        {
            _coins += args.CoinCount;
            _eventBus.Publish(new SaveGameEvent { });
            _eventBus.Publish(new UpdateUIEvent { });


        }

        private void OnGobeletsChanged(AddGobeletsEvent args)
        {
            _gobelets += args.GobeletCount;
            _eventBus.Publish(new SaveGameEvent { });
            _eventBus.Publish(new UpdateUIEvent { });
        }
    }
}
