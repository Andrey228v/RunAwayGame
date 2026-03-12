namespace Assets.Scripts.SaveLoad
{
    public class EasySaveSystem : ISaveSystem
    {
        private readonly string _encryptionPassword = "I`m going to Stars"; // Для шифрования

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

        //private ES3Settings GetSettings()
        //{
        //    return new ES3Settings
        //    {
        //        encryptionType = ES3.EncryptionType.AES,
        //        encryptionPassword = _encryptionPassword,
        //        format = ES3.Format.JSON // Для читаемости, можно Binary
        //    };
        //}
    }
}
