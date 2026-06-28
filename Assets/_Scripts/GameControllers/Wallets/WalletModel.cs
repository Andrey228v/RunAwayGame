using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.Utilites.Loger;
using Assets.Scripts.SaveLoad.Data;
using System;
using UnityEngine;

namespace Assets._Scripts.GameControllers.Wallets
{
    public class WalletModel
    {
        private WalletData _data;
        private readonly int _maxCoins = 999999;
        private readonly int _minCoins = 0;
        private IGameLogger _gameLogger;

        public WalletData Data => _data;

        // События для Presenter'а
        public event Action<int, int> OnCoinsChanged; // newCoins, delta
        public event Action<int, int> OnGobeletsChanged;
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

        public void AddGobelets(int amount)
        {
            if (amount <= 0) return;

            int oldGems = _data.Gobelets;
            _data.Gobelets += amount;

            OnGobeletsChanged?.Invoke(_data.Gobelets, amount);
            OnTransactionCompleted?.Invoke($"Added {amount} gems");
        }

        public bool SpendGobelets(int amount)
        {
            if (amount <= 0) return false;

            if (_data.Gobelets >= amount)
            {
                _data.Gobelets -= amount;
                OnGobeletsChanged?.Invoke(_data.Gobelets, -amount);
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
            _data = data; // под вопросом...
            AddCoins(data.Coins);
            AddGobelets(data.Gobelets);
        }

        public void Reset()
        {
            _data.ResetData(); // под вопросом...
            AddCoins(_data.Coins);
            AddGobelets(_data.Gobelets);
        }
    }
}
