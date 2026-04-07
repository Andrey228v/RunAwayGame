using Assets._Scripts.ObjectsScripts.Points;
using Assets.Scripts.Player;
using Assets.Scripts.Points;
using Assets.Scripts.StateMachines;
using Cysharp.Threading.Tasks;
using ECM2;
using System.Threading;


namespace Assets._Scripts.Bots.BotStateMachine.States
{
    public class JumpAI : IState
    {

        private NavMeshCharacter _agent;
        private AnimatorController _animatorController;
        //private GamePoints _gamePoints;
        private BotAISM _botAISM;
        private CancellationTokenSource _jumpCts;
        private RoadPointAIController _roadPointAIController;

        public JumpAI(BotAISM botAISM, NavMeshCharacter agent, 
            AnimatorController animatorController, RoadPointAIController roadPointAIController)
        {
            _agent = agent;
            _animatorController = animatorController;
            //_gamePoints = gamePoints;
            _botAISM = botAISM;
            _roadPointAIController = roadPointAIController;
        }

        public async void Enter()
        {
            _agent.character.Jump();
            await UniTask.Delay(500);
            _agent.character.StopJumping();
            _agent.character.Jump();
        }

        public void Exit()
        {
            _agent.character.StopJumping();
        }

        public void FixedUpdate()
        {
            //_agent.MoveToDestination(_gamePoints.FinishPoint.transform.position);
            _agent.MoveToDestination(_roadPointAIController.GetNextPoint());
        }

        public void CheckChangeState()
        {
            if (_agent.character.IsOnGround())
            {
                _botAISM.ChangeState<MoveAI>();
            }
        }
        
        private void CalculateDistanceJump()
        {

        }
    }
}
