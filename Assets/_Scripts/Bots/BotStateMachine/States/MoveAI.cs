using Assets._Scripts.ObjectsScripts.Points;
using Assets.Scripts.Player;
using Assets.Scripts.Points;
using Assets.Scripts.StateMachines;
using ECM2;
using UnityEngine;
using UnityEngine.AI;

namespace Assets._Scripts.Bots.BotStateMachine.States
{
    public class MoveAI : IState
    {
        private NavMeshCharacter _agent;
        private AnimatorController _animatorController;
        //private GamePoints _gamePoints;
        private BotAISM _botAISM;
        private RoadPointAIController _roadPointAIController;

        public MoveAI(BotAISM botAISM, NavMeshCharacter agent, 
            AnimatorController animatorController, RoadPointAIController roadPointAIController)
        {
            _agent = agent;
            _animatorController = animatorController;
            //_gamePoints = gamePoints;
            _botAISM = botAISM;
            _roadPointAIController = roadPointAIController;
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
            //_agent.MoveToDestination(_gamePoints.FinishPoint.transform.position);

            
            _agent.MoveToDestination(_roadPointAIController.GetNextPoint());

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
