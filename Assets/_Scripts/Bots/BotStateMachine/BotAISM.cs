using Assets._Scripts.Bots.BotStateMachine.States;
using Assets._Scripts.GameControllers;
using Assets.Scripts.Player;
using Assets.Scripts.Points;
using Assets.Scripts.StateMachines;
using ECM2;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;

namespace Assets._Scripts.Bots.BotStateMachine
{
    public class BotAISM : IStateSwitcher, IDisposable, IRestart
    {
        private List<IState> _states = new List<IState>();
        private IState _currentState;
        private IState _prevState;

        private NavMeshCharacter _agent;
        private AnimatorController _animatorController;
        private GamePoints _gamePoints;

        public BotAISM(NavMeshCharacter agent, AnimatorController animatorController, GamePoints gamePoints) 
        {
            _agent = agent;
            _animatorController = animatorController;
            _gamePoints = gamePoints;

            Start();
        }

        public void Dispose()
        {

        }


        public void Start()
        {
            _states = new List<IState>()
            {
               new MoveAI(this, _agent, _animatorController, _gamePoints),
               new JumpAI(this, _agent, _animatorController, _gamePoints),
               new StayAI(),
            };

            _currentState = _states[0];
            _prevState = _states[0];
            _currentState.Enter();
        }

        public void FixedTick()
        {
            _currentState.FixedUpdate();
            _currentState.CheckChangeState();
        }

        public void ChangeState<T>() where T : IState
        {
            var state = _states.FirstOrDefault(state => state is T);
            _prevState = _currentState;
            _currentState.Exit();
            _currentState = state;
            _currentState?.Enter();
        }



        public void Restart()
        {
            //_animatorController.Restart();
        }
    }
}
