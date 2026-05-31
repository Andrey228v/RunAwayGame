using Assets.Scripts.SaveLoad.Data;

namespace Assets._Scripts.SaveLoad.Data
{
    public interface IInitialzation
    {
        public void Initialzation(GameSaveData gameSaveData, LevelConfig levelConfig);
    }
}
