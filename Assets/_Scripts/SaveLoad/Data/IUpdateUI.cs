using Assets.Scripts.SaveLoad.Data;

namespace Assets._Scripts.SaveLoad.Data
{
    public interface IUpdateUI
    {
        public void UpdateUI(GameSaveData gameSaveData, LevelConfig levelConfig);
    }
}
