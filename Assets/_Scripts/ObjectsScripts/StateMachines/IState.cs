namespace Assets._Scripts.ObjectsScripts.StateMachines
{
    public interface IState
    {
        public void Enter();

        public void FixedUpdate();

        public void CheckChangeState();

        public void Exit();
    }
}
