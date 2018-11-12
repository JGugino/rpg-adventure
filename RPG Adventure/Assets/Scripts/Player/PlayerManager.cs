using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager instance;

    public GameObject playerPrefab;

    [HideInInspector]
    public GameObject playerObject;

    public GameObject playerSpawn;

    private void Awake()
    {
        instance = this;

        if (playerObject == null)
        {
            playerObject = Instantiate(playerPrefab, playerSpawn.transform.position, Quaternion.identity);
        }
    }
}
