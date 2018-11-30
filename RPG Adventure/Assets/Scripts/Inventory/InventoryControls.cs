using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryControls : MonoBehaviour {

    public static InventoryControls instance;

    public void Awake()
    {
        instance = this;
    }

    #region Add item to inventory list
    public void addItem(Item _item)
    {

        if (!InventoryController.instance.inventoryItems.Contains(_item))
        {
            InventoryController.instance.inventoryItems.Add(_item);
            return;
        }
        else if (InventoryController.instance.inventoryItems.Contains(_item))
        {
            Debug.Log("Item " + _item.name + " already exists in the inventory.");
            return;
        }
    }
    #endregion

    #region Creating Inventory Prefabs
    public void createPrefab()
    {
        if (InventoryController.instance.inventoryItems.Count > 0)
        {
            foreach (Item _item in InventoryController.instance.inventoryItems)
            {
                if (_item != null)
                {
                    ItemType type = _item.type;

                    switch (type)
                    {
                        case ItemType.Weapon:
                            if (!GameObject.Find(_item.itemName))
                            {
                                GUIControls.instance.openWeaponSlots();

                                GameObject _created = Instantiate(GUIController.instance.weaponSlotPrefab, GUIController.instance.weaponParent);

                                _created.name = _item.itemName;

                                TextMeshProUGUI[] texts = _created.GetComponentsInChildren<TextMeshProUGUI>();

                                Image[] images = _created.GetComponentsInChildren<Image>();

                                Button equipButton = _created.GetComponentInChildren<Button>();

                                equipButton.onClick.AddListener(delegate { equipItem(_item); });

                                foreach (Image i in images)
                                {
                                    string objectName = i.name;

                                    if (objectName == "Weapon Item Icon")
                                    {
                                        if (i.sprite != null)
                                        {
                                            i.sprite = _item.icon;
                                        }
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
                            break;

                        case ItemType.Head:
                            assignDefenseItems(_item);
                            break;

                        case ItemType.Chest:
                            assignDefenseItems(_item);
                            break;

                        case ItemType.Legs:
                            assignDefenseItems(_item);
                            break;

                        case ItemType.KeyItem:
                            if (!GameObject.Find(_item.itemName))
                            {
                                GUIControls.instance.openKeySlots();

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
                            break;

                        default:
                            Debug.Log("Don't know what to do with item... " + _item.itemName);
                            break;
                    }

                    GUIControls.instance.openWeaponSlots();
                }
            }
        }    
    }
    #endregion

    public void assignDefenseItems(Item _item)
    {
        if (GameObject.Find(_item.itemName))
        {
            GUIControls.instance.openDefenseSlots();

            GameObject _created = Instantiate(GUIController.instance.defenseSlotPrefab, GUIController.instance.defenseParent);

            _created.name = _item.itemName;

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

    #region Equip Items
    public void equipItem(Item _item)
    {
        ItemType _type = _item.type;

        switch (_type)
        {
            case ItemType.Weapon:
                if (InventoryController.instance.getEquippedWeapon() != null)
                {
                    unequipItem(_item.type);

                    setupEquipSlot(_item);
                }
                else
                {
                    setupEquipSlot(_item);
                }
                break;

            case ItemType.Head:
                if (InventoryController.instance.getEquippedHead() != null)
                {
                    unequipItem(_item.type);

                    setupEquipSlot(_item);

                }
                else
                {
                    setupEquipSlot(_item);
                }
                break;

            case ItemType.Chest:
                if (InventoryController.instance.getEquippedChest() != null)
                {
                    unequipItem(_item.type);

                    setupEquipSlot(_item);
                }
                else
                {

                    setupEquipSlot(_item);
                }
                break;

            case ItemType.Legs:
                if (InventoryController.instance.getEquippedLegs() != null)
                {
                    unequipItem(_item.type);

                    setupEquipSlot(_item);
                }
                else
                {

                    setupEquipSlot(_item);
                }
                break;
        }
    }
    #endregion

    private void setupEquipSlot(Item _item)
    {
        ItemType _type = _item.type;

        switch (_type)
        {
            case ItemType.Weapon:
                InventoryController.instance.setEquippedWeapon(_item);
                GUIController.instance.weaponSlot.sprite = _item.icon;
                InventoryController.instance.inventoryItems.Remove(_item);

                GameObject prefabToRemoveW = GameObject.Find(_item.name);

                if (prefabToRemoveW != null)
                {
                    GameObject.Destroy(prefabToRemoveW);
                }
                break;

            case ItemType.Head:
                InventoryController.instance.setEquippedHead(_item);
                GUIController.instance.headSlot.sprite = _item.icon;
                InventoryController.instance.inventoryItems.Remove(_item);

                GameObject prefabToRemoveH = GameObject.Find(_item.name);

                if (prefabToRemoveH != null)
                {
                    GameObject.Destroy(prefabToRemoveH);
                }
                break;

            case ItemType.Chest:
                InventoryController.instance.setEquippedChest(_item);
                GUIController.instance.chestSlot.sprite = _item.icon;
                InventoryController.instance.inventoryItems.Remove(_item);

                GameObject prefabToRemoveC = GameObject.Find(_item.name);

                if (prefabToRemoveC != null)
                {
                    GameObject.Destroy(prefabToRemoveC);
                }
                break;

            case ItemType.Legs:
                InventoryController.instance.setEquippedLegs(_item);
                GUIController.instance.legsSlot.sprite = _item.icon;
                InventoryController.instance.inventoryItems.Remove(_item);

                GameObject prefabToRemoveL = GameObject.Find(_item.name);

                if (prefabToRemoveL != null)
                {
                    GameObject.Destroy(prefabToRemoveL);
                }
                break;
        }
    }

    #region Unequip Items
    public void unequipItem(ItemType _type)
    {
        switch (_type)
        {
            case ItemType.Weapon:
                if (InventoryController.instance.getEquippedWeapon() != null)
                {
                    InventoryController.instance.inventoryItems.Add(InventoryController.instance.getEquippedWeapon());
                    GUIController.instance.weaponSlot.sprite = null;
                    InventoryController.instance.setEquippedWeapon(null);

                    createPrefab();
                }
                break;

            case ItemType.Head:
                if (InventoryController.instance.getEquippedHead() != null)
                {
                    InventoryController.instance.inventoryItems.Add(InventoryController.instance.getEquippedHead());
                    GUIController.instance.headSlot.sprite = null;
                    InventoryController.instance.setEquippedHead(null);

                    createPrefab();
                }
                break;

            case ItemType.Chest:
                if (InventoryController.instance.getEquippedChest() != null)
                {
                    InventoryController.instance.inventoryItems.Add(InventoryController.instance.getEquippedChest());
                    GUIController.instance.chestSlot.sprite = null;
                    InventoryController.instance.setEquippedChest(null);

                    createPrefab();
                }
                break;

            case ItemType.Legs:
                if (InventoryController.instance.getEquippedLegs() != null)
                {

                    InventoryController.instance.inventoryItems.Add(InventoryController.instance.getEquippedLegs());
                    GUIController.instance.legsSlot.sprite = null;
                    InventoryController.instance.setEquippedLegs(null);

                    createPrefab();
                }
                break;
        }
    }
    #endregion

    public void reEquipItems()
    {
        if (LoadManager.instance.playerData.equippedHead != null)
        {
            equipItem(InventoryController.instance.getEquippedHead());
        }

        if (LoadManager.instance.playerData.equippedChest != null)
        {
            equipItem(InventoryController.instance.getEquippedChest());
        }
        if (LoadManager.instance.playerData.equippedLegs != null)
        {
            equipItem(InventoryController.instance.getEquippedLegs());
        }

        if (LoadManager.instance.playerData.equippedWeapon != null)
        {
            equipItem(InventoryController.instance.getEquippedWeapon());
        }

    }
}
