using Assets._Scripts.ObjectsScripts.Points;
using Assets.Scripts.Player;
using Assets.Scripts.StateMachines;
using ECM2;
using System;

namespace Assets._Scripts.Bots.BotStateMachine.States
{
    public class MoveAI : IState, IDisposable
    {
        private NavMeshCharacter _agent;
        private AnimatorController _animatorController;
        private BotAISM _botAISM;
        private RoadPointAIController _roadPointAIController;

        public MoveAI(BotAISM botAISM, NavMeshCharacter agent, 
            AnimatorController animatorController, 
            RoadPointAIController roadPointAIController)
        {
            _agent = agent;
            _animatorController = animatorController;
            _botAISM = botAISM;
            _roadPointAIController = roadPointAIController;
        }

        public void Dispose()
        {
            _roadPointAIController.Dispose();
            _roadPointAIController = null;
        }

        public void Enter()
        {
            _animatorController.SetMove(true);
            _animatorController.SetGround(true);
        }

        public void Exit()
        {
            
        }

        public void FixedUpdate()
        {
            if (_roadPointAIController != null) // надо подумать как сделать по другому...
                _agent.MoveToDestination(_roadPointAIController.GetCurrentPoint());
        }

        public void CheckChangeState()
        {
            if (_agent.agent.isOnOffMeshLink == true)
            {
                _botAISM.ChangeState<JumpAI>();
            }
        }
    }
}
