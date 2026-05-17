using Assets.Scripts.SaveLoad.Data;

namespace Assets._Scripts.SaveLoad.Data
{
    public interface IFinish
    {
        public void Finish(GameSaveData gameSaveData, LevelConfig levelConfig);
    }
}
