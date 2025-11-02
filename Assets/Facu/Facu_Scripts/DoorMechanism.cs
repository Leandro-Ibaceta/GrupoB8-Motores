using UnityEngine;

public class DoorMechanism : MonoBehaviour
{
    [Header("Door Attributes")]
    [SerializeField] private float _doorheigt;
    [Header("Key Attributes")]
    [SerializeField] private Item _keyItem;
    [SerializeField] private int _securityLevel;
    [Header("Door Movement Attributes")]
    [SerializeField] private float _openedTime = 5f;
    [SerializeField] private float _openSpeed = 5f;
    [SerializeField] private float _closeSpeed = 5f;
    [Header("Player Layer")]
    [SerializeField] private LayerMask _detectionLayer;

    [Header("Audio Settings")] // 🔊 NUEVO
    [SerializeField] private AudioSource doorAudioSource;
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;

    private Inventory _playerInventory;
    private UIManager _uiManager;
    private bool _isOpen = false;
    private Vector3 _doorHeightOffset;
    private Vector3 _startPosition;
    private bool _hasPlayedOpenSound = false; // 🔊 NUEVO

    private void Start()
    {

        _doorHeightOffset = transform.position + transform.up * _doorheigt;
        _playerInventory = GameManager.instance.Inventory;
        _startPosition = transform.position;
        _uiManager = GameManager.instance.UIManager;

    }


    private void Update()
    {
        // mueve la puerta hacia arriba si esta abierta, o hacia abajo si esta cerrada
        if (_isOpen)
        {
            transform.parent.position = Vector3.MoveTowards(transform.position, _doorHeightOffset, _openSpeed * Time.deltaTime);

            // 🔊 reproducir sonido una sola vez al abrir
            if (!_hasPlayedOpenSound && doorAudioSource != null && openSound != null)
            {
                doorAudioSource.PlayOneShot(openSound);
                _hasPlayedOpenSound = true;
                Debug.Log("[DOOR] Puerta abierta, reproduciendo sonido");
            }
        }
        else
        {
            transform.parent.position = Vector3.MoveTowards(transform.position, _startPosition, _closeSpeed * Time.deltaTime);
        }

        // cuando termina de abrirse, iniciar temporizador de cierre
        if (Vector3.Distance(transform.position, _doorHeightOffset) < 0.1f)
        {
            Invoke("CloseDoor", _openedTime);
        }


    }

    private void CloseDoor()
    {
        _isOpen = false;

        // 🔊 sonido de cierre opcional
        if (doorAudioSource != null && closeSound != null)
        {
            doorAudioSource.PlayOneShot(closeSound);
        }

        _hasPlayedOpenSound = false; // 🔊 permitir reproducir otra vez al volver a abrir
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("[DOOR] Trigger detectado con " + collision.gameObject.name);
        if (((1 << collision.gameObject.layer) & _detectionLayer) != 0)
        {

             _isOpen = true;
        Debug.Log("[DOOR] Forzando apertura de prueba");
          
            if (_playerInventory.Items.ContainsKey(_keyItem))
            {
                if (_playerInventory.Items[_keyItem] >= _securityLevel)
                {
                    _isOpen = true;
                }
                else
                {
                    _uiManager.PopUpMessageTimed("You need a key with security level: " + _securityLevel);
                }
            }
            else
            {
                if (_securityLevel == 0)
                {
                    _isOpen = true;
                    return;
                }
                _uiManager.PopUpMessageTimed("You need a key with security level: " + _securityLevel);
            }
        }
    }
}
