using System.Runtime.CompilerServices;
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

        [Header("Settings")]
        [SerializeField] private string _textAllAudioSetting;
        [SerializeField] private string _textMusicSetting;
        [SerializeField] private string _textEffectSetting;
        [SerializeField] private string _textFlipAudio;
        [SerializeField] private string _textDeletSaveButton;
        [SerializeField] private string _textBackButton;

        public string ButtonAchievmentsName => _buttonAchievmentsName;

        public string ButtonSoundName => _buttonSoundName;

        public string ButtonShopName => _buttonShopName;

        public string ButtonSettingName => _buttonSettingName;

        public string ButtonExitName => _buttonExitName;

        public string TextGoldName => _textGoldName;

        public string TextGobeletsName => _textGobeletsName;

        public string TextAllAudioSetting => _textAllAudioSetting;

        public string TextMusicSetting => _textMusicSetting;

        public string TextEffectSetting => _textEffectSetting;

        public string TextFlipAudio => _textFlipAudio;

        public string TextDeletSaveButton => _textDeletSaveButton;

        public string TextBackButton => _textBackButton;

    }
}
