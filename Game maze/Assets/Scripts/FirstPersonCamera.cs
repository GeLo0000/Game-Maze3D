using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    // Mouse sensitivity
    [SerializeField] private float mouseSensitivity = 175f;

    // Reference to the character's body
    [SerializeField] private Transform playerBody;

    // Reference to the desired camera position (attached to the character)
    [SerializeField] private Transform cameraPos;

    [SerializeField] private float maxAngle = 70f;

    [SerializeField] private float minAngle = -90f;

    // To store vertical rotation (looking up/down)
    private float verticalRotation = 0f;

    // To store horizontal rotation (looking left/right)
    private float horizontalRotation = 0f;

    void Start()
    {
        // Hide and lock the cursor in the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Follow the camera position attached to the character
        transform.position = cameraPos.position;

        // Get mouse movement values for both axes
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Adjust the vertical rotation based on vertical mouse movement (up/down)
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, minAngle, maxAngle); // Clamp vertical rotation

        // Adjust the horizontal rotation based on horizontal mouse movement (left/right)
        horizontalRotation += mouseX;

        // Apply the vertical rotation to the camera (up/down look)
        transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);

        // Apply the horizontal rotation to the player body
        playerBody.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);
    }
}
