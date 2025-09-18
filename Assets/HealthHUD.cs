using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour
{
    [Header("Health HUD References")]
    [SerializeField] private Slider _healthValue;


    private PlayerManager _playerManager;


    private void Start()
    {
        _playerManager = GameObject.FindWithTag("GameManager").GetComponent<PlayerManager>();
        // setea los valores maximos de las barras de stamina
        _healthValue.maxValue = _playerManager.Health.MaxHealth;
  
    }


    private void Update()
    {
        _healthValue.value = _playerManager.Health.HealthValue;
    }


}
