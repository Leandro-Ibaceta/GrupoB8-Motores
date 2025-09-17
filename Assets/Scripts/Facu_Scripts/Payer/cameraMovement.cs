using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Rendering;

public class cameraMovement : MonoBehaviour
{
    #region INSPECTOR_ATTRIBUTES


    public float xForce, yForce = 1000;

    [Header("Axis direction attributes")]
    [SerializeField] private float _maxAngleOfCamera = 45;
    [Header("Camera attributes")]
    [SerializeField][Range(25, 60)] private float _aimingFOV;
    [Header("Cursor attributes")]
    [SerializeField] private CursorLockMode _lockMode = CursorLockMode.Locked;
    [Header("References")]
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _shoulderCameraPosition;
    #endregion
    #region INTERNAL_ATTRIBUTES
    private PlayerManager _playerManager;
    private Vector3 _startPosition;
    private float _normalFOV;
    private float _verticalReference;
    private Quaternion _startRotation;
    private float _xAxis = 0;
    private float _yAxis = 0;
    private Camera _camera;
    private Vector3 _offset;
    private Vector3 _collisionPosition;
    private Rigidbody _rb;
    #endregion

    void Start()
    {
        _startPosition = transform.localPosition;
        _startRotation = transform.localRotation;
        _camera = Camera.main;
        _normalFOV = _camera.fieldOfView;
        Cursor.lockState = _lockMode;
        Input.ResetInputAxes();
        _playerManager = GameObject.FindWithTag("GameManager").GetComponent<PlayerManager>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        #region MOUSE_AXIS_PARAMETRIZATION
        _yAxis = -_playerManager.Inputs.MouseYAxis;
        _xAxis = -_playerManager.Inputs.MouseXAxis;
        #endregion

    }

    void FixedUpdate()
    {


        #region VERTICAL_CLAMPING
        if (_playerManager.Inputs.IsRMBHeldPressed)
        {
            _verticalReference = Vector3.Angle(_shoulderCameraPosition.up, transform.forward);
          
            if (_verticalReference <= 90 - _maxAngleOfCamera )
            {
                _yAxis = Mathf.Clamp(_yAxis, 0, 1);
            }
            else if (_verticalReference >= 90 + _maxAngleOfCamera)
            {
                _yAxis = Mathf.Clamp(_yAxis, -1, 0);
            }
            
        }
        else
        {
            _verticalReference = Vector3.Angle(_target.up, (transform.position - _target.position).normalized);
            if (_verticalReference < _maxAngleOfCamera / 2)
            {
                _yAxis = Mathf.Clamp(_yAxis, -1, 0);
            }
            else if (_verticalReference >= (90 - _maxAngleOfCamera / 2))
            {
                _yAxis = Mathf.Clamp(_yAxis, 0, 1);
            }
        }
            
        #endregion
        #region SHOULDER_CAM_TO_NORMAL_CAM_TRANSITION && ROTATION_APPLICATION
      
        if (_playerManager.Inputs.IsRMBClicked)
        {
            transform.localPosition = _shoulderCameraPosition.localPosition;
            transform.localRotation = _shoulderCameraPosition.localRotation;
        }
        if (_playerManager.Inputs.IsRMBHeldPressed)
        {
            transform.localPosition = _shoulderCameraPosition.localPosition;
            _target.Rotate(_target.up ,  _xAxis);
            _camera.fieldOfView = _aimingFOV;
            transform.RotateAround(transform.position, transform.right, _yAxis);
        }
        else if (_playerManager.Inputs.IsRMBReleased)
        {
            transform.localPosition = _startPosition;
            transform.localRotation = _startRotation;
            _camera.fieldOfView = _normalFOV;
        }
        else
        {
            //transform.RotateAround(_target.position, _target.up, _xAxis);
            //transform.RotateAround(_target.position, transform.right, _yAxis);
           
            _rb.MoveRotation(Quaternion.Slerp(transform.localRotation, 
                Quaternion.LookRotation(_target.position-transform.position),0.1f));
            _rb.AddRelativeForce(Vector3.up * _yAxis * yForce * Time.fixedDeltaTime);
            _rb.AddRelativeForce(Vector3.right  * _xAxis * xForce * Time.fixedDeltaTime);
            //transform.LookAt(_target.position);
        }
        #endregion

    }
}
