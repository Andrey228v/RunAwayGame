namespace Assets._Scripts.ObjectsScripts.StateMachines
{
    public interface IStateSwitcher
    {
        public void ChangeState<T>() where T : IState;
    }
}
