using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameObject _finishMenu;
    [SerializeField] private PauseControl _pauseManager;

    // Layer names
    private const string _playerLayer = "Player";

    private AudioManager _audioManager;

    private void Awake()
    {
        //_audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void OnTriggerExit(Collider other)
    {
        int layer = other.gameObject.layer;

        if (LayerMask.LayerToName(layer) == _playerLayer)
        {
            //_audioManager.PlaySFX(_audioManager.finish);
            _pauseManager.Pause(_finishMenu);
        }
    }
}
