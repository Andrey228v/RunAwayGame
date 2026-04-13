using Assets._Scripts.Bots.BotStateMachine;
using Assets._Scripts.ObjectsScripts.Points;
using ECM2;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

namespace Assets._Scripts.Bots
{
    public class Bot : IDisposable
    {
        private readonly NavMeshCharacter _agent;
        private readonly BotAISM _botAISM;
        private readonly RoadPointAIController _roadPointAIController;
        private readonly BotMB _botMB;

        public Bot(NavMeshCharacter agent, BotAISM botAISM, RoadPointAIController roadPointAIController, Vector3 startPosition, Vector3 destination) 
        {
            _agent = agent;
            _botAISM = botAISM;
            _roadPointAIController = roadPointAIController;
            _botMB = agent.GetComponent<BotMB>(); // Переделать ... ??
            _agent.agent.Warp(startPosition);
            _agent.agent.SetDestination(destination);   

            Sub();
        }

        public void Sub()
        {
            _agent.DestinationReached += OnDestinationReached;
            _botMB.OnDie += SetPointPosition;
            _roadPointAIController.OnBotFinish += RestartBot;
        }

        public void Dispose()
        {
            if (_agent != null)
            {
                _agent.DestinationReached -= OnDestinationReached;
                _botMB.OnDie -= SetPointPosition;
                _roadPointAIController.OnBotFinish -= RestartBot;

                _botAISM.Dispose();
                _roadPointAIController.Dispose();
            }
        }

        public void FixedUpdateSM()
        {
            _botAISM.FixedTick();
        }

        private void OnDestinationReached()
        {
            _roadPointAIController.AddPointCounter();
        }

        public void SetPointPosition()
        {
            if (_roadPointAIController != null) 
            {
                Vector3 position = _roadPointAIController.GetCurrentPoint();
                _agent.agent.Warp(position);
                _roadPointAIController.AddPointCounter();
            }
        }

        private void RestartBot()
        {
            SetPointPosition();

            if (_agent.character != null)
            {
                _agent.character.SetVelocity(Vector3.zero);
                _agent.character.StopJumping();
                _agent.character.SetMovementMode(Character.MovementMode.Falling);

            }
            _agent.agent.ResetPath();
            _agent.agent.velocity = Vector3.zero;
        }
    }
}
