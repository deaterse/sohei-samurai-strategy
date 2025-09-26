using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public void Init()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;

        Debug.Log("GameManager Initialized");
    }

    private void Update()
    {
        EconomyManager.Instance?.Update(Time.deltaTime);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            StartCoroutine(InitializeComponents());
        }
    }

    private IEnumerator InitializeComponents()
    {
        yield return new WaitUntil(() => VillageManager.Instance != null &&
                                    MapGenerator.Instance != null);

        MapGenerator.Instance.Init();
        yield return null;
        VillageManager.Instance.Init();
        yield return null;
        VillageManager.Instance.SubscribeToInput();
        yield return null;
        yield return PlayerFractionCoroutine();
    }


    IEnumerator PlayerFractionCoroutine()
    {
        while (PlayerArmyController.Instance == null)
        {
            yield return null;
        }

        DoPlayerFraction();
    }

    private void DoPlayerFraction()
    {
        Fraction playerFraction = FractionManager.Instance.GetFraction(FractionManager.FractionType.Player);
        Army playerArmyData = playerFraction.CreateArmy();

        PlayerArmyController.Instance.Init(playerArmyData);
        EconomyManager.Instance.RegisterArmy(playerArmyData);
    }
}
