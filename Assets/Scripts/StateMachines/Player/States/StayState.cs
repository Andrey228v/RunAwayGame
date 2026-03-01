using Assets.Input;
using Assets.Scripts.Player;
using System;
using UnityEngine;

namespace Assets.Scripts.StateMachines.Player.States
{
    public class StayState : IState, IDisposable
    {
        private readonly IStateSwitcher _stateSwitcher;
        private InputReader _inputReader;
        private AnimatorController _animatorController;
        private bool _isMove  = false;

        public StayState(IStateSwitcher stateSwitcher, InputReader inputReader, AnimatorController animatorController) 
        {
            _stateSwitcher = stateSwitcher;
            _inputReader = inputReader;
            _animatorController = animatorController;

            //_inputReader.OnStartMove += ChangeMoveState;
            //_inputReader.OnJumped += ChangeJumpState;
            //_inputReader.OnMoved += (bool value) => { _isMove = value; };
        }

        public void Dispose()
        {
            _inputReader.OnStartMove -= ChangeMoveState;
            _inputReader.OnJumped -= ChangeJumpState;
            //_inputReader.OnMoved -= (bool value) => { _isMove = value; };
        }


        public void Enter()
        {
            _inputReader.OnStartMove += ChangeMoveState;
            _inputReader.OnJumped += ChangeJumpState;
            _animatorController.SetStatic(true);
        }

        public void Update()
        {

        }

        public void FixedUpdate()
        {
            
        }

        public void LateUpdate()
        {

        }

        public void Exit()
        {
            _inputReader.OnStartMove -= ChangeMoveState;
            _inputReader.OnJumped -= ChangeJumpState;
            //_animatorController.SetStatic(false);
        }

        public void CheckChangeState()
        {
            //if (_isMove)
            //    _stateSwitcher.ChangeState<MoveState>();
        }

        public void ChangeMoveState()
        {
            _animatorController.SetStatic(false);
            _stateSwitcher.ChangeState<MoveState>();
        }

        private void ChangeJumpState()
        {
            _stateSwitcher.ChangeState<JumpState>();
        }
    }
}
