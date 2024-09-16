using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Finish : MonoBehaviour
{
    public GameObject _finishMenu;
    [SerializeField] private PauseControl _pauseManager;

    // Layer names
    private const string _playerLayer = "Player";

    [SerializeField] private KeysManager _keysManager;

    [SerializeField] private GameObject _keyLine;

    [SerializeField] private Timer _timer;

    [SerializeField] private TMP_Text _finishText;

    private AudioManager _audioManager;

    private void Awake()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (_keysManager.GetCountKey() == _keysManager.GetMaxCountKey())
        {
            _keyLine.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        int layer = other.gameObject.layer;

        if (LayerMask.LayerToName(layer) == _playerLayer)
        {
            _finishText.text = _finishText.text + _timer.GetTimer();
            _pauseManager.Pause(_finishMenu);
            _audioManager.PlayMusic(_audioManager.finish);
        }
    }
}
