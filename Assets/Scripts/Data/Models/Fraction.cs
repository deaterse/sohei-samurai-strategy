using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Fraction
{
    //Основные переменные фракции
    private string _fractionName;
    private Color _fractionColor;
    private List<GameObject> _fractionMembers = new List<GameObject>();
    private List<Village> _fractionVillages = new List<Village>();
    private List<Army> _fractionArmies = new List<Army>();

    public string FractionName => _fractionName;
    public Color FractionColor => _fractionColor;
    public List<GameObject> FractionMembers => _fractionMembers;
    public List<Village> FractionVillages => _fractionVillages;
    public List<Army> FractionArmies => _fractionArmies;

    public Fraction(string name, Color color)
    {
        _fractionName = name;
        _fractionColor = color;

    }

    public Army CreateArmy()
    {
        Army newArmy = new Army(this, UnityEngine.Random.Range(20, 50));

        _fractionArmies.Add(newArmy);
        Debug.Log($"Создана армия для фракции {this._fractionName}");

        return newArmy;
    }
}
