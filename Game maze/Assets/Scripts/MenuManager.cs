using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Variable to store the previous menu
    private GameObject _previousMenu;

    private GameObject _nextMenu;

    // Method to open the information menu

    public void setCurrentMenu(GameObject currentMenu)
    {
        _previousMenu = currentMenu;
    }

    public void OpenNewMenu(GameObject _newMenu)
    {
        _nextMenu = _newMenu;

        // Disable the current menu
        _previousMenu.SetActive(false);

        // Enable the information menu
        _nextMenu.SetActive(true);
    }

    // Method to close the information menu and return to the previous one
    public void CloseNewMenu()
    {
        // Disable the information menu
        _nextMenu.SetActive(false);

        // Enable the previous menu (if one exists)
        if (_previousMenu != null)
        {
            _previousMenu.SetActive(true);
        }
    }
}
