using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData{
    public string playerName;

    public int playerHealth;

    public List<InventoryController.InventoryItem> inventoryItems;

    public InventoryController.InventoryItem equippedHead, equippedChest, equippedLegs, equippedWeapon;

    public GameObject equippedCreature;
}
