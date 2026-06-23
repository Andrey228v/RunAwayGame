using Assets._Scripts.ObjectsScripts.Bots.BotStateMachine;
using Assets._Scripts.ObjectsScripts.Player;
using Assets._Scripts.ObjectsScripts.Points;
using Assets.Scripts.Points;
using ECM2;

namespace Assets._Scripts.ObjectsScripts.Bots.Factorys
{
    public class BotStateMachineFactory
    {
        public BotAISM Create(NavMeshCharacter agent, RoadPointAIController roadPointAIController)
        {

            AnimatorController animatorController = new AnimatorController(agent.character.animator);

            BotAISM botAISM = new BotAISM(agent, animatorController, roadPointAIController);

            return botAISM;
        }
    }
}
