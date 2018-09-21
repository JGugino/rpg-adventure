using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;

    public float pitch = 2f;

    public Vector3 cameraOffset;

    private Transform cameraTransform;

    public float currentZoom = 0.1f;

    #region Singleton
    public static CameraFollow instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    private void Start()
    {
        //Gets camera transform position
        cameraTransform = GetComponent<Transform>();
    }

    private void LateUpdate () {

        //Follows specified target
        cameraTransform.position = target.position + cameraOffset * currentZoom;

        //Makes camera point towards targets position
        cameraTransform.LookAt(target.position + Vector3.up * pitch);

    }
}
