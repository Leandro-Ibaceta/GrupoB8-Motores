using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private int _lifes = 3;
   [SerializeField] private GameObject _GFX;
   [SerializeField] private GameObject _gunGFX;

    private PlayerMovement _movement;
    private PlayerStealth _stelth;
    private PlayerHealth _health;
    private PlayerAttack _attack;
    private PlayerInputs _inputs;
    private PlayerStamina _stamina;
    private cameraMovement _cameraMovement;
    private PlayerAnimation _Animation;


    private void Awake()
    {
        _movement = _player.GetComponent<PlayerMovement>();
        _stelth = _player.GetComponent<PlayerStealth>();
        _Animation = _player.GetComponentInChildren<PlayerAnimation>();
        _attack = _player.GetComponent<PlayerAttack>();
        _inputs = GetComponent<PlayerInputs>();
        _stamina = _player.GetComponent<PlayerStamina>();
        _health = _player.GetComponent<PlayerHealth>();
        _cameraMovement = Camera.main.GetComponent<cameraMovement>();
    }

    public GameObject Player => _player;
    public int Lifes { get { return _lifes; } set { _lifes = value; } }
    public GameObject GunGFX => _gunGFX;
    public GameObject GFX => _GFX;
    public PlayerMovement Movement => _movement;
    public PlayerStealth Stelth => _stelth;
    public PlayerAttack Attack => _attack;
    public PlayerInputs Inputs => _inputs;
    public PlayerStamina Stamina => _stamina;
    public PlayerHealth Health => _health;
    public cameraMovement CameraMovement => _cameraMovement;
    public PlayerAnimation Animation => _Animation;



}
