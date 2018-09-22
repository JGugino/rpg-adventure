using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    public GameObject playerInventory;

    public Camera mainCamera;

    private int playerHealth = 50;

    private PlayerMotor pMotor;

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
            if (playerInventory.activeSelf)
            {
                playerInventory.SetActive(false);
            }else if (!playerInventory.activeSelf)
            {
                playerInventory.SetActive(true);
            }
        }
    }

    private void moveSelect()
    {
        if (Input.GetButtonDown("Move/Select"))
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
                    if (distance <= range)
                    {
                        InventoryController.instance.addItem(item);

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
