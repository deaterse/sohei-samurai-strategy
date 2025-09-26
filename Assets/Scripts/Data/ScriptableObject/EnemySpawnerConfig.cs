using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawner", menuName = "Scriptable Objects/EnemySpawner")]
public class EnemySpawnerConfig : ScriptableObject
{
    [SerializeField] private int _enemiesCount;

    public int EnemiesCount => _enemiesCount;
}
