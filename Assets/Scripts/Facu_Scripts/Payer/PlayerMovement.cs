using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum TYPE_OF_MOVEMENT { WITH_LATERAL_DISPLACEMENT, WITHOUT_LATERAL_DISPLACEMENT }

    #region INSPECTOR ATTRIBUTES

    [Header("Displacement attributes")]
    [SerializeField] private float _staminaCost = 5;
    [SerializeField] private TYPE_OF_MOVEMENT _typeOfDisplacement = TYPE_OF_MOVEMENT.WITH_LATERAL_DISPLACEMENT;
    [SerializeField] public float _maxNormalVelocity = 10;
    [SerializeField] public float _maxCrouchVelocity = 4;
    [SerializeField] public float _maxCrawlVelocity = 2;
    [SerializeField] public float _maxSprintVelocity = 25;
    [SerializeField] public float _minNormalVelocity = 1;
    [SerializeField][Range(0, 100)] private float _scrollWheelAceleration = 10;
    [SerializeField] private float _maxForceApplied = 500;
    [SerializeField] private float _minForceApplied = 150;
    [Header("Rotation attributes")]
    [SerializeField][Range(0, 360)] private float _maxAngularSpeed = 180;
    [SerializeField][Range(0, 360)] private float _minAngularSpeed = 25;
    [Header("Animations attributes")]
    [SerializeField][Range(0, 2)] private float _stanceChangeMultiplier = 0.5f;
    [SerializeField] private Transform playerGFX;

    [Header("Colliders references")]
    [SerializeField] private CapsuleCollider _walkCollider;
    [SerializeField] private CapsuleCollider _crouchCollider;
    [SerializeField] private CapsuleCollider _crawlCollider;



    #endregion
    #region INTERNAL_ATTRIBUTES
    private int _stanceStep = 0;
    private bool _haveStamina = true;
    private bool _isRuning = false;
    private float _forceVectorMagnitude;
    private float _relativeSpeed;
    private float _angularSpeed;
    private float _rotationAngle;
    private float _maxVelocity;
    private float _speedInterpolation;
    private Vector3 _moveV;
    private Vector3 _moveH;
    private Vector3 _forceVector;
    private Quaternion _rotation = Quaternion.identity;
    private bool _onShoulderCam = false;
    private Rigidbody _rb;
    private PlayerManager _playerManager;
    #endregion
    #region PROPERTIES
    public float Speed
    {
        get { return _maxForceApplied; }
        set { _maxForceApplied = value < 0 ? 0 : value; }
    }
    public bool OnShoulderCam
    {
        get { return _onShoulderCam; }
    }

    public bool HaveStamina
    {
        set { _haveStamina = value; }
    }
    #endregion


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _maxVelocity = _maxNormalVelocity;
        _rb.maxLinearVelocity = _maxVelocity;
        _playerManager = GameObject.FindWithTag("GameManager").GetComponent<PlayerManager>();
    }

    void Update()
    {
        #region FORCE_MAGNITUDE
        _speedInterpolation += (_playerManager.Inputs.SpeedAxis * _scrollWheelAceleration * Time.deltaTime);
        _speedInterpolation = Mathf.Clamp(_speedInterpolation, 0f, 1f);

        _forceVectorMagnitude = Mathf.Lerp(_minForceApplied, _maxForceApplied, _speedInterpolation);
        _angularSpeed = Mathf.Lerp(_maxAngularSpeed, _minAngularSpeed, _speedInterpolation);
        #endregion
        #region LATERAL_ROTATION
        if (!_playerManager.Inputs.IsRMBHeldPressed)
        {
            _onShoulderCam = false;
            _rotationAngle = _playerManager.Inputs.XAxis * _angularSpeed * Time.deltaTime;
            _rotation = transform.rotation * Quaternion.Euler(0, _rotationAngle, 0);
        }
        else
        {
            _onShoulderCam = true;
        }
        #endregion
        #region LATERAL_DISPLACEMENT
        if (_typeOfDisplacement == TYPE_OF_MOVEMENT.WITH_LATERAL_DISPLACEMENT)
        {
            _moveH = transform.right * _playerManager.Inputs.XAxis;

            if (_forceVector != Vector3.zero && !_onShoulderCam)
                playerGFX.localRotation = Quaternion.Lerp(playerGFX.localRotation, Quaternion.LookRotation(_forceVector), _angularSpeed * Time.deltaTime);
            else if (OnShoulderCam)
                playerGFX.localRotation = Quaternion.identity;
           
        }
        else
        {
            _moveH = transform.right * _playerManager.Inputs.LateralAxis;
        }
        #endregion
        #region FORCE_VECTOR_CALCULATION
        _moveV = transform.forward * _playerManager.Inputs.YAxis;
        _forceVector = (_moveH + _moveV).normalized * _forceVectorMagnitude * Time.deltaTime;
        #endregion
        #region STANCE_MODIFICATION_&_COLLISION
        if (_playerManager.Inputs.IsSprintHeldPressed && _haveStamina)
        { 
            _maxVelocity = _maxSprintVelocity;
            _stanceStep = 0;
            _isRuning = true;
            _playerManager.Stamina.DrainStamina();
        }
        else
        {
            _isRuning = false;
            if (_playerManager.Inputs.IsLowStancePressed)
            {
                _stanceStep++;
            }
            if (_playerManager.Inputs.IsHighStancePressed)
            {
                _stanceStep--;
            }
            _stanceStep = math.clamp(_stanceStep,0, 3);

        }
        switch (_stanceStep)
        {
            case 0:
                _playerManager.PlayerAnimation.ChangeAnimationSpeed(1);
                _maxVelocity = _maxNormalVelocity;
                _walkCollider.enabled = true;
                _crawlCollider.enabled = false;
                _crouchCollider.enabled = false;
                break;
            case 1:
                _maxVelocity = _maxCrouchVelocity;
                _crouchCollider.enabled = true;
                _walkCollider.enabled = false;
                _crawlCollider.enabled = false;
                break;
            case 2:
                _maxVelocity = _maxCrawlVelocity;
                _crawlCollider.enabled = true;
                _crouchCollider.enabled = false;
                _walkCollider.enabled = false;
                break;

        }
        _rb.maxLinearVelocity = _maxVelocity;
        _playerManager.PlayerAnimation.ChangeStanceValue(_stanceStep);

        #endregion

    }

    void FixedUpdate()
    {

        _rb.AddForce(_forceVector );
        if (!_onShoulderCam && _typeOfDisplacement == TYPE_OF_MOVEMENT.WITHOUT_LATERAL_DISPLACEMENT)
            _rb.MoveRotation(_rotation);
        _relativeSpeed = (_rb.linearVelocity.magnitude) / _maxVelocity;
        if (_isRuning)
            _playerManager.PlayerAnimation.ChangePlayerSpeed(Mathf.Lerp(-1,1, _relativeSpeed));
        else if(_stanceStep == 0)
            _playerManager.PlayerAnimation.ChangePlayerSpeed(Mathf.Lerp(-1, 0, _relativeSpeed));
        else
            _playerManager.PlayerAnimation.ChangeAnimationSpeed(Mathf.Lerp(0, 1, _relativeSpeed));



    }

}
