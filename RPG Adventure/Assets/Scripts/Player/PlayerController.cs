using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    public Camera mainCamera;

    private int playerHealth = 50;

    private PlayerMotor pMotor;

    private bool isPaused = false;

    private void Awake()
    {
        pMotor = GetComponent<PlayerMotor>();
    }

    void Update () {

        moveSelect();

        toggleInventory();

        //Player Death
        if (playerHealth <= 0)
        {
            killPlayer();
        }
	}

    private void toggleInventory()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            if (GUIController.instance.inventoryObject.activeSelf)
            {
                GUIController.instance.toggleInventory(false);
                isPaused = false;
            }
            else if (!GUIController.instance.inventoryObject.activeSelf)
            {
                GUIController.instance.toggleInventory(true);
                InventoryController.instance.createPrefab();
                isPaused = true;
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
                        }else if ((_type == ItemType.Head) || (_type == ItemType.Chest) || (_type == ItemType.Legs))
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
            }
        }
    }

    private void killPlayer()
    {
        Debug.Log("Player Dead");
    }
}
