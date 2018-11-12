using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour {

    public static SaveManager instance;

    public string settingSaveLocation;
    public string playerDataSaveLocation;

    private PlayerData playerData;

    private GameSettingsData gameSettingsData;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerData = new PlayerData();

        gameSettingsData = new GameSettingsData();

        settingSaveLocation = Application.persistentDataPath + "/" + "Settings";

        playerDataSaveLocation = GameController.instance.getSaveLocation() + "/" + "Player_Data";

        if (!Directory.Exists(settingSaveLocation))
        {
            Directory.CreateDirectory(settingSaveLocation);
        }

        if (!Directory.Exists(playerDataSaveLocation))
        {
            Directory.CreateDirectory(playerDataSaveLocation);
        }
    }

    public void savePlayerData()
    {
        getPlayerData();

        string _playerData = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(playerDataSaveLocation + "/" + "player_data.txt", _playerData);
    }

    public void saveSettingsData()
    {
        getSettingsData();

        string saveData = JsonUtility.ToJson(gameSettingsData,true);
        File.WriteAllText(settingSaveLocation + "/" + "settings.txt", saveData);
    }

    public void getSettingsData()
    {
        gameSettingsData.gameVersion = GameController.instance.gameVersion;
        gameSettingsData.numberOfSaves = GameController.instance.numberOfSaves;
    }

    public void getPlayerData()
    {
        playerData.playerName = GameController.instance.getPlayerName();

        playerData.playerHealth = PlayerManager.instance.playerObject.GetComponent<PlayerController>().getPlayerHealth();

        playerData.inventoryItems = InventoryController.instance.inventoryItems;

        playerData.equippedHead = InventoryController.instance.getEquippedHead();
        playerData.equippedChest = InventoryController.instance.getEquippedChest();
        playerData.equippedLegs = InventoryController.instance.getEquippedLegs();
        playerData.equippedWeapon = InventoryController.instance.getEquippedWeapon();

        playerData.equippedCreature = InventoryController.instance.getEquippedCreature();
    }
}
