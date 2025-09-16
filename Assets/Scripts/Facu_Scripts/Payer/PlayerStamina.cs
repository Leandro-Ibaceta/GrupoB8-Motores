using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    #region INSPECTOR_ATTRIBUTES
    [Header("Stamina Attributes")]
    [SerializeField] private float _staminaCost = 1f;
    [SerializeField] private float _maxStamina = 10f;
    [SerializeField] private float _minStamina = 3f;
    [SerializeField] private float _maxStaminaDrainFactor = 0.5f;
    [SerializeField] private float _staminaRecoveryFactor = 0.5f;
    [SerializeField] private float _tiredCooldown = 1f;
    #endregion
    #region INTERNAL_ATTRIBUTES
    private PlayerManager _playerManager;
    private bool _isCoolingDown;
    private float _availableStamina;
    private float _actualStamina;
    #endregion
    #region PROPERTIES

    public float MaxStamina { get { return _maxStamina; } }
    public float MinStamina { get { return _minStamina; } }
    public float ActualStaminaValue { get { return _actualStamina; } }
    public float availableStaminaValue { get { return _availableStamina; } }


    protected float AvailableStamina // esta propieda permite modificar los valores y clampearlos al maximo y al minimo
    {
        get { return _availableStamina; }
        set { _availableStamina = Mathf.Clamp(_availableStamina + value, _minStamina, _maxStamina); }
    }
    protected float ActualStamina // esta propieda permite modificar los valores y clampearlos al maximo y al minimo
    {
        get { return _actualStamina; }
        set { _actualStamina = Mathf.Clamp(_actualStamina + value, 0, _availableStamina); }
    }
    #endregion
    private void Start()
    {
        _availableStamina = _maxStamina;
        _actualStamina = _availableStamina;
        _isCoolingDown = false;
        _playerManager = GameObject.FindWithTag("GameManager").GetComponent<PlayerManager>();
    }

    private void Update()
    {
        if(ActualStamina < AvailableStamina)
            ActualStamina = _staminaRecoveryFactor * Time.deltaTime;
    }
    public void DrainStamina(float Time)
    {
        if (_isCoolingDown) return;


        ActualStamina = -_staminaCost * Time;
        if (_actualStamina <= 0)
        {
            _playerManager.Movement.enabled = false;
            //Animacion de cansansio
            Invoke("RestoreMovement", _tiredCooldown);
        }
        AvailableStamina = - _maxStaminaDrainFactor * Time;
        if (_availableStamina <= _minStamina)
        {
            _playerManager.Movement.HaveStamina = false;
        }

    }

    public void RestoreAvailableStamina(float stamina)
    {
        ActualStamina = stamina;
    }

    private void RestoreMovement()
    {
        _playerManager.Movement.enabled = true;
        _isCoolingDown = false;
    }
    

}
