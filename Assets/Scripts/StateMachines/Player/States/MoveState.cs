using Assets.Input;
using Assets.Scripts.Player;
using ECM2;
using System;
using UnityEngine;

namespace Assets.Scripts.StateMachines.Player.States
{
    public class MoveState : IState, IDisposable
    {
        private const string Speed = "Speed_f";

        private readonly IStateSwitcher _stateSwitcher;
        private PlayerMovement _playerMovement;
        private PlayerRotator _playerRotator;
        private InputReader _inputReader;
        private Animator _animator;
        private Character _character;
        AnimatorController _animatorController;

        public MoveState(IStateSwitcher stateSwitcher, PlayerMovement playerMovement,
            PlayerRotator playerRotator, InputReader inputReader, Character character, AnimatorController animatorController)
        {
            _stateSwitcher = stateSwitcher;
            _playerMovement = playerMovement;
            _playerRotator = playerRotator;
            _inputReader = inputReader;
            _animatorController = animatorController;
            _character = character;
            //_animator = character.GetComponent<Animator>();

            _inputReader.OnJumped += ChangeStateJump;
        }

        public void Dispose()
        {
            _inputReader.OnJumped -= ChangeStateJump;
        }

        public void Enter()
        {
            //Debug.Log(_character);
            _character.animator.SetFloat(Speed, 1);
            //_animatorController.SetMove(true);
            //_animator.SetFloat(Speed, 1);
        }

        public void Update()
        {
            
        }

        public void FixedUpdate()
        {
            _playerMovement.Move();
            _playerRotator.Rotate();
        }

        public void Exit()
        {
            //_animator.SetFloat(Speed, 0);
        }

        public void CheckChangeState()
        {

        }

        private void ChangeStateJump()
        {
            _stateSwitcher.ChangeState<JumpState>();
        }
    }
}
