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

        saveLocation = Application.persistentDataPath + "/" + "saves" + "/";

	}

    private void Start()
    {
        StartCoroutine(LateStart());
    }

    public IEnumerator LateStart()
    {
        yield return new WaitForSeconds(1f);


        if (!File.Exists(SaveManager.instance.settingSaveLocation + "/" + "settings.json"))
        {
            SaveManager.instance.saveSettingsData();
        }

        if (!File.Exists(SaveManager.instance.playerDataSaveLocation + "/player_data.json"))
        {
            GUIControls.instance.toggleContinueButton(false);
        }
        else
        {
            GUIControls.instance.toggleContinueButton(true);
        }
    }

    public void newGame()
    {
        if (!File.Exists(SaveManager.instance.playerDataSaveLocation + "/player_data.json"))
        {
            if (GUIController.instance.nameInput.text != "")
            {
                playerName = GUIController.instance.nameInput.text.ToUpper();

                StartCoroutine(NewGame());
            }
            else if (GUIController.instance.nameInput.text == "")
            {
                Debug.LogError("ERROR: YOU MUST ENTER A NAME!");
            }
        }
        else
        {
            if (GUIController.instance.nameInput.text != "")
            {
                playerName = GUIController.instance.nameInput.text.ToUpper();

                GUIControls.instance.toggleOverwriteUI(true);

            }
            else if (GUIController.instance.nameInput.text == "")
            {
                Debug.LogError("ERROR: YOU MUST ENTER A NAME!");
            }
        }
    }

    public void continueGame() {
        StartCoroutine(ContinueGame());
    }

    public IEnumerator NewGame()
    {
        SceneManager.LoadScene(1);

        yield return new WaitForSeconds(0.001f);

        PlayerManager.instance.spawnPlayer();

        GUIController.instance.findGameUI();

        SaveManager.instance.savePlayerData();
    }

    public IEnumerator ContinueGame()
    {

       SceneManager.LoadScene(1);

        yield return new WaitForSeconds(0.001f);

        PlayerManager.instance.spawnPlayer();

        GUIController.instance.findGameUI();

        if (!File.Exists(SaveManager.instance.playerDataSaveLocation + "/player_data.json"))
        {
            SaveManager.instance.savePlayerData();
        }
        else
        {
            LoadManager.instance.loadPlayerData();
        }

    }

    public void saveAndExit()
    {
        SaveManager.instance.savePlayerData();

        SceneManager.LoadScene(0);
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
