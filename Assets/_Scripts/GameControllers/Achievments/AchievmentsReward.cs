using Assets._Scripts.EventBusGame;
using System;
using System.Collections.Generic;

namespace Assets._Scripts.GameControllers.Achievments
{

    public interface IRevard
    {
        public Action GetReward();
    }

    public class AchievmentsReward
    {
        private EventBus _eventBus;
        private List<IRevard> _rewardList = new List<IRevard>();

        public void AddRevard(IRevard revard)
        {
            _rewardList.Add(revard);
        }

        public void GetReward()
        {
            foreach (var reward in _rewardList)
            {
                _eventBus.Publish(reward.GetReward());
            }
        }
    }

    public class CoinReward : IRevard
    {
        private int _coinCount;

        public CoinReward(int coinCount)
        {
            _coinCount = coinCount;
        }

        public Action GetReward()
        {

            return () => new AddCoinsEvent { CoinCount = _coinCount };
            //_eventBus.Publish(new AddCoinsEvent { CoinCount = coinCount });
        }
    }

    public class GobeletReward : IRevard
    {
        private int _gobeletCount;

        public GobeletReward(int gobeletCount)
        {
            _gobeletCount = gobeletCount;
        }

        public Action GetReward()
        {
            return () => new AddGobeletsEvent { GobeletCount = _gobeletCount };
            //_eventBus.Publish(new AddGobeletsEvent { GobeletCount = gobeletCount });
        }
    }
}
