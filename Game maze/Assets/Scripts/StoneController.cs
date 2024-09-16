using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneController : MonoBehaviour
{
    private const string _wallLayer = "Wall";
    private const string _playerLayer = "Player";

    private AudioManager _audioManager;

    private void Awake()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _audioManager.PlaySFX(_audioManager.flyStone);
    }

    // Метод, що викликається при доторканні до інших об'єктів
    private void OnCollisionEnter(Collision collision)
    {
        int layer = collision.gameObject.layer;
        // Перевірка, чи доторкнулися до стіни
        if (LayerMask.LayerToName(layer) == _wallLayer || LayerMask.LayerToName(layer) == _playerLayer)
        {
            _audioManager.PlaySFX(_audioManager.destroyStone);
            Destroy(gameObject);
        }
    }
}
