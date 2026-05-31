using Assets.Scripts.SaveLoad.Data;

namespace Assets._Scripts.SaveLoad.Data
{
    public interface ILoad
    {
        public void Load(GameSaveData gameSaveData, LevelConfig levelConfig);
    }
}
