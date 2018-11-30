using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager instance;

    public GameObject playerPrefab;

    [HideInInspector]
    public GameObject playerObject;

    public GameObject playerSpawn;

    [HideInInspector]
    public PlayerController playerController;

    private void Awake()
    {
        instance = this;
    }

    public void spawnPlayer()
    {
        if (playerObject == null)
        {
            playerObject = Instantiate(playerPrefab, playerSpawn.transform.position, Quaternion.identity);

            playerController = playerObject.GetComponent<PlayerController>();

            CameraFollow.instance.target = playerObject.transform;
        }
    }
}
