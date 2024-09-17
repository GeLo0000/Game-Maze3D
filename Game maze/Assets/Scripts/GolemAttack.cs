using UnityEngine;

public class GolemAttack : MonoBehaviour
{
    // ������� ���, �� ����� ���� ���������� �����
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;

    // ������ ������
    [SerializeField] private GameObject stonePrefab;
    // ����, � ���� �������� �����
    [SerializeField] private float throwForce = 15f;

    // ���� ��� ������ ������
    [SerializeField] private Vector3 minStoneScale = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField] private Vector3 maxStoneScale = new Vector3(1f, 1f, 1f);
    [SerializeField] private float growthDuration = 1f; // ���, �� ���� ����� ������ ������������� ������

    private GameObject _spawnedStone;
    private Animator _animator;
    private bool _isHoldingStone = false; // ���������, ��� ����������� �� ����� ����� �����
    private bool _holdOnRightHand = false; // ���������, ��� ��������� ����� �� ����� ����
    private float _growthStartTime;

    [SerializeField] private AudioSource _golemSound;

    [SerializeField] private AudioClip _throwSound;
    [SerializeField] private AudioClip _idleSound;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // ����� ��� ���������� �������� ����� �� ����� ������
    private Vector3 CalculateSpawnPoint()
    {
        return (leftHand.position + rightHand.position) / 2;
    }

    // ����� ��� ���������� ��������� ������, ������� �� ������� ���
    private Quaternion CalculateSpawnRotation()
    {
        Vector3 direction = (rightHand.position - leftHand.position).normalized;
        return Quaternion.LookRotation(direction);
    }

    // �����, ���� ����������� �� ��� ����� ������ (� �������)
    public void SpawnStone()
    {
        // ��������� ����� �� ������ � ���������� �� ������
        Vector3 spawnPoint = CalculateSpawnPoint();
        Quaternion spawnRotation = CalculateSpawnRotation();
        _spawnedStone = Instantiate(stonePrefab, spawnPoint, spawnRotation);
        _isHoldingStone = true;
        _holdOnRightHand = false; // ����� ��� �� ��������� �� ����� ������

        // �����'������� ��� ������� �����
        _growthStartTime = Time.time;

        _golemSound.PlayOneShot(_idleSound);
    }

    // �����, �� ����� ����� �� ����� ����
    public void HoldStoneOnRightHand()
    {
        _holdOnRightHand = true; // Գ����� ����� �� ����� ����
        _golemSound.PlayOneShot(_throwSound);
    }

    private void Update()
    {
        // ���� ����� ��������� � ���� ������� �������
        if (_spawnedStone != null && _isHoldingStone)
        {
            if (_holdOnRightHand)
            {
                // ���� ��������� ������������, ������� ����� �� ����� ����
                _spawnedStone.transform.position = rightHand.position;
            }
            else
            {
                // ��������� ������� � ��������� ������ �� ����� ������, ���� ����� ���� ���
                _spawnedStone.transform.position = CalculateSpawnPoint();
                _spawnedStone.transform.rotation = CalculateSpawnRotation();
            }

            // ���������� ������ ������ ������
            float growthProgress = (Time.time - _growthStartTime) / growthDuration;
            if (growthProgress < 1f)
            {
                // ���������� ������ �� ����� ����
                _spawnedStone.transform.localScale = Vector3.Lerp(minStoneScale, maxStoneScale, growthProgress);
            }
            else
            {
                // ����� ����� ������������� ��������
                _spawnedStone.transform.localScale = maxStoneScale;
            }
        }
    }

    // �����, ���� ����������� �� ��� ������ (� �������)
    public void ThrowStone()
    {
        if (_spawnedStone != null)
        {
            // ������ ���������, ��� ����� �� ����'������� ����� �� ���
            _isHoldingStone = false;

            // ������� ����� � ��������, �� ���� �������� �����
            // _spawnedStone.transform.rotation = Quaternion.LookRotation(transform.forward);

            // �������� Rigidbody ������ � ������ ���� ��� �����
            Rigidbody rb = _spawnedStone.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(transform.right * throwForce, ForceMode.Impulse);
            }
        }
    }
}
