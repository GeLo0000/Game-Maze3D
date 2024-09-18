using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    // Reference to the TextMeshPro text component for displaying the timer
    [SerializeField] private TMP_Text _timerText;

    // Variable to track the total time elapsed since the timer started
    private float _timeElapsed = 0f;

    // Updates the timer every frame
    private void Update()
    {
        _timeElapsed += Time.deltaTime;
        DisplayTime(_timeElapsed);
    }

    // Returns the current displayed timer as a string
    public string GetTimer()
    {
        return _timerText.text;
    }

    // Converts the time to minutes and seconds, then updates the UI text
    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); // Calculate minutes
        float seconds = Mathf.FloorToInt(timeToDisplay % 60); // Calculate seconds

        // Display the time in "MM:SS" format
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
