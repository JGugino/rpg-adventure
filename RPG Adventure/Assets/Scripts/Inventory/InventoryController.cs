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
        public int damage = 0;
        public int defense = 0;

        public InventoryItem(string _name, Sprite _icon, int _damage, int _defense, ItemType _type, bool _stackable)
        {
            name = _name;
            icon = _icon;
            type = _type;
            stackable = _stackable;

            if (_damage >= 0)
            {
                damage = _damage;
            }

            if (_defense >= 0)
            {
                defense = _defense;
            }
        }

   }
    #endregion

    public static InventoryController instance;

    public List<InventoryItem> inventoryItems;

    private Item equippedHead, equippedChest, equippedLegs, equippedWeapon;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GUIController.instance.findInventoryUI();
    }

    public void addItem(Item _item, Weapon _weapon = null, Armor _armor = null)
    {
        string itemName = _item.itemName;
        Sprite itemIcon = _item.icon;

        int damage = 0;

        int defense = 0;

        if (_weapon != null)
        {
            damage = _weapon.damage;
        }

        if (_armor != null)
        {
            defense  = _armor.defense;
        }

        ItemType itemType = _item.type;
        bool stackable = _item.stackable;

        InventoryItem itemToAdd = new InventoryItem(itemName, itemIcon, damage, defense, itemType, stackable);

        if (!inventoryItems.Contains(itemToAdd))
        {
            inventoryItems.Add(itemToAdd);
            return;
        }
        else if (inventoryItems.Contains(itemToAdd))
        {
            Debug.Log("Item " + itemName + " already exists in the inventory.");
            return;
        }
    }

    public void createPrefab()
    {
        foreach (InventoryItem _item in inventoryItems)
        {
            ItemType type = _item.type;

            Debug.Log("Current Item: " + _item.name + ", Type: " + _item.type);

            if ((type == ItemType.Weapon))
            {
                GUIController.instance.weaponSlots.SetActive(true);

                if (!GameObject.Find(_item.name))
                {
                    GameObject _created = Instantiate(GUIController.instance.weaponSlotPrefab, GUIController.instance.weaponParent);

                    _created.name = _item.name;

                    TextMeshProUGUI[] texts = _created.GetComponentsInChildren<TextMeshProUGUI>();

                    Image[] images = _created.GetComponentsInChildren<Image>();

                    foreach (Image i in images) {
                        string objectName = i.name;

                        if (objectName == "Weapon Item Icon")
                        {
                            i.sprite = _item.icon;
                        }
                    }

                    foreach (TextMeshProUGUI t in texts)
                    {
                        string objectName = t.name;

                        if (objectName == "Weapon Name Text")
                        {
                            t.text = _item.name;
                        }

                        if (objectName == "Weapon Attack Text")
                        {
                            t.text = "Attack: " + _item.damage;
                        }
                    }
                }
            }

            if ((type == ItemType.Head) || (type == ItemType.Chest) || (type == ItemType.Legs))
            {
                GUIController.instance.defenseSlots.SetActive(true);

                if (!GameObject.Find(_item.name))
                {
                    GameObject _created = Instantiate(GUIController.instance.defenseSlotPrefab, GUIController.instance.defenseParent);

                    _created.name = _item.name;

                    TextMeshProUGUI[] texts = _created.GetComponentsInChildren<TextMeshProUGUI>();

                    Image[] images = _created.GetComponentsInChildren<Image>();

                    foreach (Image i in images)
                    {
                        string objectName = i.name;

                        if (objectName == "Defense Item Icon")
                        {
                            i.sprite = _item.icon;
                        }
                    }

                    foreach (TextMeshProUGUI t in texts)
                    {
                        string objectName = t.name;

                        if (objectName == "Heal Name Text")
                        {
                            t.text = _item.name;
                        }

                        if (objectName == "Heal Amount Text")
                        {
                            t.text = "Defense: " + _item.defense;
                        }
                    }
                }
            }

            if (type == ItemType.Heal)
            {
                //Add value for healAmount to item class
            }

            if ((type == ItemType.KeyItem))
            {
                GUIController.instance.keySlots.SetActive(true);

                if (!GameObject.Find(_item.name))
                {
                    GameObject _created = Instantiate(GUIController.instance.keySlotPrefab, GUIController.instance.keyParent);

                    _created.name = _item.name;

                    TextMeshProUGUI[] texts = _created.GetComponentsInChildren<TextMeshProUGUI>();

                    Image[] images = _created.GetComponentsInChildren<Image>();

                    foreach (Image i in images)
                    {
                        string objectName = i.name;

                        if (objectName == "Key Item Icon")
                        {
                            i.sprite = _item.icon;
                        }
                    }

                    foreach (TextMeshProUGUI t in texts)
                    {
                        string objectName = t.name;

                        if (objectName == "Key Name Text")
                        {
                            t.text = _item.name;
                        }
                    }
                }
            }
        }

        GUIController.instance.openWeaponSlots();
    }
}
