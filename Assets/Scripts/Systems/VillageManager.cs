using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class VillageManager : MonoBehaviour
{
    public static VillageManager Instance { get; private set; }

    private List<Village> _allVillages;

    public List<Village> AllVillages => _allVillages;

    public static event Action VillageManagerInitialized;

    private bool _isInitialized = false;
    private GameManager _gameManager;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Init()
    {
        if (_isInitialized) return;

        Debug.Log("VillageManager Initialized");

        _allVillages = new List<Village>();
        VillageManagerInitialized?.Invoke();

        _isInitialized = true;
    }

    public void SetVillages(List<Village> _generatedVillagesList)
    {
        if (_generatedVillagesList != _allVillages && _generatedVillagesList.Count > 0)
        {
            _allVillages = _generatedVillagesList;
        }
    }

    public void SubscribeToInput()
    {
        PlayerArmyController.Instance.OnVillageSteped += TryingToCapture;
    }

    public void TryingToCapture(Village _villageScript, Army _army)
    {
        if (_villageScript == null) return;

        _gameManager = GameManager.Instance;

        if (_villageScript.CurrentArmyOwner == null || _villageScript.CurrentArmyOwner != _army)
        {
            bool _canCapture = false;

            _canCapture = CanCapture(_army.ArmyCount, _villageScript.GarrisonSize);

            if (_canCapture)
            {
                Capture(_villageScript, _army);
            }
        }
    }

    private bool CanCapture(int _armyCount, int _garrisonSize)
    {
        if (_armyCount > _garrisonSize)
        {
            return true;
        }

        return false;
    }

    private void Capture(Village _villageScript, Army _army)
    {
        Debug.Log($"Capture Attempt from {_army.Owner.FractionName}");

        if (_villageScript.CurrentArmyOwner != null && _villageScript.CurrentArmyOwner != _army)
        {
            _villageScript.CurrentArmyOwner.DeleteVillage(_villageScript);
        }

        int finalArmyCount = FightMethods.GetArmyLosses(_army, _villageScript.GarrisonSize);
        Debug.Log(finalArmyCount);
        _army.SetArmy(finalArmyCount);

        _villageScript.DecreaseGarrison(_villageScript.GarrisonSize);

        _villageScript.ChangeOwner(_army);

        _army.AddVillage(_villageScript);

        Debug.Log($"Village captured by {_army.Owner.FractionName}");
    }
}
