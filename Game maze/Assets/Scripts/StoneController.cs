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
        // �������� Rigidbody ��� �������� ��������
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // ���� ����� ��������
        if (_rigidbody.velocity.magnitude > 0.1f)
        {
            // ���� ���� ������� �� ���, ��������� ����
            if (!_stoneSound.isPlaying)
            {
                _stoneSound.clip = _stoneFly;
                _stoneSound.Play();
            }
        }
        else
        {
            // ���� ����� ���������, ��������� ���� �������
            if (_stoneSound.isPlaying && _stoneSound.clip == _stoneFly)
            {
                _stoneSound.Stop();
            }
        }
    }

    // �����, �� ����������� ��� ���������� �� ����� ��'����
    private void OnTriggerEnter(Collider other)
    {
        int layer = other.gameObject.layer;

        if (LayerMask.LayerToName(layer) == _wallLayer)
        {
            // ³��������� ���� ��������
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
        // ��������, �� ������������ �� ����
        if (LayerMask.LayerToName(layer) == _wallLayer)
        {
            // ������� ���� ������
            _rigidbody.velocity = Vector3.zero; // ��������� ��������
            _rigidbody.angularVelocity = Vector3.zero; // ��������� ������ �������� (���� ���������)
        }
    }
}
