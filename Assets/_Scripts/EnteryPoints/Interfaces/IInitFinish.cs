using Assets._Scripts.GameControllers;
using System.Collections.Generic;

namespace Assets._Scripts.EnteryPoints.Interfaces
{
    public interface IInitFinish
    {
        public IEnumerable<IFinish> Finished { get; }

        public void InitFinishData();
    }
}
