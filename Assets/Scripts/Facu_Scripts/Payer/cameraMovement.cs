using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class cameraMovement : MonoBehaviour
{
    #region INSPECTOR_ATTRIBUTES

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

    }

    void LateUpdate()
    {


        #region MOUSE_AXIS_PARAMETRIZATION
        _yAxis = _playerManager.Inputs.MouseYAxis;
        _xAxis = _playerManager.Inputs.MouseXAxis;
        #endregion
        #region VERTICAL_CLAMPING
        _verticalReference = Vector3.Angle(_target.up, (transform.position - _target.position).normalized);
        if(_verticalReference < _maxAngleOfCamera/2) 
        {
            _yAxis = Mathf.Clamp(_yAxis, -1, 0);
        }
        else if(_verticalReference >= (90-_maxAngleOfCamera / 2))
        {
            _yAxis = Mathf.Clamp(_yAxis, 0, 1);
        }
        #endregion
        #region SHOULDER_CAM_TO_NORMAL_CAM_TRANSITION && ROTATION_APPLICATION
        if (_playerManager.Inputs.IsRMBClicked)
        {
            transform.localPosition = _startPosition;
            transform.localRotation = _startRotation;
        }
        if (_playerManager.Inputs.IsRMBHeldPressed)
        {
            transform.position = _shoulderCameraPosition.position;
            _target.Rotate(_target.transform.up,_xAxis);
            _camera.fieldOfView = _aimingFOV;

        }
        else if (_playerManager.Inputs.IsRMBReleased)
        {
            transform.localPosition = _startPosition;
            transform.localRotation = _startRotation;
            _camera.fieldOfView = _normalFOV;
        }
       
        else
        {

            transform.LookAt(_target.position);
            transform.RotateAround(_target.position, _target.up, _xAxis);
        }
            transform.RotateAround(_target.position, transform.right, _yAxis);
        #endregion

    }
}
