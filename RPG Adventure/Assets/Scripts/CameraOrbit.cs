using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour {

    private CameraFollow cameraFollow;

    public float rotateSpeed = 20f, zoomSpeed = 3f, minZoom = 1f, maxZoom = 18f;

    private void Start()
    {
        cameraFollow = GetComponentInParent<CameraFollow>();
    }

    private void Update()
    {
        cameraFollow.currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

        cameraFollow.currentZoom = Mathf.Clamp(cameraFollow.currentZoom, minZoom, maxZoom);
    }

    private void LateUpdate () {
        float direction = Input.GetAxis("Horizontal");

        if (direction < 0)
        {
            transform.RotateAround(cameraFollow.target.position, Vector3.up, -rotateSpeed * Time.deltaTime);
        }
        else if (direction > 0)
        {
            transform.RotateAround(cameraFollow.target.position, Vector3.up, rotateSpeed * Time.deltaTime);
        }
    }
}
