using Assets.Scripts.Player;
using Assets.Scripts.Points;
using Assets.Scripts.StateMachines;
using Cysharp.Threading.Tasks;
using ECM2;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

namespace Assets._Scripts.Bots.BotStateMachine.States
{
    public class JumpAI : IState
    {

        private NavMeshCharacter _agent;
        private AnimatorController _animatorController;
        private GamePoints _gamePoints;
        private BotAISM _botAISM;
        private CancellationTokenSource _jumpCts;

        public JumpAI(BotAISM botAISM, NavMeshCharacter agent, AnimatorController animatorController, GamePoints gamePoints)
        {
            _agent = agent;
            _animatorController = animatorController;
            _gamePoints = gamePoints;
            _botAISM = botAISM;
        }

        public async void Enter()
        {
            // Отменяем предыдущий прыжок (если был)
            _jumpCts?.Cancel();
            _jumpCts = new CancellationTokenSource();

            try
            {
                await PerformJump(_jumpCts.Token);
            }
            catch (OperationCanceledException)
            {
                Debug.Log("[JumpAI] Jump cancelled");
            }


            //OffMeshLinkData data = _agent.agent.currentOffMeshLinkData;
            //Vector3 startPos = _agent.transform.position;
            //Vector3 endPos = data.endPos;

            //Vector3 direction = (endPos - startPos).normalized;
            //float distance = Vector3.Distance(startPos, endPos);

            //float horizontalSpeed = distance / 1f;
            //float verticalSpeed = (3f / 1f) * 2f;

            //Vector3 jumpVelocity = direction * horizontalSpeed + Vector3.up * verticalSpeed;

            //Vector3 launchVelocity = direction * (distance / 1f) + Vector3.up * (5f / 1f * 2f);

            //_agent.character.LaunchCharacter(launchVelocity);
            //_agent.character.SetVelocity(jumpVelocity);
            //_agent.character.Jump();
        }

        public void Exit()
        {
            // Отменяем незавершенный прыжок
            _jumpCts?.Cancel();

            // Страховка: если выходим не через приземление
            if (_agent.character.GetMovementMode() == Character.MovementMode.Flying)
            {
                _agent.character.SetMovementMode(Character.MovementMode.Walking);
            }
            _agent.character.StopJumping();

            //_agent.character.StopJumping();
        }

        public void FixedUpdate()
        {
          
        }

        public void CheckChangeState()
        {
            if (_agent.character.IsOnGround())
            {
                _botAISM.ChangeState<MoveAI>();
            }
        }

        private async UniTask PerformJump(CancellationToken token)
        {
            // Переключаем ECM2 в режим полета
            _agent.character.SetMovementMode(Character.MovementMode.Flying);

            // Даем ECM2 время на переключение режима
            await UniTask.Yield(PlayerLoopTiming.Update, token);

            OffMeshLinkData data = _agent.agent.currentOffMeshLinkData;
            Vector3 startPos = _agent.transform.position;
            Vector3 endPos = data.endPos;

            Vector3 direction = (endPos - startPos).normalized;
            float distance = Vector3.Distance(startPos, endPos);

            // Настройки прыжка
            float jumpDuration = 0.6f;
            float jumpHeight = 2.5f;

            float horizontalSpeed = distance / jumpDuration;
            float verticalSpeed = (jumpHeight / jumpDuration) * 2f;

            Vector3 jumpVelocity = direction * horizontalSpeed + Vector3.up * verticalSpeed;

            // Применяем скорость
            _agent.character.SetVelocity(jumpVelocity);

            // Ждем приземления (с возможностью отмены)
            await WaitForLanding(endPos, jumpDuration, token);

            // Возвращаем ECM2 в обычный режим
            _agent.character.SetMovementMode(Character.MovementMode.Walking);

            // Сообщаем NavMeshAgent, что линк пройден
            _agent.agent.CompleteOffMeshLink();

            // Переключаемся обратно в состояние MoveAI
            _botAISM.ChangeState<MoveAI>();
        }

        private async UniTask WaitForLanding(Vector3 endPos, float jumpDuration, CancellationToken token)
        {
            // Ждем примерно 70% от длительности прыжка
            await UniTask.Delay(TimeSpan.FromSeconds(jumpDuration * 0.7f), cancellationToken: token);

            // Проверяем приземление с интервалами
            float maxWaitTime = 1f;
            float elapsed = 0f;
            float checkInterval = 0.05f;

            while (elapsed < maxWaitTime)
            {
                if (_agent.character.IsOnGround())
                {
                    return; // Приземлились!
                }

                await UniTask.Delay(TimeSpan.FromSeconds(checkInterval), cancellationToken: token);
                elapsed += checkInterval;
            }
        }
    }
}
