using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneController : MonoBehaviour
{
    // Layer names for wall and player objects
    private const string _wallLayer = "Wall";
    private const string _playerLayer = "Player";

    // Audio source and clips for stone sounds
    [SerializeField] private AudioSource _stoneSound;
    [SerializeField] private AudioClip _stoneDestroy;
    [SerializeField] private AudioClip _stoneFly;

    // Rigidbody for controlling the stone's movement
    private Rigidbody _rigidbody;

    // Get the Rigidbody component at the start
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update method checks for stone movement and manages sound
    private void Update()
    {
        if (_rigidbody.velocity.magnitude > 0.1f)
        {
            if (!_stoneSound.isPlaying)
            {
                _stoneSound.clip = _stoneFly;
                _stoneSound.Play();
            }
        }
        else
        {
            if (_stoneSound.isPlaying && _stoneSound.clip == _stoneFly)
            {
                _stoneSound.Stop();
            }
        }
    }

    // Trigger event for collisions with other objects
    private void OnTriggerEnter(Collider other)
    {
        int layer = other.gameObject.layer;

        // If the stone hits a wall
        if (LayerMask.LayerToName(layer) == _wallLayer)
        {
            _stoneSound.clip = _stoneDestroy;
            _stoneSound.Play();
            Destroy(gameObject, _stoneDestroy.length);
        }

        // If the stone hits the player
        if (LayerMask.LayerToName(layer) == _playerLayer)
        {
            Destroy(gameObject);
        }
    }

    // Trigger exit event for stopping the stone when leaving the wall
    private void OnTriggerExit(Collider other)
    {
        int layer = other.gameObject.layer;

        if (LayerMask.LayerToName(layer) == _wallLayer)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }
}
