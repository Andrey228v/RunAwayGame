using Assets._Scripts.Bots.BotStateMachine;
using Assets._Scripts.ObjectsScripts.Points;
using ECM2;
using System;

namespace Assets._Scripts.Bots
{
    public class Bot : IDisposable
    {
        private NavMeshCharacter _agent;
        private BotAISM _botAISM;
        private RoadPointAIController _roadPointAIController;

        public Bot(NavMeshCharacter agent, BotAISM botAISM, RoadPointAIController roadPointAIController) 
        {
            _agent = agent;
            _botAISM = botAISM;
            _roadPointAIController = roadPointAIController;
        }

        public void Sub()
        {
            _agent.DestinationReached += OnDestinationReached;
        }

        public void Dispose()
        {
            if (_agent != null)
            {
                _agent.DestinationReached -= OnDestinationReached;
            }
        }

        public void FixedUpdateSM()
        {
            _botAISM.FixedTick();
        }

        private void OnDestinationReached()
        {
            _roadPointAIController.AddPointCounter();
        }

        //public void SetRoadPointAIController(RoadPointAIController roadPointAIController)
        //{
        //    _roadPointAIController = roadPointAIController;
        //}
    }
}
