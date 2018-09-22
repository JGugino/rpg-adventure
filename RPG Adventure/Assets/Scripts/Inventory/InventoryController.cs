using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    private Item equippedHead, equippedChest, equippedLegs, equippedWeapon;

    public GameObject weaponSlots, defenseSlots, keySlots;

    public GameObject weaponSlotPrefab, defenseSlotPrefab, keySlotPrefab;

    public Image headSlot, chestSlot, legsSlot, weaponSlot;

    public Transform weaponParent, defenseParent, keyParent;

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
                //createPrefab(_item);
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

    public void createPrefab()
    {
        foreach(InventoryItem _item in inventoryItems)
        {
            ItemType type = _item.type;

            if ((type == ItemType.Weapon))
            {
                GameObject _created = Instantiate(weaponSlotPrefab, weaponParent);

                TextMeshProUGUI[] texts = _created.GetComponentsInChildren<TextMeshProUGUI>();

               // Weapon w = _item;

                foreach (TextMeshProUGUI t in texts)
                {
                    string objectName = t.name;

                    if (objectName == "Weapon Name Text")
                    {
                       // t.text = _item.itemName;
                    }

                    if (objectName == "Weapon Attack Text")
                    {
                        t.text = "Attack: " + w.damage;
                    }
                }
            }

            if ((type == ItemType.Head) || (type == ItemType.Legs) || (type == ItemType.Legs) || (type == ItemType.Heal))
            {
                GameObject _created = Instantiate(defenseSlotPrefab, defenseParent);
            }

            if ((type == ItemType.KeyItem))
            {
                GameObject _created = Instantiate(keySlotPrefab, keyParent);
            }
        }

    }
}
