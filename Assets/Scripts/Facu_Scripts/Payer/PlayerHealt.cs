using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Stats attributes")]
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth = 50f;
    [SerializeField] private float _maxStamina= 10f;
    [SerializeField] private float _minStamina= 3f;
    [SerializeField] private float _maxStaminaDrainFactor = 0.5f;
    [SerializeField] private float _staminaRecoveryFactor = 0.5f;
    [SerializeField] private float _tiredCooldown = 0.5f;

    [Header("Attack Attributes")]
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _shootPosition;
    [SerializeField] private float _shootDistance = 10f;
    [SerializeField] private LayerMask _enemyLayer;

    private bool _isCoolingDown;
    private float _availableStamina;
    private float _actualStamina;
    private PlayerMovement _playerMovement;
    private Vector3 _cameraPoint;
    private RaycastHit _hit;

    protected float AvailableStamina // esta propieda permite modificar los valores y clampearlos al maximo y al minimo
    {
        get { return _availableStamina; }
        set {  _availableStamina = Mathf.Clamp(_availableStamina+ value,_minStamina,_maxStamina); }
    }
    protected float ActualStamina // esta propieda permite modificar los valores y clampearlos al maximo y al minimo
    {
        get { return _actualStamina; }
        set { _actualStamina = Mathf.Clamp(_actualStamina + value, _minStamina, _availableStamina); }
    }


    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _health = _maxHealth;
        _availableStamina = _maxStamina;
        _actualStamina = _availableStamina;
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && _playerMovement.OnShoulderCam)
        {
            Attack();
        }
    }

    public void DrainStamina(float staminaCost)
    {
        if (_isCoolingDown) return;


        ActualStamina = -staminaCost;
        if(_actualStamina <= _minStamina)
        {
            _playerMovement.enabled = false;
            //Animacion de cansansio
            Invoke("RestoreMovement", _tiredCooldown);
        }
        ActualStamina = -(_availableStamina - _actualStamina) * _maxStaminaDrainFactor;
        if (_availableStamina <= _minStamina)
        {
            _playerMovement.HaveStamina = false;
        }

    }

    public void RestoreAvailableStamina( float stamina)
    {
        ActualStamina = stamina;
    }


    public void TakeDamage(float damage)
    {
        _health-=damage;
        if(_health<=0)
        {
            //death load checkpoint :D
        }

    }
    public void Heal(float health)
    {
        _health+=health;
        _health = Mathf.Clamp(_health, 0, _maxHealth);
    }
    private void Attack()
    {
        _cameraPoint = GetCameraPoint();
        if (Physics.Raycast(_shootPosition.position, (_cameraPoint - _shootPosition.position), out _hit, _shootDistance, _enemyLayer))
        {
            _hit.collider.GetComponent<Enemy>().Disable();
        }
    }

    private void RestoreMovement()
    {
        _playerMovement.enabled = true;
        _isCoolingDown = false;
    }
    Vector3 GetCameraPoint()
    {
        RaycastHit hit;
        if(Physics.Raycast(_camera.transform.position,_camera.transform.forward,out hit))
        {
            Debug.DrawLine(_shootPosition.position, hit.point);
            return hit.point; 
        }
        else
        {
            return Vector3.zero;
        }
    }

}
