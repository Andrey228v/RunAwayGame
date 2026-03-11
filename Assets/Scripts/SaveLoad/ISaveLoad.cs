using Assets.Scripts.SaveLoad.Data;

namespace Assets.Scripts.SaveLoad
{
    public interface ISaveLoad
    {
        public void Save(LevelData data);

        public void Load(LevelData data);
    }
}
