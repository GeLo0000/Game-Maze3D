using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCharacter : MonoBehaviour
{
    // Mouse sensitivity
    [SerializeField] private float _mouseSensitivity = 175f;

    // Reference to the character's body
    [SerializeField] private Transform _playerBody;

    // Reference to the desired camera position
    [SerializeField] private Transform _cameraPos;

    [SerializeField] private float _maxAngle = 70f;

    [SerializeField] private float _minAngle = -90f;

    // To store vertical rotation (looking up/down)
    private float _verticalRotation = 0f;

    // To store horizontal rotation (looking left/right)
    private float _horizontalRotation = 0f;

    private void Start()
    {
        // Hide and lock the cursor in the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Follow the camera position attached to the character
        transform.position = _cameraPos.position;

        // Get mouse movement values for both axes
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        // Adjust the vertical rotation based on vertical mouse movement (up/down)
        _verticalRotation -= mouseY;

        // Clamp vertical rotation
        _verticalRotation = Mathf.Clamp(_verticalRotation, _minAngle, _maxAngle);

        // Adjust the horizontal rotation based on horizontal mouse movement (left/right)
        _horizontalRotation += mouseX;

        // Apply the vertical rotation to the camera (up/down look)
        transform.localRotation = Quaternion.Euler(_verticalRotation, _horizontalRotation, 0f);

        // Apply the horizontal rotation to the player body
        _playerBody.rotation = Quaternion.Euler(0f, _horizontalRotation, 0f);
    }
}
