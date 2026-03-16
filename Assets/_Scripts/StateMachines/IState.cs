namespace Assets.Scripts.StateMachines
{
    public interface IState
    {
        public void Enter();

        public void FixedUpdate();

        public void CheckChangeState();

        public void Exit();
    }
}
