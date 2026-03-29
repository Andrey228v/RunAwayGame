using Assets.Scripts.SaveLoad.Data;

namespace Assets.Scripts.SaveLoad
{
    public interface ISaveLoad
    {
        public void Save(LevelData levelData);

        public void Load(LevelData levelData, LevelConfig levelConfig);
    }
}
