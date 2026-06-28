using Assets._Scripts.GameControllers.Wallets;
using System.Collections.Generic;

namespace Assets._Scripts.GameControllers.Achievments
{
    public interface IRevard
    {
        public void GetReward();
    }

    public class AchievmentsReward // данный класс нужен для совестной награды, если несколько ревардов выдаётся.
    {
        private List<IRevard> _rewardList = new List<IRevard>();

        public void AddRevard(IRevard revard)
        {
            _rewardList.Add(revard);
        }

        public void GetRewards()
        {
            foreach (IRevard reward in _rewardList)
            {
                reward.GetReward();
            }
        }
    }

    public class CoinReward : IRevard
    {
        private int _coinCount;
        private WalletController _walletController;

        public CoinReward(int coinCount, WalletController walletController)
        {
            _coinCount = coinCount;
            _walletController = walletController;
        }

        public void GetReward()
        {
            _walletController.AddConis(_coinCount);
        }
    }

    public class GobeletReward : IRevard
    {
        private int _gobeletCount;
        private WalletController _walletController;

        public GobeletReward(int gobeletCount, WalletController walletController)
        {
            _gobeletCount = gobeletCount;
            _walletController = walletController;
        }

        public void GetReward()
        {
            _walletController.AddGobelets(_gobeletCount);
        }
    }
}
