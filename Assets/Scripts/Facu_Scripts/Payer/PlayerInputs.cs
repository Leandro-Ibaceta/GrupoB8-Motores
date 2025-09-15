using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    #region INSPECTOR_ATTRIBUTES
    [Header("Sensitivity attributes")]
    [SerializeField][Range(0.1f, 100)] private float _mouseVerticallSensitivity = 10;
    [SerializeField][Range(0.1f, 100)] private float _mouseHorizontalSensitivity = 10;
    [Header("Mouse Axis direction attributes")]
    [SerializeField] private bool _isMouseYAxisInverted = false;
    [SerializeField] private bool _isMouseXAxisInverted = false;
    [Header("Axis direction attributes")]
    [SerializeField] private bool _isYAxisInverted = false;
    [SerializeField] private bool _isXAxisInverted= false;
    [SerializeField] private bool _isLateralAxisInverted = false;

    #endregion
    #region INTERNAL_ATTRIBUTES
    private bool _isRMBClicked;
    private bool _isRMBHeldPressed;
    private bool _isRMBReleased;
    private bool _isLMBClicked;
    private bool _isLMBHeldPressed;
    private bool _isLMBReleased;
    private float _mouseYAxis;
    private float _mouseXAxis;
    private float _yAxis;
    private float _xAxis;
    private float _lateralAxis;
    private float _speedAxis;
    private bool _lowStanceHeldPressed;
    private bool _highStanceHeldPresed;
    private bool _isSprintHeldPressed;
    #endregion
    #region PROPERTIES
    public bool IsLowHeldStancePressed { get { return _lowStanceHeldPressed; } }
    public bool IsHighHeldStancePressed { get { return _highStanceHeldPresed; } }
    public bool IsSprintHeldPressed { get { return _isSprintHeldPressed; } }
    public float MouseYAxis { get { return _mouseYAxis; } }
    public float MouseXAxis { get { return _mouseXAxis; } }
    public float YAxis { get { return _yAxis; } }
    public float XAxis { get { return _xAxis; } }
    public float LateralAxis { get { return _lateralAxis; } }
    public float SpeedAxis { get { return _speedAxis; } }
    public bool IsLateralAxisInverted { get { return _isLateralAxisInverted; } set { _isLateralAxisInverted = value; } }
    public bool IsXAxisInverted { get { return _isXAxisInverted; } set { _isXAxisInverted = value; } }
    public bool IsYAxisInverted { get { return _isYAxisInverted; } set { _isYAxisInverted = value; } }
    public bool IsMouseXAxisInverted { get { return _isMouseXAxisInverted; } set { _isMouseXAxisInverted = value; } }
    public bool IsMouseYAxisInverted { get { return _isMouseYAxisInverted; } set { _isMouseYAxisInverted = value; } }
    public float MouseVerticalSensitivity { get { return _mouseVerticallSensitivity; } set { _mouseVerticallSensitivity = value; } }
    public float MouseHorizontalSensitivity { get { return _mouseHorizontalSensitivity; } set { _mouseHorizontalSensitivity = value; } }
    public bool IsRMBClicked { get { return _isRMBClicked; } }
    public bool IsRMBReleased { get { return _isRMBReleased; } }
    public bool IsLMBClicked { get { return _isLMBClicked; } }
    public bool IsLMBReleased { get { return _isLMBReleased; } }
    public bool IsRMBHeldPressed { get { return _isRMBHeldPressed; } }
    public bool IsLMBHeldPressed { get { return _isLMBHeldPressed; } }
    #endregion

    void Update()
    {
        _mouseYAxis = _isMouseYAxisInverted ? -1 : 1 * -Input.GetAxis("Mouse Y") * _mouseVerticallSensitivity;
        _mouseXAxis = _isMouseXAxisInverted ? -1 : 1 * Input.GetAxis("Mouse X") * _mouseHorizontalSensitivity;
        _isRMBHeldPressed = Input.GetMouseButton(1);
        _isLMBHeldPressed = Input.GetMouseButton(0);
        _yAxis = _isYAxisInverted? -1:1 * Input.GetAxis("Vertical"); 
        _xAxis = _isXAxisInverted? -1:1 * Input.GetAxis("Horizontal");
        _lateralAxis = _isLateralAxisInverted? -1:1 * Input.GetAxis("Horizontal_displacement");
        _speedAxis = Input.GetAxis("Mouse ScrollWheel");
        _lowStanceHeldPressed = Input.GetButton("Crouch");
        _highStanceHeldPresed = Input.GetButton("Jump");
        _isSprintHeldPressed = Input.GetButton("Sprint");
    }
}
