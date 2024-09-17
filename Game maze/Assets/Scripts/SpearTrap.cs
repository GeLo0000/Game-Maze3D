using System.Collections;
using UnityEngine;

public class SpearTrap : MonoBehaviour
{
    [SerializeField] private GameObject spearPrefab; // Префаб списа
    [SerializeField] private Transform spawnPoint; // Точка спавну списа
    [SerializeField] private float shootInterval = 2f; // Інтервал між вистрілами
    [SerializeField] private float spearSpeed = 10f; // Швидкість польоту списа
    [SerializeField] private float spearLifetime = 5f; // Час життя списа
    [SerializeField] private AudioSource shootSound; // Звук пострілу

    private void Start()
    {
        // Починаємо цикл вистрілів
        StartCoroutine(ShootSpearRoutine());
    }

    private IEnumerator ShootSpearRoutine()
    {
        while (true)
        {
            // Створюємо спис на позиції спавну
            GameObject spear = Instantiate(spearPrefab, spawnPoint.position, spawnPoint.rotation);
            Rigidbody rb = spear.GetComponent<Rigidbody>();

            // Задаємо йому напрямок і швидкість
            if (rb != null)
            {
                rb.velocity = -spawnPoint.up * spearSpeed;
            }

            // Відтворюємо звук пострілу
            if (shootSound != null)
            {
                shootSound.Play();
            }

            // Знищуємо спис через певний час, щоб не накопичувались об'єкти
            Destroy(spear, spearLifetime);

            // Чекаємо наступний інтервал перед вистрілом
            yield return new WaitForSeconds(shootInterval);
        }
    }
}
