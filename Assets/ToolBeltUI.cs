using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolBeltUI : MonoBehaviour
{
    [SerializeField] private Image[] _itemSlots = new Image[5];


    private PlayerManager _playerManager;

    private void Start()
    {
        Inventory.instance.onItemChangedCallback += UpdateUI; // Suscribirse al evento de cambio de inventario
      
    }


    public void UpdateUI() // Actualizar la UI del cinturón de herramientas
    {
        int i =0;
        
        foreach (var item in Inventory.instance.Items) // Iterar sobre los items en el inventario
        {
            // Actualizar el sprite del slot con el sprite del item
            _itemSlots[i].sprite = item.Key.UISprite; 
            // Actualizar el texto del slot con la cantidad del item
            _itemSlots[i].gameObject.GetComponentInChildren<TMP_Text>().text = item.Value.ToString(); 
            i++;
        }
    }

}
