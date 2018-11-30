using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerInput : MonoBehaviour {

    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!GameController.instance.isPaused)
        {
            if (Input.GetButtonDown("Move/Select") && (!GameController.instance.isPaused) && (!playerController.getControllingCreature()))
            {
                playerController.moveCharacter();
            }
            else if (Input.GetButtonDown("Move/Select") && (!GameController.instance.isPaused) && (playerController.getControllingCreature()))
            {
                playerController.moveCreature();
            }

            if (Input.GetButtonDown("Use"))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hitPoint;

                if (Physics.Raycast(ray, out hitPoint))
                {
                    if (hitPoint.collider.GetComponent<CreatureController>())
                    {
                        InventoryController.instance.equippedCreature = hitPoint.collider.gameObject;
                        playerController.setControllingCreature(true);
                    }
                }
            }

            if ((playerController.getControllingCreature()) && (Input.GetButtonDown("Interact")))
            {
                InventoryController.instance.equippedCreature = null;
                playerController.setControllingCreature(false);
            }
        }

        if (Input.GetButtonDown("Map"))
        {
            toggleMap();
        }

        if (Input.GetButtonDown("Inventory"))
        {
            toggleTabMenu();
        }

        if (Input.GetButtonDown("Pause"))
        {
            if (GUIController.instance.pauseUI.activeSelf)
            {
                GUIControls.instance.togglePauseMenu(false);
            }
            else if (!GUIController.instance.pauseUI.activeSelf)
            {
                GUIControls.instance.togglePauseMenu(true);
            }
        }
    }

    public void toggleMap()
    {
        if (!GUIController.instance.mapOpen)
        {
            GUIControls.instance.toggleMap(true);
            GameController.instance.isPaused = true;
        }
        else if (GUIController.instance.mapOpen)
        {
            GUIControls.instance.toggleMap(false);
            GameController.instance.isPaused = false;
        }
    }

    public void toggleTabMenu()
    {
        if (GUIController.instance.tabMenuObject.activeSelf)
        {
            GUIControls.instance.toggleTabMenu(false);
            GameController.instance.isPaused = false;
        }
        else if (!GUIController.instance.tabMenuObject.activeSelf)
        {
            if (GUIController.instance.mapOpen)
            {
                GUIControls.instance.toggleMap(false);
            }

            GUIControls.instance.toggleTabMenu(true);
            GUIControls.instance.toggleInventory(true);
            GUIControls.instance.updatePlayerName();
            GUIControls.instance.updateHeaderStats();
            GUIControls.instance.updatePlayerStats();
            InventoryControls.instance.createPrefab();
            GameController.instance.isPaused = true;
        }
    }
}
