using UnityEngine;

public class GameExit : MonoBehaviour
{
    // Method to exit the game
    public void ExitGame()
    {
        // Check if the game is running in the Unity Editor
#if UNITY_EDITOR
        // If the game is running in the editor, stop the play mode
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Quit the application
        Application.Quit();
#endif
        // Log the exit event for debugging purposes
        Debug.Log("Game is exiting");
    }
}
