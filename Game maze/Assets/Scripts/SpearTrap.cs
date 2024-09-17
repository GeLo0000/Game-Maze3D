using System.Collections;
using UnityEngine;

public class SpearTrap : MonoBehaviour
{
    [SerializeField] private GameObject spearPrefab; // ������ �����
    [SerializeField] private Transform spawnPoint; // ����� ������ �����
    [SerializeField] private float shootInterval = 2f; // �������� �� ���������
    [SerializeField] private float spearSpeed = 10f; // �������� ������� �����
    [SerializeField] private float spearLifetime = 5f; // ��� ����� �����
    [SerializeField] private AudioSource shootSound; // ���� �������

    private void Start()
    {
        // �������� ���� �������
        StartCoroutine(ShootSpearRoutine());
    }

    private IEnumerator ShootSpearRoutine()
    {
        while (true)
        {
            // ��������� ���� �� ������� ������
            GameObject spear = Instantiate(spearPrefab, spawnPoint.position, spawnPoint.rotation);
            Rigidbody rb = spear.GetComponent<Rigidbody>();

            // ������ ���� �������� � ��������
            if (rb != null)
            {
                rb.velocity = -spawnPoint.up * spearSpeed;
            }

            // ³��������� ���� �������
            if (shootSound != null)
            {
                shootSound.Play();
            }

            // ������� ���� ����� ������ ���, ��� �� �������������� ��'����
            Destroy(spear, spearLifetime);

            // ������ ��������� �������� ����� ��������
            yield return new WaitForSeconds(shootInterval);
        }
    }
}
