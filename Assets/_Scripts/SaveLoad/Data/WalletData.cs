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

        public WalletData(int coins = 0, int gobelets = 0)
        {
            Coins = coins;
            Gobelets = gobelets;
        }

        public void ResetData()
        {
            Coins = 0;
            Gobelets = 0;
        }
    }
}
