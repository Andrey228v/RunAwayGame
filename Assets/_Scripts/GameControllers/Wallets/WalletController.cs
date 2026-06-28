using Assets._Scripts.EventBusGame;
using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.UI;
using Assets._Scripts.UI._1MenuWindow;
using Assets._Scripts.Utilites.Loger;
using Assets.Scripts.SaveLoad.Data;
using Assets.Scripts.UI;

namespace Assets._Scripts.GameControllers.Wallets
{
    public class WalletController
    {
        private WalletModel _model;
        
        private EventBus _eventBus;
        private IGameLogger _gameLogger;

        private MenuTabsView _menuView;
        private GamePanelView _gamePanelView;
        private UnitInfoUIView _unitInfoUIView;

        public WalletController(EventBus eventBus, IGameLogger gameLogger)
        {
            _eventBus = eventBus;
            _gameLogger = gameLogger;
        }

        public void Initialize(WalletModel model)
        {
            _model = model;
            _model.OnCoinsChanged += CoinUpdateView;
        }

        public void Dispose()
        {
            _model.OnCoinsChanged -= CoinUpdateView;
        }

        public void SaveAllServices(GameSaveData gameSaveData)
        {
            //Переделать...
            gameSaveData.WalletData.Coins = _model.Data.Coins;
            gameSaveData.WalletData.Gobelets = _model.Data.Gobelets;
        }

        public void LoadAllServices(GameSaveData gameSaveData)
        {
            if (gameSaveData.WalletData == null)
            {
                gameSaveData.WalletData = new WalletData();
            }

            _model.LoadData(gameSaveData.WalletData);
        }

        public void CoinUpdateView(int current, int value) 
        {
            if (_menuView != null)
            {
                _menuView.SetCoinsCountText(current, value);
            }

            if (_gamePanelView != null)
            {
                _gamePanelView.SetCoinsCountText(_model.Data.Coins, 0);
            }
        }

        public void GobeletsUpdateVied(int current, int value)
        {
            if (_unitInfoUIView != null)
            {
                _unitInfoUIView.SetGobeletsCountText(_model.Data.Gobelets, 0);
            }
        }

        public void AddMenuView(MenuTabsView menuView)
        {
            _menuView = menuView;
            _menuView.OnDestroyView += RemoveMenuView;

            //Тут надо придумать как обновлять View....
        }

        public void RemoveMenuView()
        {
            _menuView.OnDestroyView -= RemoveMenuView;
        }

        public void AddGamePanelView(GamePanelView gamePanelView)
        {
            _gamePanelView = gamePanelView;
            _gamePanelView.OnDestroyView += RemoveGamePanelView;
        }

        public void RemoveGamePanelView()
        {
            _gamePanelView.OnDestroyView -= RemoveGamePanelView;
        }

        public void AddUnitInfoUIView(UnitInfoUIView unitInfoUI)
        {
            _unitInfoUIView = unitInfoUI;
            _unitInfoUIView.OnDestroyView += RemoveUnitInfoUIView;
        }

        public void RemoveUnitInfoUIView()
        {
            _unitInfoUIView.OnDestroyView -= RemoveUnitInfoUIView;
        }

        public void AddConis(int count)
        {
            _model.AddCoins(count);
            _eventBus.Publish(new SaveGameEvent { });
            _eventBus.Publish(new CollectGoldEvent { Progress = count });
        }

        public void AddGobelets(int count)
        {
            _model.AddGobelets(count);
            _eventBus.Publish(new SaveGameEvent { });
        }

        public void Reset(GameSaveData gameSaveData)
        {
            _model.Reset();
        }
    }
}
