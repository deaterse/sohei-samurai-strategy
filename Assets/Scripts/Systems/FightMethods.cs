using UnityEngine;

public static class FightMethods
{
    public static int GetArmyLosses(Army _army, int _garrisonSize) // Get Army Losses when Capturing (Concentration Rule by Osipov-Lanchester)
    {
        float armyLosses;
        float minLosses = 0;

        if (_army.ArmyCount == _garrisonSize)
        {
            float _onePercentArmy = _army.ArmyCount * 0.01f;
            float _fivePercentArmy = _army.ArmyCount * 0.05f;

            armyLosses = Random.Range(_onePercentArmy, _fivePercentArmy);
        }
        else
        {
            minLosses = Mathf.Max(1, _garrisonSize * 0.01f);

            armyLosses = Mathf.Sqrt(-(_garrisonSize * _garrisonSize) + _army.ArmyCount * _army.ArmyCount);
        }

        armyLosses = Mathf.Max(minLosses, armyLosses);
        
        return (int)Mathf.Round(armyLosses);
    }
}
