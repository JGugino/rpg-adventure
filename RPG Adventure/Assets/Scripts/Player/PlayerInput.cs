using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerInput : MonoBehaviour {

    public Quest quest;

    public Transform target;

    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F4))
        {
            QuestController.instance.addQuest(quest, target);
        }else if (Input.GetKeyDown(KeyCode.F5))
        {
            QuestController.instance.removeQuest(quest);
        }


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
            toggleInventory();
        }

        if (Input.GetButtonDown("Quests"))
        {
            toggleQuests();
        }
    }

    public void toggleMap()
    {
        if (!GUIController.instance.mapOpen)
        {
            GUIController.instance.toggleMap(true);
            GUIController.instance.toggleMinimap(false);
            GameController.instance.isPaused = true;
        }
        else if (GUIController.instance.mapOpen)
        {
            GUIController.instance.toggleMap(false);
            GUIController.instance.toggleMinimap(true);
            GameController.instance.isPaused = false;
        }
    }

    public void toggleInventory()
    {
        if (GUIController.instance.inventoryObject.activeSelf)
        {
            GUIController.instance.toggleMinimap(true);
            GUIController.instance.toggleInventory(false);
            GameController.instance.isPaused = false;
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
            GameController.instance.isPaused = true;
        }
    }

    public void toggleQuests()
    {
        if (GUIController.instance.questsObject.activeSelf)
        {
            GUIController.instance.toggleQuests(false);
            GameController.instance.isPaused = false;
        }
        else if (!GUIController.instance.questsObject.activeSelf)
        {
            GUIController.instance.toggleQuests(true);
            GameController.instance.isPaused = false;
        }
    }
}
