using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    public Camera mainCamera;

    private int playerHealth = 100, playerMaxHealth = 100;

    private int baseDamage = 3;

    private PlayerMotor pMotor;

    private bool isPaused = false;

    private bool controllingCreature = false;

    private Vector3 startPoint;

    private void Awake()
    {
        pMotor = GetComponent<PlayerMotor>();

        startPoint = transform.position;

        playerHealth = playerMaxHealth;
    }

    void Update () {

        if (!controllingCreature)
        {
            moveSelect();
        }
        else if (controllingCreature)
        {
            moveCreature();
        }

        toggleInventory();

        toggleMap();

        if (Input.GetButtonDown("Use"))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitPoint;

            if (Physics.Raycast(ray, out hitPoint))
            {
                if (hitPoint.collider.GetComponent<CreatureController>())
                {
                    InventoryController.instance.equippedCreature = hitPoint.collider.gameObject;
                    controllingCreature = true;
                }
            }
        }

        if ((controllingCreature) && (Input.GetButtonDown("Interact")))
        {
            InventoryController.instance.equippedCreature = null;
            controllingCreature = false;
        }

        //Player Death
        if (playerHealth <= 0)
        {
            killPlayer();
        }
	}

    private void moveCreature()
    {
        if (InventoryController.instance.getEquippedCreature() != null)
        {
            if (Input.GetButtonDown("Move/Select") && (!isPaused) && (controllingCreature))
            {
                CreatureMotor equippedCreature = InventoryController.instance.getEquippedCreature().GetComponent<CreatureMotor>();

                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                RaycastHit hitPoint;

                if (Physics.Raycast(ray, out hitPoint))
                {
                    equippedCreature.moveToPoint(hitPoint.point);
                }
            }
        }
    }

    private void moveSelect()
    {
        if (Input.GetButtonDown("Move/Select") && (!isPaused))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitFromRay;

            if (Physics.Raycast(ray, out hitFromRay))
            {
                pMotor.moveToPoint(hitFromRay.point);

                //Pick up Items
                if (hitFromRay.collider.GetComponent<ItemController>())
                {
                    Item item = hitFromRay.collider.GetComponent<ItemController>().item;

                    int range = item.range;

                    int distance = (int)Vector3.Distance(transform.position, hitFromRay.point);

                    ItemType _type = item.type;

                    if (distance <= range)
                    {
                        if (_type == ItemType.Weapon)
                        {
                            Weapon _weapon = (Weapon)item;

                            InventoryController.instance.addItem(item, _weapon);
                        }
                        else if ((_type == ItemType.Head) || (_type == ItemType.Chest) || (_type == ItemType.Legs))
                        {
                            Armor _armor = (Armor)item;

                            InventoryController.instance.addItem(item, null, _armor);
                        }
                        else
                        {
                            InventoryController.instance.addItem(item);
                        }


                        hitFromRay.collider.gameObject.SetActive(false);
                    }
                }

                //Attack Creatures
                if (hitFromRay.collider.GetComponent<CreatureController>())
                {
                    if (InventoryController.instance.getEquippedWeapon() != null)
                    {
                        hitFromRay.collider.GetComponent<CreatureController>().takeDamage(this.transform, InventoryController.instance.getEquippedWeapon().damage);
                    }
                    else
                    {
                        hitFromRay.collider.GetComponent<CreatureController>().takeDamage(this.transform, baseDamage);
                    }
                }

                //Attack Enemy
                if (hitFromRay.collider.GetComponent<EnemyController>())
                {
                    if (InventoryController.instance.getEquippedWeapon() != null)
                    {
                        hitFromRay.collider.GetComponent<EnemyController>().takeDamage(InventoryController.instance.getEquippedWeapon().damage);
                    }
                    else
                    {
                        hitFromRay.collider.GetComponent<EnemyController>().takeDamage(baseDamage);
                    }
                }
            }
        }
    }

    private void toggleMap()
    {
        if (Input.GetButtonDown("Map"))
        {
            if (!GUIController.instance.mapOpen)
            {
                GUIController.instance.toggleMap(true);
                GUIController.instance.toggleMinimap(false);
                isPaused = true;
            }else if (GUIController.instance.mapOpen)
            {
                GUIController.instance.toggleMap(false);
                GUIController.instance.toggleMinimap(true);
                isPaused = false;
            }
        }
    }

    private void toggleInventory()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            if (GUIController.instance.inventoryObject.activeSelf)
            {
                GUIController.instance.toggleMinimap(true);
                GUIController.instance.toggleInventory(false);
                isPaused = false;
            }
            else if (!GUIController.instance.inventoryObject.activeSelf)
            {
                if (GUIController.instance.mapOpen)
                {
                    GUIController.instance.toggleMap(false);
                }
                GUIController.instance.toggleMinimap(false);
                GUIController.instance.toggleInventory(true);
                InventoryController.instance.createPrefab();
                isPaused = true;
            }
        }
    }

    public void takeDamage(int _damage)
    {
        playerHealth -= _damage;

        Debug.Log("Player Health: " + playerHealth);

        if (playerHealth <= 0)
        {
            killPlayer();
        }

        return;
    }

    private void killPlayer()
    {
        transform.position = startPoint;

        playerHealth = playerMaxHealth;
    }

    public bool getControllingCreature()
    {
        return controllingCreature;
    }
}
