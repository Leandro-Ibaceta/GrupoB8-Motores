using UnityEngine;

public class HitDetection : MonoBehaviour
{
    [Header("Hit Detection attributes")]
    [SerializeField] private float _damage = 25f;
    [SerializeField] private LayerMask _playerLayer;

    private PlayerManager _playerManager;

    private void Start()
    {
        _playerManager = GameObject.FindWithTag("GameManager").GetComponent<PlayerManager>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        // verifica si el objeto que colisiona es el jugador
        if ((1 << other.gameObject.layer & _playerLayer) != 0)
        {
            // aplica el daño al jugador
            _playerManager.Health.TakeDamage(_damage);
        }
    }

}
