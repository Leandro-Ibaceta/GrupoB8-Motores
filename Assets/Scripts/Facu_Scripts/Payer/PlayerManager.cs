using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerStealth _stelth;
    [SerializeField] private PlayerAttack _attack;
    [SerializeField] private PlayerInputs _inputs;
    [SerializeField] private PlayerStamina _stamina;
    [SerializeField] private PlayerHealth _health;
    [SerializeField] private cameraMovement _cameraMovement;
    [SerializeField] private PlayerAnimation _playerAnimation;

    public PlayerMovement Movement { get { return _movement; } }
    public PlayerStealth Stelth { get { return _stelth; } } 
    public PlayerAttack Attack { get { return _attack; } }
    public PlayerInputs Inputs { get { return _inputs; } }
    public PlayerStamina Stamina { get { return _stamina; } }
    public PlayerHealth Health { get { return _health; } }
    public cameraMovement CameraMovement { get { return _cameraMovement; } }
    public PlayerAnimation PlayerAnimation { get { return _playerAnimation; } }



}
