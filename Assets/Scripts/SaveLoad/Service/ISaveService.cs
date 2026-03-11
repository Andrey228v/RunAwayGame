using Assets.Scripts.SaveLoad.Data;

namespace Assets.Scripts.SaveLoad.Service
{
    public interface ISaveService
    {
        public void SetLevelId(string levelId);
        public void LoadLevel();
        public void SaveLevelData();
        public LevelData GetLevelData();
        public void DeleteSave();
    }
}
