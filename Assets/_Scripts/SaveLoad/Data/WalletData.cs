using System;
using System.Collections.Generic;
using System.Text;

namespace Assets._Scripts.SaveLoad.Data
{
    [Serializable]
    public class WalletData
    {
        public int Coins;
        public int Gobelets;
        public int LastTransactionAmount;
        public string LastTransactionTime;

        public WalletData(int coins = 0, int gobelets = 0)
        {
            Coins = coins;
            Gobelets = gobelets;
            LastTransactionAmount = 0;
            LastTransactionTime = System.DateTime.Now.ToString("HH:mm:ss");
        }

        public void ResetData()
        {
            Coins = 0;
            Gobelets = 0;
        }
    }
}
