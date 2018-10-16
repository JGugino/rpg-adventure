using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance;

    public int maxCreatureCap = 50;

    public int totalActiveCreatures = 0;

    public bool isPaused = false;

    public string gameVersion = "0.0.2 alpha";

	void Awake () {
        instance = this;
	}

}
