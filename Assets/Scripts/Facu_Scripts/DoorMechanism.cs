using UnityEngine;

public class DoorMechanism : MonoBehaviour
{
    [SerializeField] private Transform _doorTransform;
    [SerializeField] private Vector3 _doorTop;
    [SerializeField] private Item _keyItem;
    [SerializeField] private float _openSpeed = 5f;
    [SerializeField] private float _openedTime = 5f;
    [SerializeField] private float _closeSpeed = 5f;
    [SerializeField] private int _securityLevel;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private LayerMask _enemyLayer;


    private Inventory _playerInventory;
    private bool _isOpen = false;
    private Vector3 _doorHeightOffset;
    private void Start()
    {
        _doorHeightOffset = new Vector3(0, _doorTransform.localScale.y/2, 0);
        _playerInventory = Inventory.instance;
        _doorTop = _doorTransform.position + _doorHeightOffset;
    }


    private void Update()
    {
        if (_isOpen)
        {
            _doorTransform.position = Vector3.MoveTowards(_doorTransform.position, _doorTop, _openSpeed * Time.deltaTime);
        }
        else
        {
            _doorTransform.position = Vector3.MoveTowards(_doorTransform.position, transform.position + _doorHeightOffset, _closeSpeed * Time.deltaTime);
        }
        if (Vector3.Distance(_doorTransform.position, _doorTop) < 0.1f)
        {
            Invoke("CloseDoor", _openedTime);
        }
      
  

    }

    private void CloseDoor()
    {
        _isOpen = false;
    }   


    private void OnTriggerEnter(Collider collision)
    {
        if (((1 << collision.gameObject.layer) & _playerLayer) != 0)
        {
            if (_playerInventory.Items.ContainsKey(_keyItem))
            {
               if(_playerInventory.Items[_keyItem] >= _securityLevel)
                {
                    _isOpen = true;
                }
                else
                {
                    Debug.Log("You need a key with security level: " + _securityLevel);
                }
            }
            else
            {
                Debug.Log("You need a key with security level: " + _securityLevel);
            }
        }
        

    }


}
