namespace Assets.Scripts.SaveLoad
{
    public interface ISaveSystem
    {
        public void Save<T>(string key, T data);
        public T Load<T>(string key, T defaultValue = default);
        public bool HasKey(string key);
        public void Delete(string key);

        public void ResetAllProgress();
    }
}
