using UnityEngine;
using System.Collections;
using System;

public class EnemyArmyController : ArmyController
{
    [Header("Config")]
    [SerializeField] private EnemyConfig _enemyConfig;

    void Awake()
    {
        InitializeComponents();
        _moveSpeed = _enemyConfig.MoveSpeed;
    }

    void Start()
    {
        MapGenerator.Instance.MapGenerated += StartDecisionCoroutine;
    }

    private void StartDecisionCoroutine() //прослойка для акшиона
    {
        StartCoroutine(DecisionCoroutine());
    }

    IEnumerator DecisionCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_enemyConfig.DecisionTime);

            if (!_isMoving)
            {
                FindTarget();
            }
        }
    }

    private void FindTarget()
    {
        var _nearVillage = FindNearestVillage();

        if (_nearVillage != null)
        {
            MoveToCoordinates(_nearVillage);
        }
        else
        {
            // патрулирование //
        }
    }

    private Village FindNearestVillage()
    {
        Village closestVillage = null;
        float closestDistance = Mathf.Infinity;

        foreach (Village _village in VillageManager.Instance.AllVillages)
        {
            if (IsVillageSuitable(_village))
            {
                float _distance = Vector2.Distance(transform.position, _village.transform.position);
                if (_distance < closestDistance)
                {
                    closestDistance = _distance;
                    closestVillage = _village;
                }
            }
        }

        return closestVillage;
    }

    private bool IsVillageSuitable(Village _villageScript)
    {
        if (_villageScript.GarrisonSize < ArmyData.ArmyCount && _villageScript.CurrentArmyOwner != ArmyData)
        {
            return true;
        }

        return false;
    }

    protected override void StopMoving()
    {
        base.StopMoving();

        StartCoroutine(DecisionCoroutine());
    }

    private void ActionInVillage()
    {
        //позже//
    }
}
