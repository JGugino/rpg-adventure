using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    public Transform target;

    public float zoomSpeed;

    private void Update()
    {
        if (GUIController.instance.mapOpen)
        {
            float direction = Input.GetAxis("Mouse ScrollWheel");

            if (GUIController.instance.mapCamera.orthographicSize >= 5 && GUIController.instance.mapCamera.orthographicSize <= 250)
            {
                float size = Mathf.Round(direction * zoomSpeed);

                GUIController.instance.mapCamera.orthographicSize -= size;

                Debug.Log("Size: " + GUIController.instance.mapCamera.orthographicSize);
            }

            if (GUIController.instance.mapCamera.orthographicSize < 5)
            {
                GUIController.instance.mapCamera.orthographicSize = 5;
            }

            if (GUIController.instance.mapCamera.orthographicSize > 250)
            {
                GUIController.instance.mapCamera.orthographicSize = 250;
            }
        }

        if (GUIController.instance.minimapOpen)
        {
            float direction = Input.GetAxis("Mouse ScrollWheel");

            if (GUIController.instance.minimapCamera.orthographicSize >= 5 && GUIController.instance.minimapCamera.orthographicSize <= 30)
            {
                float size = Mathf.Round(direction * zoomSpeed);

                GUIController.instance.minimapCamera.orthographicSize -= size;

                Debug.Log("Size: " + GUIController.instance.minimapCamera.orthographicSize);
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

    void LateUpdate () {

        transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(target.position.x, transform.position.y, target.position.z), Time.deltaTime * 3f);
	}
}
