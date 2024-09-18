using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Variable to store the previous menu
    private GameObject _previousMenu;

    // Variable to store the next menu
    private GameObject _nextMenu;

    // Method to set previous menu
    public void setCurrentMenu(GameObject currentMenu)
    {
        _previousMenu = currentMenu;
    }

    // Method to open next menu
    public void OpenNewMenu(GameObject _newMenu)
    {
        _nextMenu = _newMenu;

        // Disable current menu
        _previousMenu.SetActive(false);

        // Enable next menu
        _nextMenu.SetActive(true);
    }

    // Method to close next menu and return to the previous one
    public void CloseNewMenu()
    {
        // Disable next menu
        _nextMenu.SetActive(false);

        // Enable previous menu (if one exists)
        if (_previousMenu != null)
        {
            _previousMenu.SetActive(true);
        }
    }
}
