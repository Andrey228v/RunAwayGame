using UnityEngine;

namespace Assets.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SkinConfig", menuName = "Game/SkinConfig")]
    public class SkinConfig : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private SkinnedMeshRenderer _skinMesh;
    }
}
