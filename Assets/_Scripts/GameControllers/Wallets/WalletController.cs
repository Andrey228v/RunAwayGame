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

        private MenuTabs _menuView;
        private GamePanelController _gamePanelView;
        private UnitInfoUI _unitInfoUIView;

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

        public void AddMenuView(MenuTabs menuView)
        {
            _menuView = menuView;
            _menuView.OnDestroyView += RemoveMenuView;
            _model.OnCoinsChanged += _menuView.SetCoinsCountText;
            _model.OnGobeletsChanged += _menuView.SetGobeletsCountText;
        }

        public void RemoveMenuView()
        {
            _menuView.OnDestroyView -= RemoveMenuView;
            _model.OnCoinsChanged -= _menuView.SetCoinsCountText;
            _model.OnGobeletsChanged -= _menuView.SetGobeletsCountText;
        }

        public void AddGamePanelView(GamePanelController gamePanelView)
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

        public void AddUnitInfoUIView(UnitInfoUI unitInfoUI)
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

        public void UpdateAllView()
        {
            _menuView.SetCoinsCountText(_model.Data.Coins, 0);
            _menuView.SetGobeletsCountText(_model.Data.Gobelets, 0);
        }

        private void OnCoinsChanged(AddCoinsEvent args)
        {
            //_gameLogger.Log("OnCoinsChanged", "Event");

            //_eventBus.Publish(new SaveGameEvent { });
            //_eventBus.Publish(new UpdateUIEvent { });
        }

        private void OnGobeletsChanged(AddGobeletsEvent args)
        {
            //_gameLogger.Log("OnGobeletsChanged", "Event");

            //_eventBus.Publish(new SaveGameEvent { });
            //_eventBus.Publish(new UpdateUIEvent { });
        }

        public void AddConis(int count)
        {
            _model.AddCoins(count);
        }
    }
}
