using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Header("Item to pickup reference")]
    [SerializeField] private Item item;


    private Inventory _inventory;
    private LayerMask _playerLayer;
    private CheckPointManager _pickUpsManager;


    private void Start()
    {
        _pickUpsManager = CheckPointManager.instance;
        _inventory = Inventory.instance;
       _playerLayer = PlayerManager.instance.PlayerLayer;
    }


    // Si el objeto que entra en el trigger es el jugador,
    // intenta agregar el item al inventario y destruye el objeto si se agrega con exito
    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & _playerLayer) != 0)
        {
            if(_inventory.AddItem(item))
            {
                _pickUpsManager.DeletePickUp(gameObject.name);
            }
        }
    }
}
