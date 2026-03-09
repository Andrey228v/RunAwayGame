using Assets.Scripts.SaveLoad.Data;

namespace Assets.Scripts.SaveLoad.Service
{
    public interface ISaveService
    {
        public void SaveAllLevel();
        public void LoadAllLevel();
        public void SaveLevelData(string levelID, LevelData data);
        public LevelData GetLevelData(string levelID);
        public bool HasSaveData();
        public void DeleteSave();
    }
}
