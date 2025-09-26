using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class Village : MonoBehaviour
{
    private int _garrisonSize;
    private Army _currentArmyOwner; //изначально армия нейтральная (создать армию в менеджеере?)

    private float _income;

    public float Income => _income;

    public Army CurrentArmyOwner
    {
        get => _currentArmyOwner;

        private set
        {
            if (_currentArmyOwner != value)
            {
                _currentArmyOwner = value;
                UpdateVisual();
            }
        }
    }

    public int GarrisonSize
    {
        get => _garrisonSize;

        private set
        {
            if (_garrisonSize != value)
            {
                _garrisonSize = value;
                UpdateVisual();
            }
        }
    }

    //UI
    [SerializeField] private TextMesh ArmyText;


    void Start()
    {
        // Start Settings
        _garrisonSize = Random.Range(1, 100);
        _income = GarrisonSize * 0.1f;

        ArmyText.text = GarrisonSize.ToString();
    }

    public void ChangeOwner(Army _army)
    {
        if (CurrentArmyOwner != _army)
        {
            CurrentArmyOwner = _army;
        }
    }

    public void AddGarrison(int _count)
    {
        if (_count > 0)
        {
            GarrisonSize += _count;
        }
    }
    public void DecreaseGarrison(int _count)
    {
        if (_count > 0)
        {
            GarrisonSize -= _count;
        }
    }

    private void UpdateVisual()
    {
        if (ArmyText != null && _currentArmyOwner != null)
        {
            ArmyText.text = "G: " + _garrisonSize.ToString();
            ArmyText.color = _currentArmyOwner.Owner.FractionColor;
        }

        // switch (_currentArmyOwner.FractionType)
            // {
            //     case FractionManager.FractionType.Player:
            //         ArmyText.color = _currentArmyOwner.Owner._fractionColor;
            //         break;
            //     case FractionManager.FractionType.Enemy:
            //         ArmyText.text = "Captured By Enemy";
            //         break;
            //     case FractionManager.FractionType.Neutral:
            //         ArmyText.text = "Neutral: " + _garrisonSize.ToString();
            //         break;
            //     default:
            //         Debug.Log("Village Fraction is undefined");
            //         break;
            // }
        }
}
