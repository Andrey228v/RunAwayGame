namespace Assets.Scripts.SaveLoad
{
    public class EasySaveSystem : ISaveSystem
    {
        public void Save<T>(string key, T data)
        {
            ES3.Save(key, data);
        }

        public T Load<T>(string key, T defaultValue = default)
        {
            return ES3.Load(key, defaultValue);
        }

        public bool HasKey(string key)
        {
            return ES3.KeyExists(key);
        }

        public void Delete(string key)
        {
            ES3.DeleteKey(key);
        }

        public void ResetAllProgress()
        {
            ES3.DeleteFile();
        }
    }
}
