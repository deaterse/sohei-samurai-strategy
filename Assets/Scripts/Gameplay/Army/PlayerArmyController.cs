using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class PlayerArmyController : ArmyController
{
    public static PlayerArmyController Instance { get; private set; }

    [Header("Config")]
    [SerializeField] private PlayerConfig _playerConfig;

    [Header("UI")]
    [SerializeField] private Text _foodText;
    [SerializeField] private Text _incomeText;

    void Start()
    {
        Instance = this;

        InitializeComponents();

        _moveSpeed = _playerConfig.MoveSpeed;

        StartCoroutine(SubscribeToInput());
        StartCoroutine(WaitingForArmyData());
    }

    IEnumerator SubscribeToInput()
    {
        while (ProcessClicker.Instance == null)
        {
            yield return null;
        }

        ProcessClicker.Instance.OnGroundClicked += MoveToCoordinates;
        ProcessClicker.Instance.OnVillageClicked += MoveToCoordinates;
    }

    IEnumerator WaitingForArmyData()
    {
        while (_armyData == null)
        {
            yield return null;
        }

        UIEventsInit();

        UpdateUI();
    }

    private void UIEventsInit()
    {
        _armyData.OnFoodChanged += UpdateUI;
        _armyData.OnIncomeChanged += UpdateUI;
    }

    private void UpdateUI()
    {
        if (_foodText != null && _incomeText != null && _armyData != null)
        {
            _foodText.text = _armyData.Food.ToString();
            _incomeText.text = _armyData.Income.ToString() + " per sec";
        }
    }

    protected override void CustomStopMovingLogic()
    {
        if (_currentDestination?.GetComponent<Village>())
        {
            Village _villageScript = _currentDestination.GetComponent<Village>();
            if (_villageScript.CurrentArmyOwner == ArmyData)
            {
                UIManager.Instance.OpenRecruitmentPanel(_villageScript);
            }
        }
        else
        {
            UIManager.Instance.CloseRecruitmentPanel();
        }
    }
}
