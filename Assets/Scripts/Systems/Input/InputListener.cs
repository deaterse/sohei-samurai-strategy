using UnityEngine;
using UnityEngine.EventSystems;

public class InputListener : MonoBehaviour
{
    public static InputListener Instance { get; private set; }

    private float _mouseDownTime;
    [SerializeField] private float _maxHoldTime = 0.3f;

    public void Init()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;

        Debug.Log("InputListener Initialized");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _mouseDownTime = Time.time;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            float _holdDuration = Time.time - _mouseDownTime;

            if (_holdDuration < _maxHoldTime)
            {
                ProcessClicker.Instance.ProcessClick();
            }
        }
    }
}
