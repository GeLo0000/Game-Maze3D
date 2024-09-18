using UnityEngine;

public class CameraSwap : MonoBehaviour
{
    // Reference to the cameras
    [SerializeField] private GameObject _firstPersonCamera;
    [SerializeField] private GameObject _thirdPersonCamera;
    [SerializeField] private Camera _firstPersonUICamera;
    [SerializeField] private Camera _thirdPersonUICamera;

    // Array of canvases to be updated
    [SerializeField] private Canvas[] _canvases;

    // Reference to the pause control manager
    [SerializeField] private PauseControl _pauseControl;

    // Reference to the settings menu game object
    [SerializeField] private GameObject _settingsMenu;

    // Called when the button to activate first-person view is pressed
    public void ActivateFirstPersonView()
    {
        _firstPersonCamera.SetActive(true);
        _thirdPersonCamera.SetActive(false);

        // Update all canvases to use the first-person UI camera
        SetCanvases(_firstPersonUICamera);
    }

    // Called when the button to activate third-person view is pressed
    public void ActivateThirdPersonView()
    {
        _thirdPersonCamera.SetActive(true);
        _firstPersonCamera.SetActive(false);

        // Update all canvases to use the third-person UI camera
        SetCanvases(_thirdPersonUICamera);
    }

    // Updates all canvases with the new UI camera
    private void SetCanvases(Camera UICamera)
    {
        // Resume the game and close the settings menu
        _pauseControl.ResumeGame(_settingsMenu);

        // Set the world camera for each canvas
        foreach (Canvas canvas in _canvases)
        {
            canvas.worldCamera = UICamera;
        }
    }
}
