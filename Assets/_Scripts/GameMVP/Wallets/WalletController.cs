using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.SaveLoad.Data.Interfaces.Game;
using Assets._Scripts.UI._1MenuWindow;
using Assets._Scripts.Utilites.Loger;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;

namespace Assets._Scripts.GameControllers.Wallets
{
    public class WalletController : IInitGame, ISaveGame, ILoadGame, IFinishGame, IResetGame
    {
        private WalletModel _model;
        private readonly List<IWalletView> _views = new(); // сделать тут словарь... вроде как удобнее... или нет??

        private IGameLogger _gameLogger;

        public event Action<int> OnAddCoin;
        public event Action<int> OnAddGobelet;

        public WalletController(IGameLogger gameLogger)
        {
            _gameLogger = gameLogger;
        }

        public void Initialization(GameSaveData gameSaveData)
        {
            var data = gameSaveData.WalletData;

            if(data == null)
            {
                data = new WalletData();
                gameSaveData.WalletData = data;
            }

            _model = new WalletModel(data, _gameLogger);

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

        public void AddView(IWalletView view)
        {
            if (view == null || _views.Contains(view)) return;

            _views.Add(view);
        }

        public void RemoveView(IWalletView view)
        {
            _views.Remove(view);
        }


        //from model event =>
        public void CoinUpdateView(int current, int value) 
        {
            foreach(IWalletView view in _views)
            {
                view.SetCoinsCountText(current, value);
            }
        }

        //from model event =>
        public void GobeletsUpdateView(int current, int value)
        {
            foreach (IWalletView view in _views)
            {
                view.SetGobeletsCountText(current, value);
            }
        }

        //from Entery =>
        public void AddConis(int count)
        {
            _model.AddCoins(count);
            OnAddCoin?.Invoke(count);
        }

        //from Entery =>
        public void AddGobelets(int count)
        {
            _model.AddGobelets(count);
            OnAddGobelet?.Invoke(count);
        }
    }
}
