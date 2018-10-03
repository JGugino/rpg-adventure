using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager instance;

    [HideInInspector]
    public GameObject playerObject;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerObject = GameObject.Find("Player");
    }
}
