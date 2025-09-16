using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Attributes")]
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _shootPosition;
    [SerializeField] private float _shootDistance = 10f;
    [SerializeField] private LayerMask _enemyLayer;

    private Vector3 _cameraPoint;
    private RaycastHit _hit;
    private PlayerManager _playerManager;

    private void Start()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }
        _playerManager = GameObject.FindWithTag("GameManager").GetComponent<PlayerManager>();
    }
    private void Update()
    {
        if (_playerManager.Inputs.IsLMBClicked && _playerManager.Movement.OnShoulderCam)
        {
            Attack();
        }
    }

    private void Attack()
    {
        _cameraPoint = GetCameraPoint();
        if (Physics.Raycast(_shootPosition.position, (_cameraPoint - _shootPosition.position), out _hit, _shootDistance, _enemyLayer))
        {
            _hit.collider.GetComponent<Enemy>().Disable();
        }
    }

    Vector3 GetCameraPoint()
    {
        RaycastHit hit;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit))
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
