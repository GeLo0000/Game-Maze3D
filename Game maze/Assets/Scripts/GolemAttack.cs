using UnityEngine;

public class GolemAttack : MonoBehaviour
{
    // ������� ���, �� ����� ���� ���������� �����
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;

    // ������ ������
    [SerializeField] private GameObject stonePrefab;
    // ����, � ���� �������� �����
    [SerializeField] private float throwForce = 10f;

    private GameObject _spawnedStone;
    private Animator _animator;
    private bool _isHoldingStone = false; // ���������, ��� ����������� �� ����� ����� �����

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
    }

    private void Update()
    {
        // ���� ����� ��������� � ���� ������� �������
        if (_spawnedStone != null && _isHoldingStone)
        {
            // ��������� ������� � ��������� ������ �� ����� ������, ���� ����� ���� ���
            _spawnedStone.transform.position = CalculateSpawnPoint();
            _spawnedStone.transform.rotation = CalculateSpawnRotation();
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
            _spawnedStone.transform.rotation = Quaternion.LookRotation(transform.forward);

            // �������� Rigidbody ������ � ������ ���� ��� �����
            Rigidbody rb = _spawnedStone.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
            }
        }
    }
}

