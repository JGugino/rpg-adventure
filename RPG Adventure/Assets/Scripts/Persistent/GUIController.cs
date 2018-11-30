using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(GUIControls))]
public class GUIController : MonoBehaviour {

    public static GUIController instance;

    public bool mapOpen = false;

    #region Menu Variables
    [HideInInspector]
    public GameObject menuUI, loadUI, overwriteUI;

    [HideInInspector]
    public TMP_InputField nameInput;

    [HideInInspector]
    public Button newGameButton, continueButton, settingsButton, exitButton, backButton, createButton, yesButton, noButton;

    [HideInInspector]
    public TextMeshProUGUI versionText;
    #endregion

    #region Tab Menu Variables

    public GameObject tabMenuObject;

    #region Menu Header Variables

    [HideInInspector]
    public TextMeshProUGUI invMenuTitle, questMenuTitle, playerHealthText, playerMoneyText;

    [HideInInspector]
    public Button invButton, questsButton;

    #endregion

    #region Inventory Variables
    public GameObject weaponSlotPrefab, defenseSlotPrefab, keySlotPrefab;

    [HideInInspector]
    public GameObject weaponSlots, defenseSlots, creaturesSlots, keySlots, inventoryObject;

    [HideInInspector]
    public Image headSlot, chestSlot, legsSlot, weaponSlot;

    [HideInInspector]
    public Transform weaponParent, defenseParent, creaturesParent, keyParent;

    [HideInInspector]
    public Button weaponButton, defenseButton, creaturesButton, keyButton;

    [HideInInspector]
    public Button weaponEquip, headEquip, chestEquip, legsEquip;

    [HideInInspector]
    public TextMeshProUGUI playerStatsTitle, playerAttackStat, playerDefenceStat, playerSpecialStat, playerNameText;

    [HideInInspector]
    public TextMeshProUGUI creaturesAttackStat, creaturesDefenceStat, creaturesSpecialStat;
    #endregion

    #region Quest Variables
    public GameObject questPrefab;

    [HideInInspector]
    public GameObject questsObject, activeQuestsParent, completedQuestsParent;

    [HideInInspector]
    public TextMeshProUGUI activeQuestName, activeQuestObjective, selectedQuestName, selectedQuestDescription;
    #endregion

    #endregion

    #region Map Variables
    [HideInInspector]
    public GameObject mapObject;

    [HideInInspector]
    public Camera mapCamera;
    #endregion

    #region Pause Menu Variables
    public GameObject pauseUI;

    public Button pauseSaveButton, pauseLoadButton, pauseSettingsButton, pauseMainMenuButton;
    #endregion

    private void Awake()
    {
        instance = this;
    }

    public void updateVersionText()
    {
        versionText.text = "VERSION " + GameController.instance.gameVersion;
    }

    #region Find UI
    public void findMenuUI()
    {
        menuUI = GameObject.Find("Menu UI");
        loadUI = GameObject.Find("Load UI");
        overwriteUI = GameObject.Find("Overwrite UI");


        newGameButton = GameObject.Find("New Game Button").GetComponent<Button>();
        continueButton = GameObject.Find("Continue Button").GetComponent<Button>();
        settingsButton = GameObject.Find("Settings Button").GetComponent<Button>();
        exitButton = GameObject.Find("Exit Button").GetComponent<Button>();

        backButton = GameObject.Find("Back Button").GetComponent<Button>();

        createButton = GameObject.Find("Create Button").GetComponent<Button>();

        yesButton = GameObject.Find("Yes Button").GetComponent<Button>();
        noButton = GameObject.Find("No Button").GetComponent<Button>();

        nameInput = GameObject.Find("Name Input").GetComponent<TMP_InputField>();

        versionText = GameObject.Find("Version Text").GetComponent<TextMeshProUGUI>();

        setMenuButtonListeners();

        setLoadButtonListeners();

        updateVersionText();

        GUIControls.instance.toggleLoadUI(false);
    }

    public void findGameUI()
    {
        findMapUI();

        findPauseUI();
        findHeaderUI();
        findQuestUI();
        findInventoryUI();
    }

    public void findHeaderUI()
    {
        tabMenuObject = GameObject.Find("Tab Menu");

        invMenuTitle = GameObject.Find("Header Inv Text").GetComponent<TextMeshProUGUI>();
        questMenuTitle = GameObject.Find("Header Quests Text").GetComponent<TextMeshProUGUI>();

        playerHealthText = GameObject.Find("Player Header Health Text").GetComponent<TextMeshProUGUI>();
        playerMoneyText = GameObject.Find("Player Header Money Text").GetComponent<TextMeshProUGUI>();

        invButton = GameObject.Find("Header Inv Button").GetComponent<Button>();
        questsButton = GameObject.Find("Header Quests Button").GetComponent<Button>();

        setHeaderButtonListeners();
    }

    public void findQuestUI()
    {
        questsObject = GameObject.Find("Player Quests");

        activeQuestsParent = GameObject.Find("Active Quests Parent");
        completedQuestsParent = GameObject.Find("Completed Quests Parent");

        selectedQuestName = GameObject.Find("Quest Name Text").GetComponent<TextMeshProUGUI>();
        selectedQuestDescription = GameObject.Find("Quest Desc Text").GetComponent<TextMeshProUGUI>();
    }

