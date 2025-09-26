using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Scriptable Objects/Player")]
public class PlayerConfig : ScriptableObject
{
    [Header("Settings")]
    [SerializeField] private float _moveSpeed = 0.3f;

    public float MoveSpeed => _moveSpeed;

    [Header("Spawn Coordinates")]
    [SerializeField] private int _minXCoordinates;
    [SerializeField] private int _maxXCoordinates;
    [SerializeField] private int _minYCoordinates;
    [SerializeField] private int _maxYCoordinates;

    public Vector2 GetRandomPosition()
    {
        return new Vector2(UnityEngine.Random.Range(_minXCoordinates, _maxXCoordinates), UnityEngine.Random.Range(_minYCoordinates, _maxYCoordinates));
    }

    public int MinXCoord => _minXCoordinates;
    public int MaxXCoord => _maxXCoordinates;
    public int MinYCoord => _minYCoordinates;
    public int MaxYCoord => _maxYCoordinates;
}
