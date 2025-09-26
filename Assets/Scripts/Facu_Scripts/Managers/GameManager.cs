using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [SerializeField] private Transform _playerSpawnPoint;
   
    


    private Vector3 _playerStartPosition;
    private CheckPointManager _checkPointManager;
    private PlayerManager _playerManager;
    private PlayerInputs _inputs;
    private Inventory _inventory;
    private EnemyManager _enemyManager;
    private UIManager _uiManager;


    public PlayerManager PlayerManager => _playerManager;
    public UIManager UIManager => _uiManager;
    public Inventory Inventory => _inventory;
    public CheckPointManager CheckPointManager => _checkPointManager;
    public PlayerInputs Inputs { get { return _inputs; } }
    public Transform PlayerSpawnPoint => _playerSpawnPoint;
    public EnemyManager EnemyManager { get { return _enemyManager; } set { _enemyManager = value; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            _inputs = GetComponent<PlayerInputs>();
            _inventory = GetComponent<Inventory>();
            _checkPointManager = GetComponent<CheckPointManager>();
            _uiManager = GetComponent<UIManager>();
            _enemyManager = GetComponent<EnemyManager>();
            _playerManager = GetComponent<PlayerManager>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        _playerStartPosition = _playerSpawnPoint.position;
        _playerManager.PlayerObject.transform.position = _playerSpawnPoint.position;
    } 

    public void QuitGame()
    {
        Application.Quit();
    }


    public void RestartGame()
    {
        _inventory.ResetInventory();
        _playerSpawnPoint.position = _playerStartPosition;
        _playerManager.Lifes = _playerManager.MaxLifes;
        LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void LoadCheckpoint()
    {
        LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }


    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        _playerSpawnPoint.position = checkpointPosition;
         
    }

}
