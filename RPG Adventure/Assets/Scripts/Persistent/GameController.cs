using UnityEngine;
using System.IO;

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

    public string gameVersion = "0.0.2 alpha";

    private string playerName = "Gugino";

    private string saveLocation;

	void Awake () {
        instance = this;

        saveLocation = Path.Combine(Application.persistentDataPath, Path.Combine("saves", playerName));
	}

}
