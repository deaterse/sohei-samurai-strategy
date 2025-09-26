using UnityEngine;

[CreateAssetMenu(fileName = "Recruit", menuName = "Scriptable Objects/Recruit")]
public class RecruitConfig : ScriptableObject
{
    [Header("Prices")]
    [SerializeField] private float _unitPrice;

    public float UnitPrice => _unitPrice;
}
