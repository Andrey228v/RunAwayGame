namespace Assets.Scripts.StateMachines
{
    public interface IStateSwitcher
    {
        public void ChangeState<T>() where T : IState;
    }
}
