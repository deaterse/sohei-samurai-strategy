using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance { get; private set; }

    [SerializeField] private MapConfig _config;

    [Header("Villages")]
    [SerializeField] private GameObject villagePrefab;
    [SerializeField] private GameObject linePrefab;

    private GameObject newVillage;
    private Vector2 randomPos;

    public event Action MapGenerated;


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
        Debug.Log("MapGenerator Initialized");

        VillageManager.VillageManagerInitialized += GenerateMap;
    }

    private void GenerateMap()
    {
        VillagesSpawn();

        if (_config.SpawnRoads)
        {
            RoadsSpawn();
        }
    }

    // Spawn Villages
    private void VillagesSpawn()
    {
        int _VillagesCount = _config.VillagesCount;

        List<Village> _villages = new List<Village>();

        for (int i = 0; i < _VillagesCount; i++)
        {
            Vector2 randomPos;
            bool positionValid = false;
            int attempts = 0;

            do
            {
                positionValid = true;

                randomPos = new Vector2(
                    UnityEngine.Random.Range(-_config.MapSize.x / 2, _config.MapSize.x / 2),
                    UnityEngine.Random.Range(-_config.MapSize.y / 2, _config.MapSize.y / 2)
                );

                if (!_config.IsWithinMapBounds(randomPos))
                {
                    positionValid = false;
                    continue;
                }

                foreach (Village existingVillage in _villages)
                {
                    if (Vector2.Distance(randomPos, existingVillage.transform.position) < 5f)
                    {
                        positionValid = false;
                        break;
                    }
                }

                attempts++;
                if (attempts > 500)
                {
                    Debug.LogWarning("Не удалось найти валидную позицию для деревни");
                    positionValid = false;
                    break;
                }

            } while (!positionValid);

            if (positionValid)
            {
                newVillage = Instantiate(villagePrefab, randomPos, Quaternion.identity);
                _villages.Add(newVillage.GetComponent<Village>());

                VillageManager.Instance.SetVillages(_villages);
            }
        }
    }

    //Spawn Roads
    // private void RoadsSpawn()
    // {
    //     var villages = VillageManager.Instance.AllVillages;
    //     for (int i = 0; i < villages.Count - 1; i++)
    //     {
    //         var StartPos = VillageManager.Instance.AllVillages[i].transform.position;
    //         var EndPos = VillageManager.Instance.AllVillages[i + 1].transform.position;

    //         var distance = Vector2.Distance(StartPos, EndPos);

    //         var newLine = Instantiate(linePrefab, StartPos, Quaternion.identity);

    //         var direction = newLine.transform.position - EndPos;

    //         newLine.transform.position = Vector3.Lerp(StartPos, EndPos, 0.5f);
    //         newLine.transform.right = EndPos - newLine.transform.position;
    //         newLine.transform.localScale = new Vector3(distance, newLine.transform.localScale.y, newLine.transform.localScale.z);
    //     }

    //     MapGenerated?.Invoke();
    // }

    private void OnDisable()
    {
        Instance = null;
        VillageManager.VillageManagerInitialized -= GenerateMap; // ← ОТПИСАТЬСЯ!
    }

    private void RoadsSpawn()
    {
        var villages = VillageManager.Instance.AllVillages;

        var connected = new List<Village> { villages[0] };
        var unconnected = new List<Village>(villages.Skip(1));

        while (unconnected.Count > 0)
        {
            Village ClosestA = null;
            Village ClosestB = null;

            var mindistance = float.MaxValue;

            foreach (var connected_village in connected)
            {
                foreach (var unconnected_village in unconnected)
                {
                    float distance = Vector2.Distance(connected_village.transform.position, unconnected_village.transform.position);

                    if (distance < mindistance)
                    {
                        mindistance = distance;
                        ClosestA = connected_village;
                        ClosestB = unconnected_village;
                    }
                }
            }

            if (ClosestA != null && ClosestB != null)
            {
                connected.Add(ClosestB);
                unconnected.Remove(ClosestB);
                BuildRoad(ClosestA.transform.position, ClosestB.transform.position);
            }
        }


        MapGenerated?.Invoke();
    }

    private void BuildRoad(Vector2 StartPos, Vector2 EndPos)
    {
        var distance = Vector2.Distance(StartPos, EndPos);

        var newLine = Instantiate(linePrefab, StartPos, Quaternion.identity);

        var direction = new Vector2(newLine.transform.position.x, newLine.transform.position.y) - EndPos;

        newLine.transform.position = Vector3.Lerp(StartPos, EndPos, 0.5f);
        newLine.transform.right = EndPos - new Vector2(newLine.transform.position.x, newLine.transform.position.y);
        newLine.transform.localScale = new Vector3(distance, newLine.transform.localScale.y, newLine.transform.localScale.z);
    }
}
