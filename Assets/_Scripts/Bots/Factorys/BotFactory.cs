using Assets._Scripts.Bots.BotStateMachine;
using Assets._Scripts.Bots.Factorys;
using Assets._Scripts.GameControllers;
using Assets.Scripts.Player;
using Assets.Scripts.Points;
using ECM2;
using System;
using UnityEngine.AI;

namespace Assets._Scripts.Bots
{
    public class BotFactory
    {
        private PlayerMovement _playerMovement;
        private PlayerRotator _playerRotator;
        private PlayerGroundChecker _playerGroundChecker;
        private PlayerJumper _playerJumper;
        private AnimatorController _animatorController;
        private FallController _fallController;
        private BotStateMachineFactory _botStateMachineFactory;

        public BotFactory(BotStateMachineFactory botStateMachineFactory)
        {
            _botStateMachineFactory = botStateMachineFactory;
        }

        public Bot CreateBot(NavMeshCharacter agent, GamePoints gamePoints)
        {
            BotAISM botAISM = _botStateMachineFactory.Create(agent, gamePoints);
            BotAIReader botAIReader = new BotAIReader();

            return new Bot(agent, botAISM, botAIReader);
        }
    }
}
