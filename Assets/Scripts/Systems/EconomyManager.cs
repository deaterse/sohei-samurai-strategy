using UnityEngine;
using System.Collections.Generic;

public class EconomyManager
{
    public static EconomyManager Instance { get; private set; }

    private List<Army> _allArmies = new List<Army>();

    private float _incomeTimer;
    private const float _incomeInterval = 1f;

    private EconomyManager() { }

    public static void Init()
    {
        Instance = new EconomyManager();

        Debug.Log("EconomyManager Initialized");
    }

    public void Update(float deltaTime)
    {
        _incomeTimer += deltaTime;

        if (_incomeTimer >= _incomeInterval)
        {
            ProcessIncome();
            _incomeTimer = 0f;
        }
    }

    private void ProcessIncome()
    {
        foreach (Army _army in _allArmies)
        {
            _army.ChangeFood(_army.Income);
        }
    }

    public void RegisterArmy(Army army)
    {
        if (!_allArmies.Contains(army))
        {
            _allArmies.Add(army);
        }
    }
}
