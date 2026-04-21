using Assets._Scripts.ObjectsScripts.Points;
using Assets.Scripts.Player;
using Assets.Scripts.StateMachines;
using Cysharp.Threading.Tasks;
using ECM2;
using UnityEngine;
using UnityEngine.AI;


namespace Assets._Scripts.Bots.BotStateMachine.States
{
    public class JumpAI : IState
    {
        private NavMeshCharacter _agent;
        private AnimatorController _animatorController;
        private BotAISM _botAISM;
        private RoadPointAIController _roadPointAIController;

        public JumpAI(BotAISM botAISM, NavMeshCharacter agent, 
            AnimatorController animatorController, RoadPointAIController roadPointAIController)
        {
            _agent = agent;
            _animatorController = animatorController;
            _botAISM = botAISM;
            _roadPointAIController = roadPointAIController;
        }

        public async void Enter()
        {
            var distance = CalculateDistanceJump();

            if(distance > 14)
            {
                _agent.character.Jump();
                await UniTask.Delay(500);
                _agent.character.StopJumping();
                _agent.character.Jump();
            }
            else
            {
                _agent.character.Jump();
            }
        }

        public void Exit()
        {
            _agent.character.StopJumping();
        }

        public void FixedUpdate()
        {
            if (_roadPointAIController != null) // надо подумать как сделать по другому...
                _agent.MoveToDestination(_roadPointAIController.GetCurrentPoint());
        }

        public void CheckChangeState()
        {
            if (_agent.character.IsOnGround())
            {
                _botAISM.ChangeState<MoveAI>();
            }
        }
        
        private float CalculateDistanceJump()
        {
            var data = TryGetCurrentNavMeshLink();
            var distance = Vector3.Distance(data.startPos, data.endPos);

            return distance;
        }

        private OffMeshLinkData TryGetCurrentNavMeshLink()
        {
            OffMeshLinkData linkData = _agent.agent.currentOffMeshLinkData;

            return linkData;
        }
    }
}
