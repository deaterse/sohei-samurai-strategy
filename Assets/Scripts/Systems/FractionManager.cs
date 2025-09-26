using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class FractionManager
{
    public static FractionManager Instance { get; private set; } //статик для доступа из любого скрипта без ссылки на конкретный фрактионменеджер

    public enum FractionType { Player, Enemy, Neutral }
    public Dictionary<FractionType, Fraction> Fractions { get; private set; }


    private FractionManager() {} // чтобы его нельзя было создавать извне

    public static void Init() // static используется, тк на момент вызова данной функции у нас еще нет экземпляра класса (instance), статик методы принадлежат самому классу и существуют в единичном экземлпяре
    {
        if (Instance != null)
        {
            return;
        }

        Instance = new FractionManager();
        Instance.InitializeFractions();

        Debug.Log("FractionManager Initialized");
    }

    // Упрощенные методы доступа
    public Fraction GetFraction(FractionType type) => Fractions[type];

    public static Fraction Player => Instance.Fractions[FractionType.Player];
    public static Fraction Enemy => Instance.Fractions[FractionType.Enemy];
    public static Fraction Neutral => Instance.Fractions[FractionType.Neutral];

    private void InitializeFractions()
    {

        Fractions = new Dictionary<FractionType, Fraction>
        {
            { FractionType.Player, new Fraction("Player", Color.blue)},
            { FractionType.Enemy, new Fraction("Enemy", Color.red)},
            { FractionType.Neutral, new Fraction("Neutral", Color.white)}
        };
    }
}
