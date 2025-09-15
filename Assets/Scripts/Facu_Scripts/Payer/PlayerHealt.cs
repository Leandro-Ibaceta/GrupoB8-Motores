using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Stats attributes")]
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth = 50f;
 

    private void Start()
    {
        _health = _maxHealth;
    }
    public void TakeDamage(float damage)
    {
        _health -= damage;
        if(_health <= 0)
        {
            //death load checkpoint :D
        }
    }
    public void Heal(float health)
    {
        _health += health;
        _health = Mathf.Clamp(_health, 0, _maxHealth);
    }

}
