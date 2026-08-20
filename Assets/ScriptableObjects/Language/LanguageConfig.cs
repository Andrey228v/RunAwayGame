using UnityEngine;
using UnityEngine.UI;

namespace Assets.ScriptableObjects.Language
{

    [CreateAssetMenu(fileName = "LanguageConfig", menuName = "Game/Language Configuration")]
    public class LanguageConfig : ScriptableObject
    {
        [Header("Menu")]
        [SerializeField] private string _buttonAchievmentsName;
        [SerializeField] private string _buttonSoundName;
        [SerializeField] private string _buttonShopName;
        [SerializeField] private string _buttonSettingName;
        [SerializeField] private string _buttonExitName;
        [SerializeField] private string _textGoldName;
        [SerializeField] private string _textGobeletsName;

        public string ButtonAchievmentsName => _buttonAchievmentsName;

        public string ButtonSoundName => _buttonSoundName;

        public string ButtonShopName => _buttonShopName;

        public string ButtonSettingName => _buttonSettingName;

        public string TextGoldName => _textGoldName;

        public string TextGobeletsName => _textGobeletsName;

    }
}
