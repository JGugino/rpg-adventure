using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUIController : MonoBehaviour {

    public static GUIController instance;

    public bool minimapOpen = true, mapOpen = false, questsOpen = false;

    #region Menu Variables
    public GameObject menuUI, loadUI, nameUI;

    public TMP_InputField nameInput;

    public Button playButton, settingsButton, exitButton, saveOneButton, saveTwoButton, saveThreeButton, backButton, createButton;
    public TextMeshProUGUI versionText;
    #endregion

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

    public void findMenuUI()
    {
        menuUI = GameObject.Find("Menu UI");
        loadUI = GameObject.Find("Load UI");
        nameUI = GameObject.Find("Name UI");

        playButton = GameObject.Find("Play Button").GetComponent<Button>();
        settingsButton = GameObject.Find("Settings Button").GetComponent<Button>();
        exitButton = GameObject.Find("Exit Button").GetComponent<Button>();

        saveOneButton = GameObject.Find("Save One Button").GetComponent<Button>();
        saveTwoButton = GameObject.Find("Save Two Button").GetComponent<Button>();
        saveThreeButton = GameObject.Find("Save Three Button").GetComponent<Button>();

        backButton = GameObject.Find("Back Button").GetComponent<Button>();

        createButton = GameObject.Find("Create Button").GetComponent<Button>();

        nameInput = GameObject.Find("Name Input").GetComponent<TMP_InputField>();

        versionText = GameObject.Find("Version Text").GetComponent<TextMeshProUGUI>();

        setMenuButtonListeners();

        setLoadButtonListeners();

        updateVersionText();

        toggleLoadUI(false);
        toggleNameUI(false);
    }

    public void updateVersionText()
    {
        versionText.text = "VERSION " + GameController.instance.gameVersion;
    }

    #region Find Game UI
    public void findQuestUI()
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

        setEquipButtonListeners();

        defaultDisable();
        #endregion

        mapObject = GameObject.Find("Map");

        minimapObject = GameObject.Find("Minimap");

        mapCamera = GameObject.Find("Map Camera").GetComponent<Camera>();

        minimapCamera = GameObject.Find("Minimap Camera").GetComponent<Camera>();

        toggleMap(mapOpen);
    }
    #endregion

    #region Toggle methods for menu UI
    public void toggleMenuUI(bool _open)
    {
        menuUI.SetActive(_open);
        loadUI.SetActive(!_open);
    }

    public void toggleLoadUI(bool _open)
    {
        loadUI.SetActive(_open);
        menuUI.SetActive(!_open);
    }

    public void toggleNameUI(bool _open)
    {
        nameUI.SetActive(_open);
    }

    #endregion

    #region Toggle methods for game UI
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
    #endregion

    #region Assign Button Listeners
    public void setMenuButtonListeners()
    {
        playButton.onClick.AddListener(delegate { toggleLoadUI(true); });
        exitButton.onClick.AddListener(delegate { GameController.instance.exitGame(); });
    }

    public void setLoadButtonListeners()
    {
        saveOneButton.onClick.AddListener(delegate { toggleNameUI(true); });
        saveTwoButton.onClick.AddListener(delegate { toggleNameUI(true); });
        saveThreeButton.onClick.AddListener(delegate { toggleNameUI(true); });

        backButton.onClick.AddListener(delegate { toggleMenuUI(true); });

        createButton.onClick.AddListener(delegate { GameController.instance.startGame(); });
    }

    public void setEquipButtonListeners()
    {
        headEquip.onClick.AddListener( delegate { InventoryController.instance.unequipItem(ItemType.Head); });
        chestEquip.onClick.AddListener(delegate { InventoryController.instance.unequipItem(ItemType.Chest); });
        legsEquip.onClick.AddListener(delegate { InventoryController.instance.unequipItem(ItemType.Legs); });

        weaponEquip.onClick.AddListener(delegate { InventoryController.instance.unequipItem(ItemType.Weapon); });
    }
    #endregion

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
