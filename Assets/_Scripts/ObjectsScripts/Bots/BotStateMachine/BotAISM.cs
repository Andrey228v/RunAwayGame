using Assets._Scripts.ObjectsScripts.Bots.BotStateMachine.States;
using Assets._Scripts.ObjectsScripts.Player;
using Assets._Scripts.ObjectsScripts.Points;
using Assets._Scripts.ObjectsScripts.StateMachines;
using ECM2;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets._Scripts.ObjectsScripts.Bots.BotStateMachine
{
    public class BotAISM : IStateSwitcher, IDisposable
    {
        private List<IState> _states = new List<IState>();
        private IState _currentState;
        private IState _prevState;

        private NavMeshCharacter _agent;
        private AnimatorController _animatorController;
        private RoadPointAIController _roadPointAIController;

        public BotAISM(NavMeshCharacter agent, 
            AnimatorController animatorController, 
            RoadPointAIController roadPointAIController) 
        {
            _agent = agent;
            _animatorController = animatorController;
            _roadPointAIController = roadPointAIController;

            Start();
        }

        public void Dispose()
        {
            _states.Clear();
        }


        public void Start()
        {
            _states = new List<IState>()
            {
               new MoveAI(this, _agent, _animatorController, _roadPointAIController),
               new JumpAI(this, _agent, _animatorController, _roadPointAIController),
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
    }
}
