using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy")]
public class EnemyConfig : ScriptableObject
{
    [SerializeField] private float _moveSpeed = 0.3f;
    [SerializeField] private float _decisionTime;
    [SerializeField] private int _minArmySize;
    [SerializeField] private int _maxArmySize;

    public float MoveSpeed => _moveSpeed;
    public float DecisionTime => _decisionTime;
    public int MinArmySize => _minArmySize;
    public int MaxArmySize => _maxArmySize;
}
