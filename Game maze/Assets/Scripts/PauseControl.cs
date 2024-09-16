using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    // Variable to track the paused state
    private bool _isPaused = false;

    // Reference to the pause menu UI
    [SerializeField] private GameObject _pauseMenuUI;

    // Reference to the audio manager (optional)
    private AudioManager _audioManager;

    // Called when the script instance is being loaded
    private void Awake()
    {
        //_audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Called once per frame
    private void Update()
    {
        // Check if the spacebar (or another key) is pressed to toggle pause
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isPaused)
            {
                ResumeGame(_pauseMenuUI);  // Resume the game if it's paused
            }
            else
            {
                Pause(_pauseMenuUI);  // Pause the game if it's running
            }
        }
    }

    // Resumes the game by hiding the pause menu, resetting time scale, and showing the cursor
    public void ResumeGame(GameObject menuUI)
    {
        menuUI.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
        Cursor.visible = false; // Hide the cursor when resuming the game
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor in the center of the screen
        //_audioManager.musicSource.Play(); // Resume the music (optional)
    }

    // Pauses the game by showing the pause menu, freezing time, and showing the cursor
    public void Pause(GameObject menuUI)
    {
        menuUI.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
        Cursor.visible = true;  // Show the cursor when the game is paused
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor so the player can move it
        //_audioManager.musicSource.Pause(); // Pause the music (optional)
    }
}
