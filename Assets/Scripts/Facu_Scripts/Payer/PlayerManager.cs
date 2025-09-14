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

}
