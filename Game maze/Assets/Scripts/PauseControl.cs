using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    // Variable to track the paused state
    private bool _isPaused = false;

    // Reference to the pause menu UI
    [SerializeField] private GameObject _pauseMenuUI;
    [SerializeField] private GameObject _settingsMenuUI;
    [SerializeField] private GameObject _infoMenuUI;

    // Reference to the audio manager
    private AudioManager _audioManager;

    private bool _gameEnded = false;

    private void Awake()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    private void Update()
    {
        // Check if the Esc is pressed to toggle pause
        if (Input.GetKeyDown(KeyCode.Escape) && _gameEnded == false)
        {
            if (_isPaused)
            {
                ResumeGame(_pauseMenuUI);
                if (_settingsMenuUI.activeSelf)
                    _settingsMenuUI.SetActive(false);
                if (_infoMenuUI.activeSelf)
                    _infoMenuUI.SetActive(false);
            }
            else
            {
                _audioManager.PlaySFX(_audioManager.pause);
                Pause(_pauseMenuUI);
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

        setGameEnded(false);
    }

    // Pauses the game by showing the pause menu, freezing time, and showing the cursor
    public void Pause(GameObject menuUI)
    {
        menuUI.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
        Cursor.visible = true;  // Show the cursor when the game is paused
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor so the player can move it
        
        _audioManager.soundMoveSource.Pause();
    }
    public void setGameEnded(bool isEnd)
    {
        _gameEnded = isEnd;
    }
}
