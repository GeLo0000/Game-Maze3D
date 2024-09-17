using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _heartImages;
    public GameObject _deathMenu;
    [SerializeField] private PauseControl _pauseManager;
    private int currentIndex;

    private AudioManager _audioManager;

    private void Awake()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        currentIndex = _heartImages.Length;
    }

    public void TakeDamage()
    {
        currentIndex --;
        _heartImages[currentIndex].SetActive(false);
        
        if (currentIndex <= 0)
        {
            _audioManager.PlaySFX(_audioManager.damage);
            _audioManager.PlaySFX(_audioManager.playerDeath);
            _audioManager.Mute();
            _pauseManager.Pause(_deathMenu);
        }
        else
        {
            _audioManager.PlaySFX(_audioManager.damage);
        }
    }
}
