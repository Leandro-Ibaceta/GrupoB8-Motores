using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    #region INSPECTOR_ATTRIBUTES
    [SerializeField] private GameObject _playerObject;
    [SerializeField] private int _maxLifes = 3;
    [SerializeField] private GameObject _GFX;
    [SerializeField] private GameObject _gunGFX;
    [SerializeField] private LayerMask _playerLayer;
    #endregion

    #region INTERNAL_ATTRIBUTES
    private PlayerMovement _movement;
    private PlayerStealth _stelth;
    private Player _player;
    private PlayerAttack _attack;
    private PlayerStamina _stamina;
    private cameraMovement _cameraMovement;
    private PlayerAnimation _Animation;
    private Rigidbody _rigidBody;
    private Collider _activeCollider;

    private int _currentLifes;
    #endregion

    #region PROPERTIES
    public Collider ActiveCollider { get { return _activeCollider; } set { _activeCollider = value; } }
    public Rigidbody Rigid_Body => _rigidBody;
    public LayerMask PlayerLayer => _playerLayer;
    public GameObject PlayerObject => _playerObject;
    public int MaxLifes => _maxLifes;
    public int Lifes { get { return _currentLifes; } set { _currentLifes = value; } }
    public GameObject GunGFX => _gunGFX;
    public GameObject GFX => _GFX;
    public PlayerMovement Movement => _movement;
    public PlayerStealth Stelth => _stelth;
    public PlayerAttack Attack => _attack;
    public PlayerStamina Stamina => _stamina;
    public Player Health => _player;
    public cameraMovement CameraMovement => _cameraMovement;
    public PlayerAnimation Animation => _Animation;
    #endregion
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            SetPlayer(PlayerObject);
            DontDestroyOnLoad(gameObject);
            _currentLifes = _maxLifes;
        }
        else
        {
            Destroy(gameObject);
        }
      

    }
    public void SetPlayer(GameObject playerObject)
    {
        _playerObject = playerObject;
        _movement = _playerObject.GetComponent<PlayerMovement>();
        _stelth = _playerObject.GetComponent<PlayerStealth>();
        _Animation = _playerObject.GetComponentInChildren<PlayerAnimation>();
        _cameraMovement = Camera.main.GetComponent<cameraMovement>();
        _attack = _playerObject.GetComponent<PlayerAttack>();
        _stamina = _playerObject.GetComponent<PlayerStamina>();
        _player = _playerObject.GetComponent<Player>();
        _rigidBody = _playerObject.GetComponent<Rigidbody>();
        _GFX = _player.GFX;
        _gunGFX = _player.GunGFX;

    }

    public bool CompareLayer(int layer)
    {
        return (1 << layer & _playerLayer) != 0;
    }

}
