using Assets.Scripts.SaveLoad;
using System.Collections.Generic;

namespace Assets._Scripts.EnteryPoints.Interfaces
{
    public interface IInitSaveLoad
    {
        public IEnumerable<ISaveLoad> SaveLoads { get; }

        public void InitSaveLoadData(LevelConfig levelConfig);
    }
}
