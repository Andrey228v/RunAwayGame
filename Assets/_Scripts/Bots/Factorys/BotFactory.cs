using Assets._Scripts.Bots.BotStateMachine;
using Assets._Scripts.Bots.Factorys;
using Assets.Scripts.Points;
using ECM2;
using System;
using Unity.VisualScripting;

namespace Assets._Scripts.Bots
{
    public class BotFactory
    {
        private BotStateMachineFactory _botStateMachineFactory;
        private Func<Character> _characterFactory;

        public BotFactory(BotStateMachineFactory botStateMachineFactory, Func<Character> characterFactory)
        {
            _botStateMachineFactory = botStateMachineFactory;
            _characterFactory = characterFactory;
        }


        public Bot CreateBot()
        {
            Character character = _characterFactory();
            character.AddComponent<NavMeshCharacter>(); // Тут подумать так ли делать ...
            character.AddComponent<BotMB>();
            NavMeshCharacter agent = character.GetComponent<NavMeshCharacter>();

            BotAISM botAISM = _botStateMachineFactory.Create(agent);
            BotAIReader botAIReader = new BotAIReader();

            BotMB botMB = agent.gameObject.GetComponent<BotMB>();

            return new Bot(agent, botAISM, botAIReader);
        }
    }
}
