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

    private InventoryItem equippedHead, equippedChest, equippedLegs, equippedWeapon;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GUIController.instance.findInventoryUI();
        GUIController.instance.setButtonListeners();
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

            if ((type == ItemType.Weapon))
            {
                GUIController.instance.weaponSlots.SetActive(true);

                if (!GameObject.Find(_item.name))
                {
                    GameObject _created = Instantiate(GUIController.instance.weaponSlotPrefab, GUIController.instance.weaponParent);

                    _created.name = _item.name;

                    TextMeshProUGUI[] texts = _created.GetComponentsInChildren<TextMeshProUGUI>();

                    Image[] images = _created.GetComponentsInChildren<Image>();

                    Button equipButton = _created.GetComponentInChildren<Button>();

                    equipButton.onClick.AddListener(delegate { equipItem(_item); });

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

                    Button equipButton = _created.GetComponentInChildren<Button>();

                    equipButton.onClick.AddListener(delegate { equipItem(_item); });

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

                    Button equipButton = _created.GetComponentInChildren<Button>();

                    equipButton.onClick.AddListener(delegate { equipItem(_item); });

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

    public void equipItem(InventoryItem _item)
    {
        if (_item.type == ItemType.Weapon)
        {
            equippedWeapon = _item;
            GUIController.instance.weaponSlot.sprite = _item.icon;
            inventoryItems.Remove(_item);

            GameObject prefabToRemove = GameObject.Find(_item.name);

            if (prefabToRemove != null)
            {
                prefabToRemove.SetActive(false);
            }
        }

        if ((_item.type == ItemType.Head))
        {
            equippedHead = _item;
            GUIController.instance.headSlot.sprite = _item.icon;
            inventoryItems.Remove(_item);

            GameObject prefabToRemove = GameObject.Find(_item.name);

            if (prefabToRemove != null)
            {
                prefabToRemove.SetActive(false);
            }
        }

        if ((_item.type == ItemType.Chest))
        {
            equippedChest = _item;
            GUIController.instance.chestSlot.sprite = _item.icon;
            inventoryItems.Remove(_item);

            GameObject prefabToRemove = GameObject.Find(_item.name);

            if (prefabToRemove != null)
            {
                prefabToRemove.SetActive(false);
            }
        }

        if ((_item.type == ItemType.Legs))
        {
            equippedLegs = _item;
            GUIController.instance.legsSlot.sprite = _item.icon;
            inventoryItems.Remove(_item);

            GameObject prefabToRemove = GameObject.Find(_item.name);

            if (prefabToRemove != null)
            {
                prefabToRemove.SetActive(false);
            }
        }
    }

    public void unequipItem(ItemType _type)
    {
        if (_type == ItemType.Weapon)
        {
            inventoryItems.Add(equippedWeapon);
            GUIController.instance.weaponSlot.sprite = null;
            equippedWeapon = null;

            GameObject prefabToAdd = GameObject.Find(equippedWeapon.name);

            if (prefabToAdd != null)
            {
                prefabToAdd.SetActive(true);
            }
        }

        if ((_type == ItemType.Head))
        {
            inventoryItems.Add(equippedHead);
            GUIController.instance.headSlot.sprite = null;
            equippedHead = null;

            GameObject prefabToAdd = GameObject.Find(equippedHead.name);

            if (prefabToAdd != null)
            {
                prefabToAdd.SetActive(true);
            }
        }

        if ((_type == ItemType.Chest))
        {
            inventoryItems.Add(equippedChest);
            GUIController.instance.chestSlot.sprite = null;
            equippedChest = null;

            GameObject prefabToAdd = GameObject.Find(equippedChest.name);

            if (prefabToAdd != null)
            {
                prefabToAdd.SetActive(true);
            }
        }

        if ((_type == ItemType.Legs))
        {
            inventoryItems.Add(equippedLegs);
            GUIController.instance.legsSlot.sprite = null;
            equippedLegs = null;

            GameObject prefabToAdd = GameObject.Find(equippedLegs.name);

            if (prefabToAdd != null)
            {
                prefabToAdd.SetActive(true);
            }
        }
    }
}
