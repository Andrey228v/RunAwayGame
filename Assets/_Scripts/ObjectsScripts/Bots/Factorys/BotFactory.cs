using Assets._Scripts.ObjectsScripts.Bots.BotStateMachine;
using Assets._Scripts.ObjectsScripts.Points;
using Assets.Scripts.Points;
using ECM2;
using System;
using UnityEngine;

namespace Assets._Scripts.ObjectsScripts.Bots.Factorys
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

            //Определить стартовую позицию и передать..
            Vector3 startPosition = roadPointAIController.GetRandomPosition();
            roadPointAIController.AddPointCounter();
            Vector3 destination = roadPointAIController.GetCurrentPoint();

            return new Bot(agent, botAISM, roadPointAIController, startPosition, destination);
        }
    }
}
