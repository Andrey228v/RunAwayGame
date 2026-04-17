using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Game/Level Configuration")]
public class LevelConfig : ScriptableObject
{
    [Header("Level Identification")]
    [SerializeField] private int _levelId; // убрать...
    [SerializeField] private string _levelName;
    [SerializeField] private string _sceneName;

    [Header("Spawn Settings")]
    [SerializeField] private Vector3 _startPoint;
    [SerializeField] private Vector3 _finishPoint;
    [SerializeField] private Vector3 _playerStartRotation;

    public int LevelId => _levelId; // убрать...
    public string LevelName => _levelName;
    public string SceneName => _sceneName;
    public Vector3 StartPosition => _startPoint;
    public Quaternion PlayerStartRotation => Quaternion.Euler(_playerStartRotation);
    public Vector3 StartRotationEuler => _playerStartRotation;
    public Vector3 FinishPosition => _finishPoint;
}
