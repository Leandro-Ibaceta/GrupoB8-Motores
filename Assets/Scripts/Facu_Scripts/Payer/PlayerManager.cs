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
    [SerializeField] private PlayerAnimation _Animation;


    public PlayerMovement Movement => _movement;
    public PlayerStealth Stelth => _stelth;
    public PlayerAttack Attack => _attack;
    public PlayerInputs Inputs => _inputs;
    public PlayerStamina Stamina => _stamina;
    public PlayerHealth Health => _health;
    public cameraMovement CameraMovement => _cameraMovement;
    public PlayerAnimation Animation => _Animation;



}
