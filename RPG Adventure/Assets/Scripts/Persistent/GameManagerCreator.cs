using UnityEngine;

public class GameManagerCreator : MonoBehaviour {

    public GameObject gameManagerPrefab;

    [HideInInspector]
    public GameObject gameManagerObject;

	void Awake () {

        if (!GameObject.Find("_GameManager(Clone)"))
        {
            gameManagerObject = Instantiate(gameManagerPrefab);
            return;
        }
        else
        {
            return;
        }
	}

    private void Start()
    {
        GUIController.instance.findMenuUI();
    }
}
