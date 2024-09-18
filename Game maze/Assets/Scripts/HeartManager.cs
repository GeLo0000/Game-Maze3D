using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    // Array of heart image GameObjects
    [SerializeField] private GameObject[] _heartImages;

    // Death menu GameObject to display on player death
    public GameObject deathMenu;

    // Pause manager for handling game pause
    [SerializeField] private PauseControl _pauseManager;

    // Tracks the current index of hearts
    private int currentIndex;

    // Reference to the AudioManager for playing sounds
    private AudioManager _audioManager;

    private void Awake()
    {
        // Initialize AudioManager and set the current heart index
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        currentIndex = _heartImages.Length;
    }

    // Method called when the player takes damage
    public void TakeDamage()
    {
        // Decrement the current heart index and hide the current heart image
        currentIndex--; 
        _heartImages[currentIndex].SetActive(false);

        // Check if player is out of hearts
        if (currentIndex <= 0)
        {
            // Play damage sound effect and death sound effect and mute background sounds
            _audioManager.PlaySFX(_audioManager.damage);
            _audioManager.PlaySFX(_audioManager.playerDeath);
            _audioManager.Mute();

            // Destroy all projectiles and display the death menu and pause the game
            DestroyAllProjectiles();
            _pauseManager.Pause(deathMenu);
            _pauseManager.setGameEnded(true);
        }
        else
        {
            // Play damage sound effect if player isn't out of hearts
            _audioManager.PlaySFX(_audioManager.damage);
        }
    }

    // Method to destroy all projectiles in the scene
    private void DestroyAllProjectiles()
    {
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (var projectile in projectiles)
        {
            Destroy(projectile.gameObject);
        }
    }
}
