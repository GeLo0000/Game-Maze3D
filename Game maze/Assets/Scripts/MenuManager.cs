using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Variable to store the previous menu
    private GameObject _previousMenu;

    // References to info menu
    [SerializeField] private GameObject _infoMenu;

    // Method to open the information menu
    public void OpenInfoMenu(GameObject currentMenu)
    {
        // Store the reference to the currently open menu
        _previousMenu = currentMenu;

        // Disable the current menu
        currentMenu.SetActive(false);

        // Enable the information menu
        _infoMenu.SetActive(true);
    }

    // Method to close the information menu and return to the previous one
    public void CloseInfoMenu()
    {
        // Disable the information menu
        _infoMenu.SetActive(false);

        // Enable the previous menu (if one exists)
        if (_previousMenu != null)
        {
            _previousMenu.SetActive(true);
        }
    }
}
