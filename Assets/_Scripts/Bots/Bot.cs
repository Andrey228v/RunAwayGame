using Assets._Scripts.Bots.BotStateMachine;
using ECM2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Scripts.Bots
{
    public class Bot
    {
        private NavMeshCharacter _agent;
        private BotAISM _botAISM;
        private BotAIReader _botAIReader;

        public Bot(NavMeshCharacter agent, BotAISM botAISM, BotAIReader botAIReader) 
        {
            _agent = agent;
            _botAISM = botAISM;
            _botAIReader = botAIReader;
        }

        public void FixedUpdateSM()
        {
            _botAISM.FixedTick();
        }

    }
}
