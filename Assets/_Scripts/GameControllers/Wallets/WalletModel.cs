using Assets._Scripts.Loger;
using Assets._Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets._Scripts.GameControllers.Wallets
{
    public class WalletModel
    {
        //private int _coins;
        //private int _gobelets;

        private WalletData _data;
        private readonly int _maxCoins = 999999;
        private readonly int _minCoins = 0;
        private IGameLogger _gameLogger;

        public WalletData Data => _data;

        // События для Presenter'а
        public event Action<int, int> OnCoinsChanged; // newCoins, delta
        public event Action<int, int> OnGemsChanged;
        public event Action<string> OnTransactionCompleted;
        public event Action<string, int> OnInsufficientFunds;

        public WalletModel(WalletData data, IGameLogger gameLogger)
        {
            _data = data;
            _gameLogger = gameLogger;
        }

        public void AddCoins(int amount)
        {
            if (amount <= 0)
            {
                _gameLogger.LogWarning("AchievmentsController Initialize", "Success");
                return;
            }

            int oldCoins = _data.Coins;
            _data.Coins = Mathf.Min(_maxCoins, _data.Coins + amount);
            int actualAdded = _data.Coins - oldCoins;

            if (actualAdded > 0)
            {
                _data.LastTransactionAmount = actualAdded;
                _data.LastTransactionTime = DateTime.Now.ToString("HH:mm:ss");

                OnCoinsChanged?.Invoke(_data.Coins, actualAdded);
                OnTransactionCompleted?.Invoke($"Added {actualAdded} coins");
            }
        }

        public bool SpendCoins(int amount)
        {
            if (amount <= 0)
            {
                OnInsufficientFunds?.Invoke("Invalid amount", amount);
                return false;
            }

            if (_data.Coins >= amount)
            {
                int oldCoins = _data.Coins;
                _data.Coins -= amount;

                _data.LastTransactionAmount = -amount;
                _data.LastTransactionTime = DateTime.Now.ToString("HH:mm:ss");

                OnCoinsChanged?.Invoke(_data.Coins, -amount);
                OnTransactionCompleted?.Invoke($"Spent {amount} coins");
                return true;
            }
            else
            {
                OnInsufficientFunds?.Invoke("Not enough coins", _data.Coins - amount);
                return false;
            }
        }

        public void AddGems(int amount)
        {
            if (amount <= 0) return;

            int oldGems = _data.Gobelets;
            _data.Gobelets += amount;

            OnGemsChanged?.Invoke(_data.Gobelets, amount);
            OnTransactionCompleted?.Invoke($"Added {amount} gems");
        }

        public bool SpendGems(int amount)
        {
            if (amount <= 0) return false;

            if (_data.Gobelets >= amount)
            {
                _data.Gobelets -= amount;
                OnGemsChanged?.Invoke(_data.Gobelets, -amount);
                OnTransactionCompleted?.Invoke($"Spent {amount} gems");
                return true;
            }
            else
            {
                OnInsufficientFunds?.Invoke("Not enough gems", _data.Gobelets - amount);
                return false;
            }
        }

        public void LoadData(WalletData data)
        {
            //if (data != null)
            //{
            //    _data = data.Clone();
            //    OnCoinsChanged?.Invoke(_data.Coins, 0);
            //    OnGemsChanged?.Invoke(_data.Gems, 0);
            //}
        }
    }
}
