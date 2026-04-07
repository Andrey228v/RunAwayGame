using Assets._Scripts.Bots.BotStateMachine;
using Assets.Scripts.Player;
using Assets.Scripts.Points;
using ECM2;

namespace Assets._Scripts.Bots.Factorys
{
    public class BotStateMachineFactory
    {
        public BotAISM Create(NavMeshCharacter agent)
        {

            AnimatorController animatorController = new AnimatorController(agent.character.animator);

            BotAISM botAISM = new BotAISM(agent, animatorController);

            return botAISM;
        }
    }
}
