using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance;

    public bool isPaused = false;

    public string gameVersion = "0.0.2 alpha";

	void Awake () {
        instance = this;
	}

}
