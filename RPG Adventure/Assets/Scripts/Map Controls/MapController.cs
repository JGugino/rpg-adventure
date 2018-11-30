using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    public Transform target;

    public int minZoom = 5, maxZoom = 150;

    public float zoomSpeed;

    private void Update()
    {
        if (GUIController.instance.mapObject != null)
        {
            if (GUIController.instance.mapOpen)
            {
                float direction = Input.GetAxis("Mouse ScrollWheel");

                if (GUIController.instance.mapCamera.orthographicSize >= minZoom && GUIController.instance.mapCamera.orthographicSize <= maxZoom)
                {
                    float size = Mathf.Round(direction * zoomSpeed);

                    GUIController.instance.mapCamera.orthographicSize -= size;
                }

                if (GUIController.instance.mapCamera.orthographicSize < minZoom)
                {
                    GUIController.instance.mapCamera.orthographicSize = minZoom;
                }

                if (GUIController.instance.mapCamera.orthographicSize > maxZoom)
                {
                    GUIController.instance.mapCamera.orthographicSize = maxZoom;
                }
            }
        }
    }

    void LateUpdate () {

        if (GUIController.instance.mapObject != null)
        {
            if (GUIController.instance.mapOpen)
            {
                PlayerController pc = PlayerManager.instance.playerObject.GetComponent<PlayerController>();

                if (!pc.getControllingCreature())
                {
                    target = PlayerManager.instance.playerObject.transform;

                    GUIController.instance.mapCamera.transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(target.position.x, transform.position.y, target.position.z), Time.deltaTime * 3f);
                }
                else if (pc.getControllingCreature())
                {
                    target = InventoryController.instance.getEquippedCreature().transform;

                    GUIController.instance.mapCamera.transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(target.position.x, transform.position.y, target.position.z), Time.deltaTime * 3f);
                } 
            }
        }
	}
}
