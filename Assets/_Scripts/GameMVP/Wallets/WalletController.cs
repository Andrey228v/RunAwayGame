using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.SaveLoad.Data.Interfaces.Game;
using Assets._Scripts.UI;
using Assets._Scripts.UI._1MenuWindow;
using Assets._Scripts.Utilites.Loger;
using Assets.Scripts.SaveLoad.Data;
using Assets.Scripts.UI;

namespace Assets._Scripts.GameControllers.Wallets
{
    public class WalletController : IInitGame, ISaveGame, ILoadGame, IFinishGame, IResetGame
    {
        private WalletModel _model;
        private MenuTabsView _menuView;
        private GamePanelView _gamePanelView;
        private UnitInfoUIView _unitInfoUIView;
        private IGameLogger _gameLogger;

        public WalletController(IGameLogger gameLogger)
        {
            _gameLogger = gameLogger;
        }

        public void Initialization()
        {
            var walletData = new WalletData();

            _model = new WalletModel(walletData, _gameLogger);

            _model.OnCoinsChanged += CoinUpdateView;
            _model.OnGobeletsChanged += GobeletsUpdateView;
        }

        public void Dispose()
        {
            _model.OnCoinsChanged -= CoinUpdateView;
            _model.OnGobeletsChanged -= GobeletsUpdateView;
        }

        public void Save(GameSaveData gameSaveData)
        {
            //Переделать...
            gameSaveData.WalletData.Coins = _model.Data.Coins;
            gameSaveData.WalletData.Gobelets = _model.Data.Gobelets;
        }

        public void Load(GameSaveData gameSaveData)
        {
            _model.LoadData(gameSaveData.WalletData);
        }

        public void Finish(GameSaveData gameSaveData)
        {

        }

        public void Reset(GameSaveData gameSaveData)
        {
            _model.Reset();
        }

        public void CoinUpdateView(int current, int value) 
        {
            if (_menuView != null)
            {
                _menuView.SetCoinsCountText(current, value);
            }
            else if (_gamePanelView != null)
            {
                _gamePanelView.SetCoinsCountText(current, value);
            }
        }

        public void GobeletsUpdateView(int current, int value)
        {
            if (_menuView != null)
            {
                _menuView.SetGobeletsCountText(current, value);
            }
            else if(_unitInfoUIView != null)
            {
                _unitInfoUIView.SetGobeletsCountText(current, value);
            }
        }

        public void GobeletsUpdateVied(int current, int value)
        {
            if (_unitInfoUIView != null)
            {
                _unitInfoUIView.SetGobeletsCountText(current, value);
            }
        }

        public void AddMenuView(MenuTabsView menuView)
        {
            _menuView = menuView;
            _menuView.OnDestroyView += RemoveMenuView;

            _menuView.SetCoinsCountText(_model.Data.Coins, 0);
            _menuView.SetGobeletsCountText(_model.Data.Gobelets, 0);
        }

        public void RemoveMenuView()
        {
            _menuView.OnDestroyView -= RemoveMenuView;
        }

        public void AddGamePanelView(GamePanelView gamePanelView)
        {
            _gamePanelView = gamePanelView;
            _gamePanelView.OnDestroyView += RemoveGamePanelView;

            _gamePanelView.SetCoinsCountText(_model.Data.Coins, 0);
        }

        public void RemoveGamePanelView()
        {
            _gamePanelView.OnDestroyView -= RemoveGamePanelView;
        }

        public void AddUnitInfoUIView(UnitInfoUIView unitInfoUI)
        {
            _unitInfoUIView = unitInfoUI;
            _unitInfoUIView.OnDestroyView += RemoveUnitInfoUIView;

            _unitInfoUIView.SetGobeletsCountText(_model.Data.Gobelets, 0);
        }

        public void RemoveUnitInfoUIView()
        {
            _unitInfoUIView.OnDestroyView -= RemoveUnitInfoUIView;
        }

        public void AddConis(int count)
        {
            _model.AddCoins(count);
            //_eventBus.Publish(new SaveGameEvent { });
            //_eventBus.Publish(new CollectGoldEvent { Progress = count });
        } // achievments

        public void AddGobelets(int count)
        {
            _model.AddGobelets(count);
            //_eventBus.Publish(new SaveGameEvent { });
        } // achievments
    }
}
