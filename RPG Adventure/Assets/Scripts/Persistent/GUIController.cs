using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUIController : MonoBehaviour {

    public static GUIController instance;

    public bool minimapOpen = true, mapOpen = false, questsOpen = false;

    #region Inventory Variables
    public GameObject weaponSlotPrefab, defenseSlotPrefab, keySlotPrefab;

    [HideInInspector]
    public GameObject weaponSlots, defenseSlots, keySlots, inventoryObject;

    [HideInInspector]
    public Image headSlot, chestSlot, legsSlot, weaponSlot;

    [HideInInspector]
    public Transform weaponParent, defenseParent, keyParent;

    [HideInInspector]
    public Button weaponButton, defenseButton, keyButton;

    [HideInInspector]
    public Button weaponEquip, headEquip, chestEquip, legsEquip;
    #endregion

    #region Map Variables
    [HideInInspector]
    public GameObject mapObject, minimapObject;

    [HideInInspector]
    public Camera mapCamera, minimapCamera;
    #endregion

    #region Quest Variables
    public GameObject questPrefab;

    [HideInInspector]
    public GameObject questParent, questsObject;

    [HideInInspector]
    public TextMeshProUGUI activeQuestName, activeQuestObjective;
    #endregion

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        questParent = GameObject.Find("Quests Parent");

        questsObject = GameObject.Find("Quests");

        activeQuestName = GameObject.Find("Active Quest Name Text").GetComponent<TextMeshProUGUI>();
        activeQuestObjective = GameObject.Find("Active Quest Objective Text").GetComponent<TextMeshProUGUI>();

        if (!questsOpen && questsObject.activeSelf)
        {
            questsObject.SetActive(questsOpen);
        }
    }

    public void findInventoryUI()
    {

        #region Finding Inventory Objects
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

        //Finds Equip Buttons
        headEquip = GameObject.Find("Head Item Icon").GetComponent<Button>();
        chestEquip = GameObject.Find("Chest Item Icon").GetComponent<Button>();
        legsEquip = GameObject.Find("Leg Item Icon").GetComponent<Button>();
        weaponEquip = GameObject.Find("Weapon Item Icon").GetComponent<Button>();

        //Finds Slot Selector Buttons
        weaponButton = GameObject.Find("Weapons Button").GetComponent<Button>();
        defenseButton = GameObject.Find("Defense Button").GetComponent<Button>();
        keyButton = GameObject.Find("Key Button").GetComponent<Button>();

        //Assigns Listeners to Buttons
        weaponButton.onClick.AddListener(delegate { openWeaponSlots(); });
        defenseButton.onClick.AddListener(delegate { openDefenseSlots(); });
        keyButton.onClick.AddListener(delegate { openKeySlots(); });

        defaultDisable();
        #endregion

        mapObject = GameObject.Find("Map");

        minimapObject = GameObject.Find("Minimap");

        mapCamera = GameObject.Find("Map Camera").GetComponent<Camera>();

        minimapCamera = GameObject.Find("Minimap Camera").GetComponent<Camera>();

        toggleMap(mapOpen);
    }

    public void toggleQuests(bool _open)
    {
        if (_open)
        {
            questsOpen = true;
            questsObject.SetActive(true);
            return;
        }else if (!_open)
        {
            questsOpen = false;
            questsObject.SetActive(false);
            return;
        }
    }

    public void toggleMap(bool _open)
    {
        if (_open)
        {
            mapObject.SetActive(true);
            mapCamera.gameObject.SetActive(true);
            mapOpen = true;
            return;
        }else if (!_open)
        {
            mapObject.SetActive(false);
            mapCamera.gameObject.SetActive(false);
            mapOpen = false;
            return;
        }
    }

    public void toggleMinimap(bool _open)
    {
        if (_open)
        {
            minimapObject.SetActive(true);
            minimapOpen = true;
            return;
        }
        else if (!_open)
        {
            minimapObject.SetActive(false);
            minimapOpen = false;
            return;
        }
    }

    //Toggles inventory open and closed
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

    //Sets the default view of the inventory
    private void defaultDisable()
    {

        openWeaponSlots();

        toggleInventory(false);
    }

    public void setButtonListeners()
    {
        headEquip.onClick.AddListener( delegate { InventoryController.instance.unequipItem(ItemType.Head); });
        chestEquip.onClick.AddListener(delegate { InventoryController.instance.unequipItem(ItemType.Chest); });
        legsEquip.onClick.AddListener(delegate { InventoryController.instance.unequipItem(ItemType.Legs); });

        weaponEquip.onClick.AddListener(delegate { InventoryController.instance.unequipItem(ItemType.Weapon); });
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
