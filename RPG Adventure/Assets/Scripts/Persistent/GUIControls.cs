using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIControls : MonoBehaviour {

    public static GUIControls instance;

    private void Awake()
    {
        instance = this;
    }

    public void updatePlayerName()
    {
        GUIController.instance.playerNameText.text = GameController.instance.getPlayerName();
        GUIController.instance.playerStatsTitle.text = GameController.instance.getPlayerName() + "'S STATS";
    }

    public void updateHeaderStats()
    {
        GUIController.instance.playerHealthText.text = "HEALTH: " + PlayerManager.instance.playerController.getPlayerHealth();

        GUIController.instance.playerMoneyText.text = "MONEY: $" + PlayerManager.instance.playerController.getPlayerMoney();
    }

    public void updatePlayerStats()
    {
        GUIController.instance.playerAttackStat.text = "ATTACK: " + PlayerManager.instance.playerController.getPlayerAttack();
        GUIController.instance.playerDefenceStat.text = "DEFENCE: " + PlayerManager.instance.playerController.getPlayerDefence();
        GUIController.instance.playerSpecialStat.text = "SPECIAL: NONE";
    }

    #region Menu Toggles
    public void toggleMenuUI(bool _open)
    {
        GUIController.instance.menuUI.SetActive(_open);
        GUIController.instance.loadUI.SetActive(!_open);
        GUIController.instance.overwriteUI.SetActive(!_open);

        GUIController.instance.nameInput.text = "";
    }

    public void toggleLoadUI(bool _open)
    {
        GUIController.instance.loadUI.SetActive(_open);
        GUIController.instance.menuUI.SetActive(!_open);
        GUIController.instance.overwriteUI.SetActive(!_open);
    }

    public void toggleOverwriteUI(bool _open)
    {
        GUIController.instance.overwriteUI.SetActive(_open);
    }

    public void toggleContinueButton(bool _open)
    {
        if (GUIController.instance.continueButton != null)
        {
            GUIController.instance.continueButton.gameObject.SetActive(_open);
        }
    }

    #endregion

    #region Game Toggles
    public void toggleMap(bool _open)
    {
        GUIController.instance.mapObject.SetActive(_open);
        GUIController.instance.mapCamera.gameObject.SetActive(_open);
        GUIController.instance.mapOpen = _open;
    }

    public void toggleTabMenu(bool _open)
    {
        GUIController.instance.tabMenuObject.SetActive(_open);
    }

    public void toggleQuests(bool _open)
    {
        GUIController.instance.questsObject.SetActive(_open);
        GUIController.instance.invMenuTitle.gameObject.SetActive(false);
        GUIController.instance.questMenuTitle.gameObject.SetActive(true);
        GUIController.instance.inventoryObject.SetActive(!_open);
    }

    public void toggleInventory(bool _open)
    {
        GUIController.instance.inventoryObject.SetActive(_open);
        GUIController.instance.invMenuTitle.gameObject.SetActive(true);
        GUIController.instance.questMenuTitle.gameObject.SetActive(false);
        GUIController.instance.questsObject.SetActive(!_open);

        openWeaponSlots();
    }

    public void togglePauseMenu(bool _open)
    {
        GUIController.instance.pauseUI.SetActive(_open);
        GameController.instance.isPaused = _open;
    }
    #endregion

    #region Inventory Slots Controls

    #region Open Slot
    public void openWeaponSlots()
    {
        GUIController.instance.weaponSlots.SetActive(true);
        closeDefenseSlots();
        closeCreaturesSlots();
        closeKeySlots();
    }

    public void openDefenseSlots()
    {
        GUIController.instance.defenseSlots.SetActive(true);
        closeWeaponSlots();
        closeCreaturesSlots();
        closeKeySlots();
    }

    public void openCreaturesSlots()
    {
        GUIController.instance.creaturesSlots.SetActive(true);
        closeWeaponSlots();
        closeDefenseSlots();
        closeKeySlots();
    }

    public void openKeySlots()
    {
        GUIController.instance.keySlots.SetActive(true);
        closeDefenseSlots();
        closeWeaponSlots();
        closeCreaturesSlots();
    }
    #endregion

    #region Close Slot
    public void closeWeaponSlots()
    {
        GUIController.instance.weaponSlots.SetActive(false);
    }

    public void closeDefenseSlots()
    {
        GUIController.instance.defenseSlots.SetActive(false);
    }

    public void closeCreaturesSlots()
    {
        GUIController.instance.creaturesSlots.SetActive(false);
    }

    public void closeKeySlots()
    {
        GUIController.instance.keySlots.SetActive(false);
    }
    #endregion

    #endregion
}
