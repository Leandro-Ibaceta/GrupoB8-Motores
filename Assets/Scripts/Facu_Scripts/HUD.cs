using UnityEngine;
using UnityEngine.UI;   

public class HUD : MonoBehaviour
{
    [SerializeField] private Slider _staminaValue;
    [SerializeField] private Slider _staminaDepleted;

    private PlayerManager _playerManager;

    private void Start()
    {
        _playerManager = GameObject.FindWithTag("GameManager").GetComponent<PlayerManager>();
        _staminaValue.maxValue = _playerManager.Stamina.MaxStamina;
        _staminaDepleted.maxValue = _playerManager.Stamina.MaxStamina - _playerManager.Stamina.MinStamina;
    }

    private void Update()
    {
        _staminaValue.value = _playerManager.Stamina.ActualStaminaValue;
        _staminaDepleted.value = _playerManager.Stamina.MaxStamina - _playerManager.Stamina.availableStaminaValue;
    }



}
