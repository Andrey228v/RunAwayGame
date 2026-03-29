using Assets._Scripts.GameControllers;
using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class AnimatorController : IDisposable, IRestart
    {
        private Animator _animator;

        private const string StaticIdle = "Static_b";
        private const string Speed = "Speed_f";
        private const string IsJumping = "IsJumping_b";
        private const string IsFalling_b = "IsFalling_b";
        private const string IsGround_b = "IsGround_b";

        public AnimatorController(Animator animator)
        {
            _animator = animator;
        }

        public void Dispose()
        {
            Debug.Log("Animator CONTROLLER DESTROU");
            _animator = null;
        }

        public void SetStatic(bool isStatic)
        {
            if(_animator  != null)
                _animator.SetBool(StaticIdle, isStatic);
        }

        public void SetMove(bool isMove)
        {
            float speed;

            if (isMove)
            {
                speed = 1;
            }
            else
            {
                speed = 0;
            }

            _animator.SetFloat(Speed, speed);
        }

        public void SetJump(bool isJump)
        {
            _animator.SetBool(IsJumping, isJump);
        }

        public void SetFall(bool isFall)
        {
            _animator.SetBool(IsFalling_b, isFall);
        }

        public void SetGround(bool isGround) 
        {
            _animator.SetBool(IsGround_b, isGround);
        }

        public void Restart()
        {
            SetMove(false);
            SetJump(false);
            SetFall(false);
            SetGround(false);
        }
    }
}
