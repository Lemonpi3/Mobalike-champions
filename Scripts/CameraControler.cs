using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{

    public Transform playerPos;

    public Camera cam;
    private Vector3 camOffset;
    public float zoomSpeed;
    private float camFOV;
    private float mouseScrollInput;
    [Range(0.01f, 1f)]
    public float cameraSmooth=0.5f;

    private void Start()
    {
        camOffset = transform.position - playerPos.position;
        camFOV = cam.fieldOfView;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = playerPos.position + camOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, cameraSmooth);
        mouseScrollInput = Input.GetAxis("Mouse ScrollWheel");

        camFOV -= mouseScrollInput * zoomSpeed;
        camFOV = Mathf.Clamp(camFOV, 30, 60);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, camFOV, zoomSpeed);
    }
}
