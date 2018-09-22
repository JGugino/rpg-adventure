using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {

    #region Inventory Item Class
    [System.Serializable]
    public class InventoryItem{

        public string name;
        public Sprite icon;
        public ItemType type;
        public bool stackable;

        public InventoryItem(string _name, Sprite _icon, ItemType _type, bool _stackable)
        {
            name = _name;
            icon = _icon;
            type = _type;
            stackable = _stackable;
        }

   }
    #endregion

    public static InventoryController instance;

    public List<InventoryItem> inventoryItems;

    private void Awake()
    {
        instance = this;
    }
    
    public void addItem(Item _item)
    {
        string itemName = _item.itemName;
        Sprite itemIcon = _item.icon;
        ItemType itemType = _item.type;
        bool stackable = _item.stackable;

        InventoryItem itemToAdd = new InventoryItem(itemName, itemIcon, itemType, stackable);

        if (!stackable)
        {
            if (!inventoryItems.Contains(itemToAdd))
            {
                inventoryItems.Add(itemToAdd);
                return;
            }
            else if(inventoryItems.Contains(itemToAdd))
            {
                Debug.Log("Item " + itemName + " already exists in the inventory.");
                return;
            }
        }else if (stackable)
        {
            inventoryItems.Add(itemToAdd);
            return;
        }
    }
}
