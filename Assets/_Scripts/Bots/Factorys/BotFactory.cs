using Assets._Scripts.Bots.BotStateMachine;
using Assets._Scripts.Bots.Factorys;
using Assets._Scripts.ObjectsScripts.Points;
using Assets.Scripts.Points;
using ECM2;
using System;

namespace Assets._Scripts.Bots
{
    public class BotFactory
    {
        private BotStateMachineFactory _botStateMachineFactory;
        private Func<NavMeshCharacter> _characterFactory;
        private GamePoints _gamePoints;

        public BotFactory(BotStateMachineFactory botStateMachineFactory, 
            Func<NavMeshCharacter> characterFactory, GamePoints gamePoints)
        {
            _botStateMachineFactory = botStateMachineFactory;
            _characterFactory = characterFactory;
            _gamePoints = gamePoints;
        }

        public Bot CreateBot()
        {
            NavMeshCharacter agent = _characterFactory();
            RoadPointAIController roadPointAIController = new RoadPointAIController();
            roadPointAIController.SetRoadPointAIController(_gamePoints);
            BotAISM botAISM = _botStateMachineFactory.Create(agent, roadPointAIController);

            return new Bot(agent, botAISM, roadPointAIController);
        }
    }
}
