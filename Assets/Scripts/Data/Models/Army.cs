using UnityEngine;
using System;
using System.Collections.Generic;

public class Army
{
    private Fraction _owner;
    private int _armyCount;

    //Ресурсы армии
    private float _foodValue;
    private float _incomePerSec;

    private List<Village> _armyVillages = new List<Village>();

    public Fraction Owner => _owner;
    public int ArmyCount => _armyCount;

    public FractionManager.FractionType FractionType
    {
        get
        {
            if (_owner == FractionManager.Player) 
                return FractionManager.FractionType.Player;
            if (_owner == FractionManager.Enemy) 
                return FractionManager.FractionType.Enemy;
            if (_owner == FractionManager.Neutral) 
                return FractionManager.FractionType.Neutral;
            
            return FractionManager.FractionType.Neutral; // fallback
        }
    }

    public event Action OnFoodChanged;
    public event Action OnArmyChanged;
    public event Action OnIncomeChanged;

    public float Food
    {
        get => _foodValue;

        private set
        {
            if (value >= 0)
            {
                _foodValue = value;
            }
            else
            {
                _foodValue = 0;
            }

            OnFoodChanged?.Invoke();
        }
    }

    public float Income
    {
        get => _incomePerSec;

        private set
        {
            if (value > 0)
            {
                _incomePerSec = value;
            }
            else
            {
                _incomePerSec = 0;
            }

            OnIncomeChanged?.Invoke();
        }
    }

    public Army(Fraction owner, int armyCount, float startFood = 10)
    {
        _owner = owner;
        _armyCount = armyCount;
        Food = startFood;
        Income = 0;
    }

    public void ChangeFood(float value)
    {
        Food += value;
    }

    public void ChangeIncome(float value)
    {
        Income += value;
    }

    public void ChangeArmy(int value)
    {
        _armyCount += value;

        OnArmyChanged?.Invoke();
    }

    public void SetArmy(int value)
    {
        if (value >= 0)
        {
            _armyCount = value;

            OnArmyChanged?.Invoke();
        }
    }

    public void AddVillage(Village _villageScript)
    {
        if (!_armyVillages.Contains(_villageScript))
        {
            _armyVillages.Add(_villageScript);

            ChangeIncome(_villageScript.Income);
        }
    }

    public void DeleteVillage(Village _villageScript)
    {
        if (_armyVillages.Contains(_villageScript))
        {
            _armyVillages.Remove(_villageScript);

            ChangeIncome(-_villageScript.Income);
        }
    }

    public void DestroyArmy()
    {
        ClearVillages();
        Owner.FractionArmies.Remove(this);
    }

    private void ClearVillages()
    {
        for (int i = 0; i < _armyVillages.Count; i++)
        {
            //каждой деревни обнулить овнера
            _armyVillages.RemoveAt(i);
        }
    }
}
