using UnityEngine;

[CreateAssetMenu(fileName = "newItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("Item Attributes")]
    [SerializeField] private new string name;
    [SerializeField] private int _maxStack = 1;
    [Header("UI Attributes")]
    [SerializeField] private Sprite _UISprite;


    public int MaxStack => _maxStack;
    public string ItemName => name;
    public Sprite UISprite => _UISprite;
    

}
