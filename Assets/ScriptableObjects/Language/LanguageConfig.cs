using UnityEngine;

namespace Assets.ScriptableObjects.Language
{

    [CreateAssetMenu(fileName = "LanguageConfig", menuName = "Game/Language Configuration")]
    public class LanguageConfig : ScriptableObject
    {
        [Header("Menu")]

        [Header("Menu")]
        [SerializeField] private string _buttonAchievmentsName;
        [SerializeField] private string _buttonSoundName;
        [SerializeField] private string _buttonShopName;
        [SerializeField] private string _buttonSettingName;
        [SerializeField] private string _buttonExitName;
        [SerializeField] private string _textGoldName;
        [SerializeField] private string _textGobeletsName;

    }
}
