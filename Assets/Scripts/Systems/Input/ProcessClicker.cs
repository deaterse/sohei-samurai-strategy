using UnityEngine;
using System;

public class ProcessClicker : MonoBehaviour
{
    public static ProcessClicker Instance { get; private set; }

    public event Action<Vector2> OnGroundClicked;
    public event Action<Village> OnVillageClicked;

    public void Awake()
    {
        Init();
    }

    public void Init()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    public void ProcessClick()
    {
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(_ray.origin, _ray.direction);

        if (hit == false) return;

        if (hit.collider.CompareTag(Tags.GROUND_TAG))
        {
            Vector2 _clickVector2 = hit.point;
            OnGroundClicked?.Invoke(_clickVector2);
        }
        else if (hit.collider.CompareTag(Tags.VILLAGE_TAG))
        {
            OnVillageClicked?.Invoke(hit.collider.GetComponent<Village>());
        }
        else
        {
            Debug.Log("Captured something else");
        }
    }
}
