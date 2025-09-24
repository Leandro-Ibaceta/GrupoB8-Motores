using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolBeltUI : MonoBehaviour
{
    [SerializeField] private Image[] _itemSlots;


    

    private void Start()
    {
        Inventory.instance.onItemChangedCallback += UpdateUI; // Suscribirse al evento de cambio de inventario
        UpdateUI();
        Inventory.instance.addDefaultItems(); // Agregar items por defecto al inventario    
    }

    public void UpdateUI() // Actualizar la UI del cinturón de herramientas
    {
        
        int i =1;
        _itemSlots = GetComponentsInChildren<Image>(); // Obtener los componentes Image de los slots de items
        foreach (var item in Inventory.instance.Items) // Iterar sobre los items en el inventario
        {
            // Actualizar el sprite del slot con el sprite del item
            _itemSlots[i].sprite = item.Key.UISprite; 
            // Actualizar el texto del slot con la cantidad del item
            _itemSlots[i].gameObject.GetComponentInChildren<TMP_Text>().text = item.Value.ToString(); 
            i++;
        }
    }
    public void OnDestroy()
    {
        if (Inventory.instance != null)
            Inventory.instance.onItemChangedCallback -= UpdateUI; // Desuscribirse del evento al destruir el objeto
    }

}
