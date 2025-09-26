using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [Header("Managers Prefabs")]
    [SerializeField] private GameManager _gameManagerPrefab;
    [SerializeField] private EnemyManager _enemyManagerPrefab;
    [SerializeField] private InputListener _inputListenerPrefab;

    [Header("Settings")]
    [SerializeField] private string _nextSceneName;

    private void Awake()
    {
        if (FindObjectOfType<GameManager>() == null)
        {
            InitializeManagers();
        }

        SceneManager.LoadScene(_nextSceneName);
    }

    private void InitializeManagers()
    {
        FractionManager.Init();
        EconomyManager.Init();

        GameManager _gameManager = Instantiate(_gameManagerPrefab);
        EnemyManager _enemyManager = Instantiate(_enemyManagerPrefab);
        InputListener _inputListener = Instantiate(_inputListenerPrefab);

        _gameManager.Init();
        _enemyManager.Init();
        _inputListener.Init();

        DontDestroyOnLoad(_gameManager);
        DontDestroyOnLoad(_enemyManager);
        DontDestroyOnLoad(_inputListener);

        _gameManager.name = "GameManager";
        _enemyManager.name = "EnemyManager";
        _inputListener.name = "InputListener";
    }
}
