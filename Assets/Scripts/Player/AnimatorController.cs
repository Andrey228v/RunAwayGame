using ECM2;
using UnityEngine;
using VContainer.Unity;

namespace Assets.Scripts.Player
{
    public class AnimatorController
    {
        private Animator _animator;
        private Character _character;

        private const string StaticIdle = "Static_b";
        private const string Speed = "Speed_f";
        private const string IsJumping = "IsJumping_b";
        private const string IsFalling_b = "IsFalling_b";

        public AnimatorController(Character character, Animator animator)
        {
            _character = character;
            _animator = animator;
        }

        public void SetStatic(bool isStatic)
        {
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


    }
}
