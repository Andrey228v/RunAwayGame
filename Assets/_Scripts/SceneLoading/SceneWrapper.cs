using Eflatun.SceneReference;
using System;

namespace Assets._Scripts.SceneLoading
{
    public enum SceneType
    {
        Active,
        NoMain,
        Menu,
        HUD,
        Player,
    }

    [Serializable]
    public class SceneWrapper
    {

        public SceneReference scene;
        public string Name => scene.Name;
        public SceneType type;
    }
}
