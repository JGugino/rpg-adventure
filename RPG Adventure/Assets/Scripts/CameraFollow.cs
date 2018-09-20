using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public static CameraFollow instance;

    public Transform target;

    public float pitch = 2f;

    public Vector3 cameraOffset;

    private Transform cameraTransform;

    public float currentZoom = 3f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cameraTransform = GetComponent<Transform>();
    }

    private void LateUpdate () {

        cameraTransform.position = target.position - cameraOffset * currentZoom;

        cameraTransform.LookAt(target.position + Vector3.up * pitch);
    }
}
