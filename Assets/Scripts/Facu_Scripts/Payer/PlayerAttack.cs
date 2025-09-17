using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Attributes")]
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _shootPosition;
    [SerializeField] private float _shootDistance = 10f;
    [SerializeField] private float _throwForce = 100f;
    [SerializeField] private LayerMask _enemyLayer;

    [Header("Prefabs")]
    [SerializeField] private GameObject _SmokeGranadePrefab;
    
    [Header("Inventoty items")]
    [SerializeField] private Item _dart;
    [SerializeField] private Item _smokeGranade;

    private Vector3 _cameraPoint;
    private RaycastHit _hit;
    private PlayerManager _playerManager;
    private Inventory _inventory;
    private GameObject _granade;
    private void Start()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }
        _playerManager = GameObject.FindWithTag("GameManager").GetComponent<PlayerManager>();
        _inventory = Inventory.instance;    
    }
    private void Update()
    {
        // Dispara un dardo si se hace click izquierdo y la camara esta en vista de hombro
        if (_playerManager.Inputs.IsLMBClicked && _playerManager.Movement.OnShoulderCam ) 
        {
            // verifica que haya dardos en el inventario
            if (_inventory.Items.ContainsKey(_dart) && _inventory.Items[_dart]>0)
                Attack();
        }
        // Lanza una granada de humo si se hace click derecho y hay granadas en el inventario
        if (_playerManager.Inputs.IsThrowClicked && _inventory.Items.ContainsKey(_smokeGranade) )
        {
            // verifica que haya granadas en el inventario
            if (_inventory.Items[_smokeGranade] > 0)
                ThrowGranade();
         
        }
    }

    // Lanza una granada de humo desde la posicion de disparo hacia adelante con una fuerza determinada
    private void ThrowGranade()
    {
        _granade=Instantiate(_SmokeGranadePrefab, _shootPosition.position, Quaternion.identity);
        _granade.GetComponent<Rigidbody>().AddForce(_camera.transform.forward * _throwForce);
        Inventory.instance.RemoveItem(_smokeGranade);
    }
    // Dispara un rayo desde la posicion de disparo hacia el punto que mira la camara
    private void Attack()
    {
        _cameraPoint = GetCameraPoint();
        if (Physics.Raycast(_shootPosition.position, (_cameraPoint - _shootPosition.position), out _hit, _shootDistance, _enemyLayer))
        {
            // Si el rayo colisiona con un enemigo, deshabilita al enemigo y remueve un dardo del inventario
            _hit.collider.GetComponentInParent<Enemy>().Dead();
            Inventory.instance.RemoveItem(_dart);
        }
    }


    // Devuelve el punto en el que la camara esta mirando, si no hay colision devuelve Vector3.zero
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
