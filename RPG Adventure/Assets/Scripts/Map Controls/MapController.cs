using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    public Transform target;

    public int minZoom = 5, maxZoom = 150;

    public float zoomSpeed;

    private void Update()
    {
        if (GUIController.instance.mapObject != null && GUIController.instance.minimapObject != null)
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

            if (GUIController.instance.minimapOpen)
            {
                float direction = Input.GetAxis("Mouse ScrollWheel");

                if (GUIController.instance.minimapCamera.orthographicSize >= 5 && GUIController.instance.minimapCamera.orthographicSize <= 30)
                {
                    float size = Mathf.Round(direction * zoomSpeed);

                    GUIController.instance.minimapCamera.orthographicSize -= size;
                }

                if (GUIController.instance.minimapCamera.orthographicSize < 5)
                {
                    GUIController.instance.minimapCamera.orthographicSize = 5;
                }

                if (GUIController.instance.minimapCamera.orthographicSize > 30)
                {
                    GUIController.instance.minimapCamera.orthographicSize = 30;
                }
            }
        }
    }

    void LateUpdate () {

        if (GUIController.instance.mapObject != null && GUIController.instance.minimapObject != null)
        {
            target = PlayerManager.instance.playerObject.transform;

            GUIController.instance.mapCamera.transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(target.position.x, transform.position.y, target.position.z), Time.deltaTime * 3f);

            if (!PlayerManager.instance.playerObject.GetComponent<PlayerController>().getControllingCreature())
            {
                GUIController.instance.minimapCamera.transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(target.position.x, transform.position.y, target.position.z), Time.deltaTime * 3f);
            }
            else if (PlayerManager.instance.playerObject.GetComponent<PlayerController>().getControllingCreature())
            {
                GUIController.instance.minimapCamera.transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(InventoryController.instance.equippedCreature.transform.position.x, transform.position.y, InventoryController.instance.equippedCreature.transform.position.z), Time.deltaTime * 3f);
            }
        }
	}
}
