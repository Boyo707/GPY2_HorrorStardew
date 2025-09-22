using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public Crop cropPrefab;
    public bool isSeed;
    public Item harvestProduct;
}
