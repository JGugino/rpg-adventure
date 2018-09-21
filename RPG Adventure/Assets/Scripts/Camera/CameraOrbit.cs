using System.Collections;
using System.Collections.Generic;
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
        #region Camera Zoom Controls
        if (canZoom)
        {
            cameraFollow.currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

            cameraFollow.currentZoom = Mathf.Clamp(cameraFollow.currentZoom, minZoom, maxZoom);
        }
        #endregion

        #region Vertical Camera Movement
        float _direction = Input.GetAxis("Vertical");

        if (_direction < 0 && currentTilt > -maxXTilt)
        {
            currentTilt--;
            transform.Rotate(new Vector3(verRotateSpeed * Time.deltaTime, 0, 0));
        }
        else if (_direction > 0 && currentTilt < maxXTilt)
        {
            currentTilt++;
            transform.Rotate(new Vector3(-verRotateSpeed * Time.deltaTime, 0, 0));
        }
        #endregion
    }

    private void LateUpdate () {

        #region Horizontal Camera Controls
        float _direction = Input.GetAxis("Horizontal");

        if (_direction < 0)
        {
            transform.RotateAround(cameraFollow.target.position, Vector3.up, horRotateSpeed * Time.deltaTime);
        }
        else if (_direction > 0)
        {
            transform.RotateAround(cameraFollow.target.position, Vector3.up, -horRotateSpeed * Time.deltaTime);
        }
        #endregion
    }
}
