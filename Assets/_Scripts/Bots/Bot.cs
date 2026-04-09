using Assets._Scripts.Bots.BotStateMachine;
using Assets._Scripts.ObjectsScripts.Points;
using ECM2;
using System;
using UnityEngine;

namespace Assets._Scripts.Bots
{
    public class Bot : IDisposable
    {
        private NavMeshCharacter _agent;
        private BotAISM _botAISM;
        private RoadPointAIController _roadPointAIController;
        private BotMB _botMB;

        public Bot(NavMeshCharacter agent, BotAISM botAISM, RoadPointAIController roadPointAIController) 
        {
            _agent = agent;
            _botAISM = botAISM;
            _roadPointAIController = roadPointAIController;
            _botMB = agent.GetComponent<BotMB>(); // Переделать ... ??
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
            Vector3 position = _roadPointAIController.GetCurrentPoint();
            _botMB.transform.position = position;
            _roadPointAIController.AddPointCounter();
        }

        private void RestartBot()
        {
            SetPointPosition();
        }
    }
}
