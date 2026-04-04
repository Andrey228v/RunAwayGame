using Assets._Scripts.GameControllers;
using Assets.Scripts.Player;
using Assets.Scripts.StateMachines;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Assets._Scripts.Bots.BotStateMachine
{
    public class BotAISM : IStateSwitcher, IDisposable, IRestart
    {
        private List<IState> _states = new List<IState>();
        private IState _currentState;
        private IState _prevState;

        public BotAISM(AnimatorController animatorController) 
        {

        }


        public void Dispose()
        {
            throw new NotImplementedException();
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
            _animatorController.Restart();
        }
    }
}
