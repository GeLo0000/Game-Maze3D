using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Finish : MonoBehaviour
{
    // Reference to the finish menu UI
    public GameObject finishMenu;
    [SerializeField] private PauseControl _pauseManager;

    // Layer name for the player
    private const string _playerLayer = "Player";

    [SerializeField] private KeysManager _keysManager;
    [SerializeField] private GameObject _keyLine;
    [SerializeField] private Timer _timer;
    [SerializeField] private TMP_Text _finishText;

    private AudioManager _audioManager;

    // Initialize audio manager reference
    private void Awake()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Check if all keys are collected and hide key line
    private void Update()
    {
        if (_keysManager.GetCountKey() == _keysManager.GetMaxCountKey())
        {
            _keyLine.SetActive(false);
        }
    }

    // Handle player reaching the finish
    private void OnTriggerEnter(Collider other)
    {
        int layer = other.gameObject.layer;

        // Check if the player has reached the finish
        if (LayerMask.LayerToName(layer) == _playerLayer)
        {
            // Display finish time and show finish menu
            _finishText.text = _finishText.text + _timer.GetTimer();
            _pauseManager.Pause(finishMenu);
            _audioManager.PlayMusic(_audioManager.finish);
        }
    }
}
