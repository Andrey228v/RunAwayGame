using Assets._Scripts.GameMVP.Language;
using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.UI;
using Assets._Scripts.UI._1MenuWindow;
using Assets.Scripts.SaveLoad.Data;
using Assets.Scripts.UI;
using System;

namespace Assets._Scripts.GameControllers.Settings
{



    public class SettingsController
    {
        private SettingsModel _model;
        private MenuTabsView _menuView;
        //private GameInterfacePanelView _gamePanelView;

        //public void Initialize(SettingsModel model)
        //{
        //    _model = model;
        //}

        public void SaveAllServices(GameSaveData gameSaveData)
        {
            //gameSaveData.WalletData.Coins = _model.Data.Coins;
            //gameSaveData.WalletData.Gobelets = _model.Data.Gobelets;
        }

        public void LoadAllServices(GameSaveData gameSaveData)
        {
            //if (gameSaveData.WalletData == null)
            //{
            //    gameSaveData.WalletData = new WalletData();
            //}

            //_model.LoadData(gameSaveData.WalletData);
        }

        //public void AddMenuView(MenuTabsView menuView)
        //{
        //    //_menuView = menuView;
        //    //_menuView.OnDestroyView += RemoveMenuView;
        //    //_model.OnCoinsChanged += _menuView.SetCoinsCountText;
        //    //_model.OnGobeletsChanged += _menuView.SetGobeletsCountText;
        //}

        public void RemoveMenuView()
        {
            //_menuView.OnDestroyView -= RemoveMenuView;
            //_model.OnCoinsChanged -= _menuView.SetCoinsCountText;
            //_model.OnGobeletsChanged -= _menuView.SetGobeletsCountText;
        }

        public void AddGamePanelView(GamePanelView gamePanelView)
        {
            //_gamePanelView = gamePanelView;
            //_gamePanelView.OnDestroyView += RemoveGamePanelView;
            //_model.OnCoinsChanged += _gamePanelView.SetCoinsCountText;

            //_gamePanelView.SetCoinsCountText(_model.Data.Coins, 0);
            //_unitInfoUIView.SetGobeletsCountText(_model.Data.Gobelets, 0);
        }

        public void RemoveGamePanelView()
        {
            //_gamePanelView.OnDestroyView -= RemoveGamePanelView;
            //_model.OnCoinsChanged -= _gamePanelView.SetCoinsCountText;
        }

        public void UpdateView()
        {
            //_menuView.SetCoinsCountText(_model.Data.Coins, 0);
            //_menuView.SetGobeletsCountText(_model.Data.Gobelets, 0);
        }

        public void Reset(GameSaveData gameSaveData)
        {

        }
    }
}
