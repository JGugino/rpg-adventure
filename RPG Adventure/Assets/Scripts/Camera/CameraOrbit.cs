using UnityEngine;

public class CameraOrbit : MonoBehaviour {

    private CameraFollow cameraFollow;

    public float horRotateSpeed = 20f, verRotateSpeed = 20f, zoomSpeed = 5f, minZoom = 0.1f, maxZoom = 16f;

    public bool canZoom = false;

    [SerializeField]
    private int maxXTilt = 25, currentTilt = 0;

    private void Start()
    {
        cameraFollow = GetComponentInParent<CameraFollow>();
    }

    private void Update()
    {
        if (!GameController.instance.isPaused)
        {
            #region Camera Zoom Controls
            if (canZoom)
            {
                cameraFollow.currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

                cameraFollow.currentZoom = Mathf.Clamp(cameraFollow.currentZoom, minZoom, maxZoom);
            }
            #endregion
        }
    }

    private void LateUpdate () {

        if (!GameController.instance.isPaused)
        {
            #region Horizontal Camera Controls
            float _directionHor = Input.GetAxis("Horizontal");

            if (_directionHor > 0)
            {
                if (!PlayerManager.instance.playerObject.GetComponent<PlayerController>().getControllingCreature())
                {
                    transform.RotateAround(cameraFollow.target.position, Vector3.up, -horRotateSpeed * Time.deltaTime);
                }
                else if (PlayerManager.instance.playerObject.GetComponent<PlayerController>().getControllingCreature())
                {

                    transform.RotateAround(InventoryController.instance.equippedCreature.transform.position, Vector3.up, -horRotateSpeed * Time.deltaTime);
                }

            }
            else if (_directionHor < 0)
            {
                if (!PlayerManager.instance.playerObject.GetComponent<PlayerController>().getControllingCreature())
                {
                    transform.RotateAround(cameraFollow.target.position, Vector3.up, horRotateSpeed * Time.deltaTime);
                }
                else if (PlayerManager.instance.playerObject.GetComponent<PlayerController>().getControllingCreature())
                {
                    transform.RotateAround(InventoryController.instance.equippedCreature.transform.position, Vector3.up, horRotateSpeed * Time.deltaTime);
                }
            }
            #endregion

            #region Vertical Camera Movement
            float _directionVert = Input.GetAxis("Vertical");

            if (_directionVert < 0 && currentTilt > -maxXTilt)
            {
                currentTilt--;

                if (!PlayerManager.instance.playerObject.GetComponent<PlayerController>().getControllingCreature())
                {
                    transform.RotateAround(cameraFollow.target.position, transform.TransformDirection(Vector3.right), -verRotateSpeed * Time.deltaTime);
                }
                else if (PlayerManager.instance.playerObject.GetComponent<PlayerController>().getControllingCreature())
                {
                    transform.RotateAround(InventoryController.instance.equippedCreature.transform.position, transform.TransformDirection(Vector3.right), -verRotateSpeed * Time.deltaTime);
                }
            }
            else if (_directionVert > 0 && currentTilt < maxXTilt)
            {
                currentTilt++;

                if (!PlayerManager.instance.playerObject.GetComponent<PlayerController>().getControllingCreature())
                {
                    transform.RotateAround(cameraFollow.target.position, transform.TransformDirection(Vector3.right), verRotateSpeed * Time.deltaTime);
                }
                else if (PlayerManager.instance.playerObject.GetComponent<PlayerController>().getControllingCreature())
                {

                    transform.RotateAround(InventoryController.instance.equippedCreature.transform.position, transform.TransformDirection(Vector3.right), verRotateSpeed * Time.deltaTime);
                }
            }
            #endregion
        }
    }
}
