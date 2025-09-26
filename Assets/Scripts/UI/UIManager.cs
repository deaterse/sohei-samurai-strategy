using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private Text _incomeText;
    [SerializeField] private Text _foodText;
    [SerializeField] private TextMesh _armyText;
    [SerializeField] private GameObject _recruitmentPanel;

    private Fraction _playerFraction;

    private Village _selectedVillage;

    private void Start()
    {
        Instance = this;
        StartCoroutine(WaitingForFraction());
    }

    IEnumerator WaitingForFraction()
    {
        while (FractionManager.Instance == null)
        {
            yield return null;
        }

        _playerFraction = FractionManager.Player;
    }

    public void CloseRecruitmentPanel()
    {
        _selectedVillage = null;
        _recruitmentPanel.SetActive(false);
    }

    public void OpenRecruitmentPanel(Village village)
    {
        _selectedVillage = village;
        _recruitmentPanel.SetActive(true);
    }

    public void OnRecruitButtonClick()
    {
        if (_selectedVillage != null)
        {
            RecruitmentManager.Instance.RecruitUnit(_selectedVillage, PlayerArmyController.Instance.ArmyData);
        }
    }

    public void OnAddGarrisonButtonClick()
    {
        if (_selectedVillage != null)
        {
            RecruitmentManager.Instance.RecruitUnitToGarrison(_selectedVillage, PlayerArmyController.Instance.ArmyData);
        }
    }

    private void UpdateArmy(int _army)
    {
        _armyText.text = _army.ToString();
    }
    private void UpdateFood(float _food)
    {
        _foodText.text = _food.ToString();
    }
    private void UpdateIncome(float _income)
    {
        _incomeText.text = _income.ToString() + " per sec";
    }
}
