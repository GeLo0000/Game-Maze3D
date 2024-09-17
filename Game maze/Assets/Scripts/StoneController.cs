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
        // ќтримуЇмо Rigidbody дл€ перев≥рки швидкост≥
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // якщо кам≥нь рухаЇтьс€
        if (_rigidbody.velocity.magnitude > 0.1f)
        {
            // якщо звук польоту не граЇ, запускаЇмо його
            if (!_stoneSound.isPlaying)
            {
                _stoneSound.clip = _stoneFly;
                _stoneSound.Play();
            }
        }
        else
        {
            // якщо кам≥нь зупинивс€, зупин€Їмо звук польоту
            if (_stoneSound.isPlaying && _stoneSound.clip == _stoneFly)
            {
                _stoneSound.Stop();
            }
        }
    }

    // ћетод, що викликаЇтьс€ при доторканн≥ до ≥нших об'Їкт≥в
    private void OnCollisionEnter(Collision collision)
    {
        int layer = collision.gameObject.layer;
        // ѕерев≥рка, чи доторкнулис€ до ст≥ни або гравц€
        if (LayerMask.LayerToName(layer) == _wallLayer)
        {
            StartCoroutine(DestroyStoneWithDelay());
        }
        if (LayerMask.LayerToName(layer) == _playerLayer)
        {
            Destroy(gameObject);
        }
    }

    //  орутина дл€ знищенн€ камен€ п≥сл€ затримки
    private IEnumerator DestroyStoneWithDelay()
    {
        // ¬≥дтворюЇмо звук знищенн€
        _stoneSound.PlayOneShot(_stoneDestroy);

        // ќч≥куЇмо тривал≥сть звуку перед знищенн€м камен€
        yield return new WaitForSeconds(_stoneDestroy.length);

        // «нищуЇмо кам≥нь
        Destroy(gameObject);
    }
}
