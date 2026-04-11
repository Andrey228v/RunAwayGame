using Assets._Scripts.ObjectsScripts.Points;
using Assets.Scripts.Player;
using Assets.Scripts.Points;
using Assets.Scripts.StateMachines;
using Cysharp.Threading.Tasks;
using ECM2;
using System.Threading;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;


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
            //Vector3 startPoint = transform.TransformPoint(navMeshLink.startPoint);
            //Vector3 endPoint = transform.TransformPoint(navMeshLink.endPoint);

            //// Вычисляем евклидово расстояние (прямую линию)
            //float length = Vector3.Distance(startPoint, endPoint);

            var data = TryGetCurrentNavMeshLink();
            var distance = Vector3.Distance(data.startPos, data.endPos);


            return distance;
        }

        private OffMeshLinkData TryGetCurrentNavMeshLink()
        {
            // Получаем данные текущего линка
            OffMeshLinkData linkData = _agent.agent.currentOffMeshLinkData;

            // Пытаемся получить компонент линка через navMeshOwner
            // Это работает как для NavMeshLink, так и для OffMeshLink [citation:3]
            if (_agent.agent.navMeshOwner is NavMeshLink navMeshLink)
            {
                Debug.Log($"Найден NavMeshLink: {navMeshLink.name}");
                // Здесь вы можете получить доступ к свойствам NavMeshLink
                // например, к navMeshLink.startPoint, navMeshLink.endPoint, navMeshLink.area и т.д.
            }
            // Если это старый компонент OffMeshLink, он будет доступен напрямую
            else if (linkData.owner != null)
            {
                Debug.Log($"Найден OffMeshLink: {linkData.owner.name}");
            }
            else
            {
                Debug.Log("Агент на линке, но компонент линка не найден.");
            }

            return linkData;
        }
    }
}
