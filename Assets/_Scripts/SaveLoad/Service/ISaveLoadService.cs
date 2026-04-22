
namespace Assets._Scripts.SaveLoad.Service
{
    public interface ISaveLoadService
    {
        public void Dispose();

        public void Save();

        public void Load();
    }
}
