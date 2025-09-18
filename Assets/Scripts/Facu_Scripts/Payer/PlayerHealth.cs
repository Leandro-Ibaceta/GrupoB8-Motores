using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Stats attributes")]
    [SerializeField] private float _health = 100;
    [SerializeField] private float _maxHealth = 50f;
    
    private PlayerManager _playerManager;

    public float HealthValue => _health;
    public float MaxHealth => _maxHealth;

    private void Start()
    {
        _health = _maxHealth;
        _playerManager =
        _playerManager = GameObject.FindWithTag("GameManager").GetComponent<PlayerManager>();
    }


    //solo para debug

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(10);
        }
    }


    public void TakeDamage(float damage)
    {
        _health -= damage;
        if(_health <= 0)
        {
            if(_playerManager.Lifes>=0)
            {
                _playerManager.Lifes--;
                _playerManager.Health.Heal(_maxHealth);
                GameManager.instance.LoadCheckpoint();
            }
            else
            {
                GameManager.instance.RestartGame();
            }
        }
    }
    public void Heal(float health)
    {
        _health += health;
        _health = Mathf.Clamp(_health, 0, _maxHealth);
    }

}
