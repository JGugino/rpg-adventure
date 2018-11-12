using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadManager : MonoBehaviour {

    private PlayerData playerData;

    private GameSettingsData settingsData;

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

        InventoryController.instance.setEquippedHead(playerData.equippedHead);
        InventoryController.instance.setEquippedChest(playerData.equippedChest);
        InventoryController.instance.setEquippedLegs(playerData.equippedLegs);

        InventoryController.instance.setEquippedWeapon(playerData.equippedWeapon);

        InventoryController.instance.setEquippedCreature(playerData.equippedCreature);
    }
}
