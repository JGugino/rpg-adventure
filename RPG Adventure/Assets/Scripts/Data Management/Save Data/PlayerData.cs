using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData{

    public string playerName;

    public int playerHealth;

    public List<Item> inventoryItems;

    public Item equippedHead, equippedChest, equippedLegs, equippedWeapon;

    public GameObject equippedCreature;
}
