using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class CameraController : MonoBehaviour
{
    public Transform player; // Assign the player object in the Inspector
    public float distanceFromPlayer = 7f; // Distance of the camera from the player
    public float maxVerticalAngle = 80f, minVerticalAngle = -80f; // Limits for vertical rotation
    public float turnSpeed = 100f; // Speed of camera rotation

    private float verticalAngle = 0f; // Tracks vertical rotation
    private float horizontalAngle = 0f; // Tracks horizontal rotation

    void Start()
    {
        verticalAngle = transform.eulerAngles.x; // Initialize with current camera angle
        horizontalAngle = transform.eulerAngles.y; // Initialize with current camera angle
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) // Rotate when holding left mouse button
        {
            float mouseY = Input.GetAxis("Mouse Y");
            float mouseX = Input.GetAxis("Mouse X");

            verticalAngle = Mathf.Clamp(verticalAngle - mouseY * turnSpeed * Time.deltaTime, minVerticalAngle, maxVerticalAngle);
            horizontalAngle += mouseX * turnSpeed * Time.deltaTime;

            UpdateCameraPosition();
        }

        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        // Calculate the new camera position
        Vector3 offset = new Vector3(0, 0, -distanceFromPlayer);
        Quaternion rotation = Quaternion.Euler(verticalAngle, horizontalAngle, 0);

        // Update camera position and rotation based on the player's position
        transform.position = player.position + rotation * offset;

        // Make sure the camera is always looking at the player
        transform.LookAt(player.position);
    }
}