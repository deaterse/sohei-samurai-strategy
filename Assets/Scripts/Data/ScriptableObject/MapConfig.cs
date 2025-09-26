using UnityEngine;

[CreateAssetMenu(fileName = "Map", menuName = "Scriptable Objects/Map")]
public class MapConfig : ScriptableObject
{
    [Header("Spawn Config")]
    [SerializeField] private int _villagesCount;
    [SerializeField] private bool _spawnRoads;
    [SerializeField] private Vector2 _mapSize;

    public int VillagesCount => _villagesCount;
    public bool SpawnRoads => _spawnRoads;
    public Vector2 MapSize => _mapSize;

    public bool IsWithinMapBounds(Vector2 position)
    {
        float mapWidth = 200f;
        float mapHeight = 200f;

        return position.x >= -mapWidth / 2 &&
            position.x <= mapWidth / 2 &&
            position.y >= -mapHeight / 2 &&
            position.y <= mapHeight / 2;
    }
}
