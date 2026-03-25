using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets._Scripts.SceneLoading
{
    [Serializable]
    public class SceneGroupHandle
    {
        public string groupName;
        public List<SceneWrapper> scenesNames;

        public string FindSceneNameByType(SceneType sceneType)
        {
            // под вопросом
            return scenesNames.FirstOrDefault(scene => scene.type == sceneType).Name;
        }

        //Надо изменить...
        public SceneWrapper FindScene(string sceneName)
        {
            return scenesNames.FirstOrDefault(scene => scene.Name == sceneName);
        }
    }
}
