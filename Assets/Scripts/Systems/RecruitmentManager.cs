using UnityEngine;

public class RecruitmentManager : MonoBehaviour
{
    public static RecruitmentManager Instance { get; private set; }

    [SerializeField] private RecruitConfig _config;

    void Start()
    {
        Instance = this;
    }

    public void RecruitUnit(Village _village, Army _army)
    {
        if (_village.CurrentArmyOwner == _army)
        {
            if (_army.Food >= _config.UnitPrice)
            {
                _army.ChangeFood(-_config.UnitPrice);
                _army.ChangeArmy(1);
            }
        }
    }

    public void RecruitUnitToGarrison(Village _village, Army _army)
    {
        if (_village.CurrentArmyOwner == _army)
        {
            if (_army.Food >= _config.UnitPrice)
            {
                _army.ChangeFood(-_config.UnitPrice);
                _village.AddGarrison(1);
            }
        }
    }
}