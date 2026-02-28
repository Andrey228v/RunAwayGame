using Assets.Input;
using Assets.Scripts.Player;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Assets.Scripts.StateMachines.Player.States
{
    public class JumpState : IState, IDisposable
    {
        private int _forceX = 5;
        private int _forceY = 10;

        private IStateSwitcher _stateSwitcher;
        private PlayerJumper _playerJumper;
        private readonly InputReader _inputReader;
        private PlayerMoveDirectionCalculator _playerMoveDirectionCalculator;
        private PlayerMovement _playerMovement;
        private PlayerRotator _playerRotator;
        private PlayerGroundChecker _playerGroundChecker;
        private PlayerGravityController _playerGravityController;

        private bool _isGround = false;

        public JumpState(IStateSwitcher stateSwitcher, InputReader inputReader, 
            PlayerMoveDirectionCalculator playerMoveDirectionCalculator, PlayerMovement playerMovement, 
            PlayerRotator playerRotator, PlayerGroundChecker playerGroundChecker, PlayerGravityController playerGravityController,
            PlayerJumper playerJumper)
        {
            _stateSwitcher = stateSwitcher;
            _inputReader = inputReader;
            _playerMoveDirectionCalculator = playerMoveDirectionCalculator;
            _playerMovement = playerMovement;
            _playerRotator = playerRotator;
            _playerGroundChecker = playerGroundChecker;
            _playerJumper = playerJumper;
        }

        public void Dispose()
        {

        }

        public void Enter()
        {
            _playerJumper.Jump();
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
            _playerJumper.StopJump();
        }

        public async void CheckChangeState()
        {
            await UniTask.NextFrame();
            _isGround = _playerGroundChecker.GetIsGrounded();
            Debug.Log(_isGround);

            if (_isGround)
            {
                ChangeStateMove();
            }
        }

        private void ChangeStateMove()
        {
            
            _stateSwitcher.ChangeState<MoveState>();
        }
    }
}
