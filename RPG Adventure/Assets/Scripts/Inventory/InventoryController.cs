using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {

    public static InventoryController instance;

    public List<Item> inventoryItems;

    private Item equippedHead, equippedChest, equippedLegs, equippedWeapon;

    [SerializeField]
    public GameObject equippedCreature;

    private void Awake()
    {
        instance = this;
    }

    #region Getters
    public Item getEquippedWeapon()
    {
        return equippedWeapon;
    }

    public Item getEquippedHead()
    {
        return equippedHead;
    }

    public Item getEquippedChest()
    {
        return equippedChest;
    }

    public Item getEquippedLegs()
    {
        return equippedLegs;
    }

    public GameObject getEquippedCreature()
    {
        return equippedCreature;
    }
    #endregion

    #region Setters
    public void setEquippedHead(Item _head)
    {
        equippedHead = _head;
    }

    public void setEquippedChest(Item _chest)
    {
        equippedChest = _chest;
    }

    public void setEquippedLegs(Item _legs)
    {
        equippedLegs = _legs;
    }

    public void setEquippedWeapon(Item _weapon)
    {
        equippedWeapon = _weapon;
    }

    public void setEquippedCreature(GameObject _creature)
    {
        equippedCreature = _creature;
    }
    #endregion
}