    public void findInventoryUI()
    {
        //Finds Inventory Object
        inventoryObject = GameObject.Find("Player Inventory");

        //Finds Inventory Slots
        weaponSlots = GameObject.Find("Weapon Slots");
        defenseSlots = GameObject.Find("Defense Slots");
        creaturesSlots = GameObject.Find("Creatures Slots");
        keySlots = GameObject.Find("Key Slots");

        //Finds Inventory Slots Parent
        weaponParent = GameObject.Find("Weapon Slots Parent").transform;
        defenseParent = GameObject.Find("Defense Slots Parent").transform;
        creaturesParent = GameObject.Find("Creatures Slots Parent").transform;
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
        creaturesButton = GameObject.Find("Creatures Button").GetComponent<Button>();
        keyButton = GameObject.Find("Key Button").GetComponent<Button>();

        playerStatsTitle = GameObject.Find("Player Stats Title Text").GetComponent<TextMeshProUGUI>();
        playerAttackStat = GameObject.Find("Player Stat Attack Text").GetComponent<TextMeshProUGUI>();
        playerDefenceStat = GameObject.Find("Player Stat Defence Text").GetComponent<TextMeshProUGUI>();
        playerSpecialStat = GameObject.Find("Player Stat Special Text").GetComponent<TextMeshProUGUI>();

        playerNameText = GameObject.Find("Player Name Text").GetComponent<TextMeshProUGUI>();

        //Assigns Listeners to Buttons
        setTabMenuButtonListeners();

        defaultDisable();
    }

    public void findPauseUI()
    {
        pauseUI = GameObject.Find("Pause Menu");

        pauseSaveButton = GameObject.Find("Save Button").GetComponent<Button>();
        pauseLoadButton = GameObject.Find("Load Button").GetComponent<Button>();
        pauseSettingsButton = GameObject.Find("Settings Button").GetComponent<Button>();
        pauseMainMenuButton = GameObject.Find("Main Menu Button").GetComponent<Button>();

        setPauseButtonListeners();

        GUIControls.instance.togglePauseMenu(false);
    }

    public void findMapUI()
    {
        mapObject = GameObject.Find("Map");

        mapCamera = GameObject.Find("Map Camera").GetComponent<Camera>();

        activeQuestName = GameObject.Find("Active Quest Name Text").GetComponent<TextMeshProUGUI>();
        activeQuestObjective = GameObject.Find("Active Quest Objective Text").GetComponent<TextMeshProUGUI>();

        GUIControls.instance.toggleMap(mapOpen);
    }
    #endregion

    #region Assign Button Listeners

    #region Menu & Load Button Listeners
    public void setMenuButtonListeners()
    {
        newGameButton.onClick.AddListener(delegate { GUIControls.instance.toggleLoadUI(true); });
        continueButton.onClick.AddListener(delegate { GameController.instance.continueGame(); });
        exitButton.onClick.AddListener(delegate { GameController.instance.exitGame(); });
    }

    public void setLoadButtonListeners()
    {
        backButton.onClick.AddListener(delegate { GUIControls.instance.toggleMenuUI(true); });

        createButton.onClick.AddListener(delegate { GameController.instance.newGame(); });

        yesButton.onClick.AddListener(delegate { StartCoroutine(GameController.instance.NewGame()); });
        noButton.onClick.AddListener(delegate { GUIControls.instance.toggleMenuUI(true); });
    }
    #endregion

    #region Game Button Listeners
    public void setHeaderButtonListeners()
    {
        invButton.onClick.AddListener(delegate { GUIControls.instance.toggleInventory(true); });
        questsButton.onClick.AddListener(delegate { GUIControls.instance.toggleQuests(true); });
    }

    public void setPauseButtonListeners()
    {
        pauseSaveButton.onClick.AddListener(delegate { SaveManager.instance.savePlayerData(); });
        pauseLoadButton.onClick.AddListener(delegate { Debug.Log("Load Game"); });
        pauseSettingsButton.onClick.AddListener(delegate { Debug.Log("Settings Menu"); });
        pauseMainMenuButton.onClick.AddListener(delegate { GameController.instance.saveAndExit(); });
    }

    public void setTabMenuButtonListeners()
    {
        headEquip.onClick.AddListener(delegate { InventoryControls.instance.unequipItem(ItemType.Head); });
        chestEquip.onClick.AddListener(delegate { InventoryControls.instance.unequipItem(ItemType.Chest); });
        legsEquip.onClick.AddListener(delegate { InventoryControls.instance.unequipItem(ItemType.Legs); });

        weaponEquip.onClick.AddListener(delegate { InventoryControls.instance.unequipItem(ItemType.Weapon); });

        weaponButton.onClick.AddListener(delegate { GUIControls.instance.openWeaponSlots(); });
        defenseButton.onClick.AddListener(delegate { GUIControls.instance.openDefenseSlots(); });
        creaturesButton.onClick.AddListener(delegate { GUIControls.instance.openCreaturesSlots(); });
        keyButton.onClick.AddListener(delegate { GUIControls.instance.openKeySlots(); });
    }
    #endregion

    #endregion

    private void defaultDisable()
    {
        GUIControls.instance.toggleInventory(true);

        GUIControls.instance.toggleTabMenu(false);
    }
}
