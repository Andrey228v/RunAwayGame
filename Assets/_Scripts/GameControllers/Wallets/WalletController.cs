using Assets._Scripts.EventBusGame;
using Assets._Scripts.Loger;
using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.UI;
using Assets._Scripts.UI._1MenuWindow;
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
            _eventBus.Subscribe<AddCoinsEvent>(OnCoinsChanged);
            _eventBus.Subscribe<AddGobeletsEvent>(OnGobeletsChanged);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<AddCoinsEvent>(OnCoinsChanged);
            _eventBus.Unsubscribe<AddGobeletsEvent>(OnGobeletsChanged);
        }

        public void SaveAllServices(GameSaveData gameSaveData)
        {
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

        public void AddMenuView(MenuTabsView menuView)
        {
            _menuView = menuView;
            _menuView.OnDestroyView += RemoveMenuView;
            _model.OnCoinsChanged += _menuView.SetCoinsCountText;
            _model.OnGobeletsChanged += _menuView.SetGobeletsCountText;

            //UpdateView();
        }

        public void RemoveMenuView()
        {
            _menuView.OnDestroyView -= RemoveMenuView;
            _model.OnCoinsChanged -= _menuView.SetCoinsCountText;
            _model.OnGobeletsChanged -= _menuView.SetGobeletsCountText;
        }

        public void AddGamePanelView(GamePanelView gamePanelView)
        {
            _gamePanelView = gamePanelView;
            _gamePanelView.OnDestroyView += RemoveGamePanelView;
            _model.OnCoinsChanged += _gamePanelView.SetCoinsCountText;
        }

        public void RemoveGamePanelView()
        {
            _gamePanelView.OnDestroyView -= RemoveGamePanelView;
            _model.OnCoinsChanged -= _gamePanelView.SetCoinsCountText;

        }

        public void AddUnitInfoUIView(UnitInfoUIView unitInfoUI)
        {
            _unitInfoUIView = unitInfoUI;
            _unitInfoUIView.OnDestroyView += RemoveUnitInfoUIView;
            _model.OnGobeletsChanged += _unitInfoUIView.SetGobeletsCountText;

        }

        public void RemoveUnitInfoUIView()
        {
            _unitInfoUIView.OnDestroyView -= RemoveUnitInfoUIView;
            _model.OnGobeletsChanged -= _unitInfoUIView.SetGobeletsCountText;
        }

        public void UpdateView()
        {
            _menuView.SetCoinsCountText(_model.Data.Coins, 0);
            _menuView.SetGobeletsCountText(_model.Data.Gobelets, 0);
        }

        private void OnCoinsChanged(AddCoinsEvent args)
        {
            UpdateView();
        }

        private void OnGobeletsChanged(AddGobeletsEvent args)
        {
            UpdateView();
        }

        public void AddConis(int count)
        {
            _model.AddCoins(count);
        }

        public void Reset(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            _model.Reset();

            if (_menuView != null)
            {
                int coins = gameSaveData.WalletData.Coins;
                int gobelets = gameSaveData.WalletData.Gobelets;

                _menuView.SetCoinsCountText(coins, 0);
                _menuView.SetGobeletsCountText(gobelets, 0);
            }

            if(_gamePanelView != null)
            {
                _gamePanelView.UpdateView();
            }

            if(_unitInfoUIView != null)
            {
                _unitInfoUIView.UpdateView();
            }


        }
    }
}
