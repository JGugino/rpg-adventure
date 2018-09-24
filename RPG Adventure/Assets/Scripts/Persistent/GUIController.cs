﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour {

    public static GUIController instance;

    public GameObject weaponSlotPrefab, defenseSlotPrefab, keySlotPrefab;

    [HideInInspector]
    public GameObject weaponSlots, defenseSlots, keySlots, inventoryObject;

    [HideInInspector]
    public Image headSlot, chestSlot, legsSlot, weaponSlot;

    [HideInInspector]
    public Transform weaponParent, defenseParent, keyParent;

    [HideInInspector]
    public Button weaponButton, defenseButton, keyButton;

    private void Awake()
    {
        instance = this;
    }

    public void findInventoryUI()
    {
        //Finds Inventory Object
        inventoryObject = GameObject.Find("Player Inventory");

        //Finds Inventory Slots
        weaponSlots = GameObject.Find("Weapon Slots");
        defenseSlots = GameObject.Find("Defense Slots");
        keySlots = GameObject.Find("Key Slots");

        //Finds Inventory Slots Parent
        weaponParent = GameObject.Find("Weapon Slots Parent").transform;
        defenseParent = GameObject.Find("Defense Slots Parent").transform;
        keyParent = GameObject.Find("Key Slots Parent").transform;

        //Finds Inventory Equip Slots
        headSlot = GameObject.Find("Head Item Icon").GetComponent<Image>();
        chestSlot = GameObject.Find("Chest Item Icon").GetComponent<Image>();
        legsSlot = GameObject.Find("Leg Item Icon").GetComponent<Image>();
        weaponSlot = GameObject.Find("Weapon Item Icon").GetComponent<Image>();

        //Finds Slot Selector Buttons
        weaponButton = GameObject.Find("Weapons Button").GetComponent<Button>();
        defenseButton = GameObject.Find("Defense Button").GetComponent<Button>();
        keyButton = GameObject.Find("Key Button").GetComponent<Button>();

        //Assigns Listeners to Buttons
        weaponButton.onClick.AddListener(delegate { openWeaponSlots(); });
        defenseButton.onClick.AddListener(delegate { openDefenseSlots(); });
        keyButton.onClick.AddListener(delegate { openKeySlots(); });

        defaultDisable();
    }

    public void toggleInventory(bool open)
    {
        if (open)
        {
            inventoryObject.SetActive(true);
        }else if (!open)
        {
            inventoryObject.SetActive(false);
        }
    }

    private void defaultDisable()
    {

        openWeaponSlots();

        toggleInventory(false);
    }

    #region Slot Controls

    public void openWeaponSlots()
    {
        weaponSlots.SetActive(true);
        closeDefenseSlots();
        closeKeySlots();
    }

    public void closeWeaponSlots()
    {
        weaponSlots.SetActive(false);
    }

    public void openDefenseSlots()
    {
        defenseSlots.SetActive(true);
        closeWeaponSlots();
        closeKeySlots();
    }

    public void closeDefenseSlots()
    {
        defenseSlots.SetActive(false);
    }

    public void openKeySlots()
    {
        keySlots.SetActive(true);
        closeDefenseSlots();
        closeWeaponSlots();
    }

    public void closeKeySlots()
    {
        keySlots.SetActive(false);
    }

    #endregion
}