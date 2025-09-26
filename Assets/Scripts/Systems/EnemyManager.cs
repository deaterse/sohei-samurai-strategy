using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    [SerializeField] private List<EnemyArmyController> _allEnemies = new List<EnemyArmyController>();

    public void Init()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;

        Debug.Log("EnemyManager Initialized");

        StartCoroutine(WaitingEnemySpawner());
    }

    IEnumerator WaitingEnemySpawner()
    {
        while (EnemySpawner.Instance == null)
        {
            yield return null;
        }

        EnemySpawner.Instance.OnEnemySpawned += InitializeEnemy;

        EnemySpawner.Instance.AfterInit();
    }

    private void InitializeEnemy(EnemyArmyController _enemy)
    {
        _allEnemies.Add(_enemy);

        _enemy.OnVillageSteped += EnemyTryingToCapture;
    }

    private void EnemyTryingToCapture(Village _currentVillage, Army _army)
    {
        Village _targetVillage = VillageManager.Instance.AllVillages.Find(village => village != null && village == _currentVillage);

        if (_targetVillage != null)
        {
            VillageManager.Instance.TryingToCapture(_currentVillage, _army);
        }
    }
    
    private void OnDisable()
    {
        if (EnemySpawner.Instance != null)
        {
            EnemySpawner.Instance.OnEnemySpawned -= InitializeEnemy; // ← ОТПИСАТЬСЯ!
        }
        
        foreach (var enemy in _allEnemies)
        {
            if (enemy != null)
            {
                enemy.OnVillageSteped -= EnemyTryingToCapture; // ← ОТПИСАТЬСЯ!
            }
        }
        _allEnemies.Clear();
    }
}
