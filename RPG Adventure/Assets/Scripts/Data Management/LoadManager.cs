using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadManager : MonoBehaviour {

    public static LoadManager instance;

    public PlayerData playerData;

    private GameSettingsData settingsData;

    private void Awake()
    {
        instance = this;
    }

    void Start () {
        settingsData = new GameSettingsData();

        playerData = new PlayerData();
	}

    public void loadPlayerData()
    {
        string _playerData = File.ReadAllText(Path.Combine(SaveManager.instance.playerDataSaveLocation, "player_data.json"));
        JsonUtility.FromJsonOverwrite(_playerData, playerData);

        setPlayerData();
    }

    public void loadSettingData()
    {
        string _settingData = File.ReadAllText(Path.Combine(SaveManager.instance.settingSaveLocation, "settings.json"));
        JsonUtility.FromJsonOverwrite(_settingData, settingsData);

        setSettingsData();
    }

    public void setSettingsData()
    {
        GameController.instance.gameVersion = settingsData.gameVersion;
        GameController.instance.numberOfSaves = settingsData.numberOfSaves;
    }

    public void setPlayerData()
    {
        GameController.instance.setPlayerName(playerData.playerName);

        PlayerManager.instance.playerObject.GetComponent<PlayerController>().setPlayerHealth(playerData.playerHealth);

        if (playerData.inventoryItems != null)
        {
            foreach (Item _item in playerData.inventoryItems)
            {
                Debug.Log(_item);

                if (_item != null)
                {
                    InventoryControls.instance.addItem(_item);
                    return;
                }
                else
                {
                    return;
                }
            }
        }
        if (playerData.equippedHead != null)
        {
            InventoryController.instance.setEquippedHead(playerData.equippedHead);
        }
        if (playerData.equippedChest != null)
        {
            InventoryController.instance.setEquippedChest(playerData.equippedChest);
        }
        if (playerData.equippedLegs != null)
        {
            InventoryController.instance.setEquippedLegs(playerData.equippedLegs);
        }

        if (playerData.equippedWeapon != null)
        {
            InventoryController.instance.setEquippedWeapon(playerData.equippedWeapon);
        }

        InventoryController.instance.setEquippedCreature(playerData.equippedCreature);

        InventoryControls.instance.reEquipItems();
    }
}
