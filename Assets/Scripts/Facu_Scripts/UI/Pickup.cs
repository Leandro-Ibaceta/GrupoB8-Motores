using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Header("Item to pickup reference")]
    [SerializeField] private Item item;
    [Header("Player Layer")]
    [SerializeField] private LayerMask _playerLayer;

    private Inventory _inventory;
    private void Start()
    {
       _inventory = Inventory.instance;
    }


    // Si el objeto que entra en el trigger es el jugador,
    // intenta agregar el item al inventario y destruye el objeto si se agrega con exito
    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & _playerLayer) != 0)
        {
            if(_inventory.AddItem(item))
            {
                Destroy(gameObject);
            }
        }
    }
}
