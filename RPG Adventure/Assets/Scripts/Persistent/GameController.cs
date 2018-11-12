using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;

public class GameController : MonoBehaviour {

    public static GameController instance;

    [Header("TOTAL AMOUNT OF ALLOWED CREATURES")]
    public int maxCreatureCap = 60;

    [Header("Creatures Max"), Tooltip("Max amount of each creature")]
    public int maxPassiveCreatures = 20;
    public int maxNeutralCreatures = 20;
    public int maxAggressiveCreatures = 20;

    [Header("Creatures Totals"), Tooltip("Total amount of active creatures")]
    public int totalActiveCreatures = 0;
    public int totalPassiveCreatures = 0;
    public int totalNeutralCreatures = 0;
    public int totalAggressiveCreatures = 0;

    [Header("Game Settings")]
    public bool isPaused = false;
    public bool isGameStarted = false;

    public string gameVersion = "0.0.2 alpha";

    public int numberOfSaves = 0;

    private string playerName;

    private string saveLocation;

	void Awake () {
        instance = this;

        saveLocation = Application.persistentDataPath + "/" + "saves" + "/" + playerName;

        GUIController.instance.findMenuUI();
	}

    private void Start()
    {
        SaveManager.instance.saveSettingsData();
    }

    public void startGame()
    {
        if (GUIController.instance.nameInput.text == "")
        {
            GUIController.instance.nameInput.text = "Bob";
            playerName = GUIController.instance.nameInput.text;
        }
        else
        {
            playerName = GUIController.instance.nameInput.text;
        }

        saveLocation = Application.persistentDataPath + "/" + "saves" + "/" + playerName;

        GUIController.instance.toggleNameUI(false);

        SceneManager.LoadScene(1);

        StartCoroutine(LateStart());
    }

    public IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.001f);

        GUIController.instance.findInventoryUI();

        GUIController.instance.findQuestUI();

        SaveManager.instance.savePlayerData();
    }

    public void exitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game");
    }

    public void setPlayerName(string _name)
    {
        playerName = _name;
    }

    public string getSaveLocation()
    {
        return saveLocation;
    }

    public string getPlayerName()
    {
        return playerName;
    }
}
