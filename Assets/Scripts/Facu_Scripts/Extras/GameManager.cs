using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [SerializeField] private Transform _playerSpawnPoint;

    private Vector3 _playerStartPosition;
    private PlayerManager _playerManager;


    public Transform PlayerSpawnPoint => _playerSpawnPoint;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
        _playerManager =  PlayerManager.instance;
        _playerManager.PlayerObject.transform.position = _playerSpawnPoint.position;
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void RestartGame()
    {
        Inventory.instance = new Inventory();
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
