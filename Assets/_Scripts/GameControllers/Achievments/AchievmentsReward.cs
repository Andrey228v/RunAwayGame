using Assets._Scripts.EventBusGame;
using System;
using System.Collections.Generic;

namespace Assets._Scripts.GameControllers.Achievments
{
    public interface IRevard
    {
        public void GetReward();
    }

    public class AchievmentsReward
    {
        private EventBus _eventBus;
        private List<IRevard> _rewardList = new List<IRevard>();

        public AchievmentsReward(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

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
        private EventBus _eventBus;
        private int _coinCount;

        public CoinReward(EventBus eventBus, int coinCount)
        {
            _eventBus= eventBus;
            _coinCount = coinCount;
        }

        public void GetReward()
        {
            _eventBus.Publish(new AddCoinsEvent { CoinCount = _coinCount });
        }
    }

    public class GobeletReward : IRevard
    {
        private EventBus _eventBus;
        private int _gobeletCount;

        public GobeletReward(EventBus eventBus, int gobeletCount)
        {
            _eventBus = eventBus;
            _gobeletCount = gobeletCount;
        }

        public void GetReward()
        {
            _eventBus.Publish(new AddGobeletsEvent { GobeletCount = _gobeletCount });
        }
    }
}
