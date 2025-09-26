using UnityEngine;
using System;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    [SerializeField] private EnemySpawnerConfig _spawnConfig;

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _minRange; // coordinates min range (x y)
    [SerializeField] private int _maxRange; // coordinates max range (x y)

    public event Action<EnemyArmyController> OnEnemySpawned;

    private void Awake()
    {
        Init();
    }

    public void AfterInit()
    {
        StartCoroutine(WaitingFractionManager());
    }

    private void Init()
    {
        if (Instance != null)
        {
            return;
        }

        Instance = this;
    }

    private void SpawnCycle()
    {
        for (int i = 0; i < _spawnConfig.EnemiesCount; i++)
        {
            SpawnEnemy();
        }
    }

    public IEnumerator WaitingFractionManager()
    {
        while (FractionManager.Instance == null)
        {
            yield return null;
        }

        SpawnCycle();
    }

    private void SpawnEnemy()
    {
        Vector2 _randomPos = new Vector2(UnityEngine.Random.Range(_minRange, _maxRange), UnityEngine.Random.Range(_minRange, _maxRange));

        GameObject _newEnemy = Instantiate(_enemyPrefab, _randomPos, Quaternion.identity);

        if (_newEnemy.TryGetComponent(out EnemyArmyController _enemyController))
        {
            Fraction enemyFraction = FractionManager.Enemy;
            Army enemyArmyData = enemyFraction.CreateArmy();

            _enemyController.Init(enemyArmyData);

            EconomyManager.Instance.RegisterArmy(enemyArmyData);

            OnEnemySpawned?.Invoke(_enemyController);
        }
        else
        {
            Debug.Log("Prefab doesnt have EnemyArmyController script.");
        }
    }
}
