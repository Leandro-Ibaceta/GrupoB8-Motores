using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [Header("Sensitivity attributes")]
    [SerializeField][Range(0.1f, 100)] private float _mouseVerticallSensitivity = 10;
    [SerializeField][Range(0.1f, 100)] private float _mouseHorizontalSensitivity = 10;
    [Header("Axis direction attributes")]
    [SerializeField] private bool yAxisInverted = false;
    [SerializeField] private bool xAxisInverted= false;
    [SerializeField] private bool lateralAxisInverted = false;
    [SerializeField] private bool _mouseYAxisInverted = false;
    [SerializeField] private bool _mouseXAxisInverted = false;

    private bool _RMBPressed;
    private bool _LMBPressed;
    private float _mouseYAxis;
    private float _mouseXAxis;
    private float _yAxis;
    private float _xAxis;
    private float _lateralAxis;
    private float _speedAxis;
    private bool _lowStance;
    private bool _highStance;
    private bool _sprint;


    // Update is called once per frame
    void Update()
    {
        _mouseYAxis = _mouseYAxisInverted ? -1 : 1 * -Input.GetAxis("Mouse Y") * _mouseVerticallSensitivity;
        _mouseXAxis = _mouseXAxisInverted ? -1 : 1 * Input.GetAxis("Mouse X") * _mouseHorizontalSensitivity;
        _RMBPressed = Input.GetMouseButton(1);
        _yAxis = yAxisInverted? -1:1 * Input.GetAxis("Vertical"); 
        _xAxis = xAxisInverted? -1:1 * Input.GetAxis("Horizontal");
        _lateralAxis = lateralAxisInverted? -1:1 * Input.GetAxis("Horizontal_displacement");
        _speedAxis = Input.GetAxis("Mouse ScrollWheel");
        _lowStance = Input.GetButton("Crouch");
        _highStance = Input.GetButton("Jump");
        _sprint = Input.GetButton("Sprint");
    }
}
