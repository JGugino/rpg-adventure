using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public static CameraFollow instance;

    public Transform target;

    public float smoothSpeed = 0.8f, rotateSpeed = 20f, pitch = 2f;

    public Vector3 cameraOffset;

    private Transform cameraTransform;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cameraTransform = GetComponent<Transform>();
    }

    private void LateUpdate () {
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, target.position + cameraOffset, smoothSpeed);

        float direction = Input.GetAxis("Horizontal");

        cameraTransform.RotateAround(target.position, Vector3.up * direction, rotateSpeed * Time.deltaTime);

       // cameraTransform.LookAt(target.position + Vector3.up * pitch);
    }
}
