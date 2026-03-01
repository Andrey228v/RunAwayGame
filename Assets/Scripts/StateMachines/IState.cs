namespace Assets.Scripts.StateMachines
{
    public interface IState
    {
        public void Enter();

        public void Update();

        public void FixedUpdate();

        public void LateUpdate();

        public void CheckChangeState();

        public void Exit();
    }
}
