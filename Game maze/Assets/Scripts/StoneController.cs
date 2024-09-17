using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneController : MonoBehaviour
{
    private const string _wallLayer = "Wall";
    private const string _playerLayer = "Player";

    [SerializeField] private AudioSource _stoneSound;

    [SerializeField] private AudioClip _stoneDestroy;
    [SerializeField] private AudioClip _stoneFly;
    private Rigidbody _rigidbody;

    private void Start()
    {
        // Отримуємо Rigidbody для перевірки швидкості
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Якщо камінь рухається
        if (_rigidbody.velocity.magnitude > 0.1f)
        {
            // Якщо звук польоту не грає, запускаємо його
            if (!_stoneSound.isPlaying)
            {
                _stoneSound.clip = _stoneFly;
                _stoneSound.Play();
            }
        }
        else
        {
            // Якщо камінь зупинився, зупиняємо звук польоту
            if (_stoneSound.isPlaying && _stoneSound.clip == _stoneFly)
            {
                _stoneSound.Stop();
            }
        }
    }

    // Метод, що викликається при доторканні до інших об'єктів
    private void OnTriggerEnter(Collider other)
    {
        int layer = other.gameObject.layer;

        if (LayerMask.LayerToName(layer) == _wallLayer)
        {
            // Відтворюємо звук знищення
            _stoneSound.clip = _stoneDestroy;
            _stoneSound.Play();
            Destroy(gameObject, _stoneDestroy.length);
        }

        if (LayerMask.LayerToName(layer) == _playerLayer)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        int layer = other.gameObject.layer;
        // Перевірка, чи доторкнулися до стіни
        if (LayerMask.LayerToName(layer) == _wallLayer)
        {
            // Зупинка руху каменя
            _rigidbody.velocity = Vector3.zero; // Обнулення швидкості
            _rigidbody.angularVelocity = Vector3.zero; // Обнулення кутової швидкості (якщо необхідно)
        }
    }
}
