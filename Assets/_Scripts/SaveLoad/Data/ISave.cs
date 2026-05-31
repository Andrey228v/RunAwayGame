using Assets.Scripts.SaveLoad.Data;

namespace Assets._Scripts.SaveLoad.Data
{
    public interface ISave
    {
        public void Save(GameSaveData gameSaveData, LevelConfig levelConfig);
    }
}
