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
    [SerializeField] private bool _isXAxisInverted = false;
    [SerializeField] private bool _isLateralAxisInverted = false;

    #endregion
    #region INTERNAL_ATTRIBUTES
    private bool _isHabilityClicked;
    private bool _isHabilityHeldPressd;
    private bool _isHabilityRelead;
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
    private bool _lowStancePressed;
    private bool _highStancePresed;
    private bool _isSprintHeldPressed;
    private bool _isThrowClicked;
    #endregion
    #region PROPERTIES

    public bool IsHabilityClicked => _isHabilityClicked;
    public bool IsHabilityHeldPressed => _isHabilityHeldPressd;
    public bool IsHabilityReleased => _isHabilityRelead;
    public bool IsThrowClicked => _isThrowClicked;
    public bool IsLowStancePressed => _lowStancePressed;
    public bool IsHighStancePressed => _highStancePresed;
    public bool IsSprintHeldPressed => _isSprintHeldPressed;
    public float MouseYAxis => _mouseYAxis; 
    public float MouseXAxis => _mouseXAxis;
    public float YAxis => _yAxis;
    public float XAxis => _xAxis;
    public float LateralAxis => _lateralAxis;
    public float SpeedAxis => _speedAxis;
    public bool IsRMBClicked => _isRMBClicked;
    public bool IsRMBReleased => _isRMBReleased;
    public bool IsLMBClicked => _isLMBClicked;
    public bool IsLMBReleased => _isLMBReleased;
    public bool IsRMBHeldPressed => _isRMBHeldPressed;
    public bool IsLMBHeldPressed => _isLMBHeldPressed;
    public bool IsLateralAxisInverted { get { return _isLateralAxisInverted; } set { _isLateralAxisInverted = value; } }
    public bool IsXAxisInverted { get { return _isXAxisInverted; } set { _isXAxisInverted = value; } }
    public bool IsYAxisInverted { get { return _isYAxisInverted; } set { _isYAxisInverted = value; } }
    public bool IsMouseXAxisInverted { get { return _isMouseXAxisInverted; } set { _isMouseXAxisInverted = value; } }
    public bool IsMouseYAxisInverted { get { return _isMouseYAxisInverted; } set { _isMouseYAxisInverted = value; } }
    public float MouseVerticalSensitivity { get { return _mouseVerticallSensitivity; } set { _mouseVerticallSensitivity = value; } }
    public float MouseHorizontalSensitivity { get { return _mouseHorizontalSensitivity; } set { _mouseHorizontalSensitivity = value; } }
    #endregion

    // Actualiza los inputs cada frame
    void Update()
    {
        // Obtiene los ejes del mouse , aplicando la sensibilidad y la inversion segun las configuraciones
        _mouseYAxis = (_isMouseYAxisInverted ? -1 : 1) * (-Input.GetAxis("Mouse Y") * _mouseVerticallSensitivity);
        _mouseXAxis = (_isMouseXAxisInverted ? -1 : 1) * (Input.GetAxis("Mouse X") * _mouseHorizontalSensitivity);

        // Obtiene los estados de los botones del mouse
        _isRMBClicked = Input.GetMouseButtonDown(1);
        _isRMBHeldPressed = Input.GetMouseButton(1);
        _isRMBReleased = Input.GetMouseButtonUp(1);
        _isLMBClicked = Input.GetMouseButtonDown(0);
        _isLMBHeldPressed = Input.GetMouseButton(0);
        _isLMBReleased = Input.GetMouseButtonUp(0);

        // Obtiene los ejes de movimiento , aplicando la inversion segun las configuraciones
        _yAxis = (_isYAxisInverted? -1:1) * Input.GetAxis("Vertical"); 
        _xAxis = (_isXAxisInverted? -1:1) * Input.GetAxis("Horizontal");
        _lateralAxis = _isLateralAxisInverted? -1:1 * Input.GetAxis("Horizontal_displacement");

        // Obtiene el eje de velocidad (scroll del mouse)
        _speedAxis = Input.GetAxis("Mouse ScrollWheel");

        // Obtiene los estados de los botones de postura , sprint y lanzar
        _lowStancePressed = Input.GetButtonDown("Crouch");
        _highStancePresed = Input.GetButtonDown("Jump");
        _isSprintHeldPressed = Input.GetButton("Sprint");
        _isThrowClicked = Input.GetButtonDown("Throw");

        // Obtiene el estado del boton de habilidad
        _isHabilityClicked = Input.GetButton("Interact");
        _isHabilityHeldPressd = Input.GetButtonDown("Interact");
        _isHabilityRelead = Input.GetButtonUp("Interact");


    }
}
