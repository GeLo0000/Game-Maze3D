using UnityEngine;

public class GolemAttack : MonoBehaviour
{
    // Позиції рук, між якими буде спавнитися камінь
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;

    // Префаб каменя
    [SerializeField] private GameObject stonePrefab;
    // Сила, з якою кидається камінь
    [SerializeField] private float throwForce = 10f;

    private GameObject _spawnedStone;
    private Animator _animator;
    private bool _isHoldingStone = false; // Прапорець, щоб відстежувати чи голем тримає камінь

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Метод для визначення середньої точки між двома руками
    private Vector3 CalculateSpawnPoint()
    {
        return (leftHand.position + rightHand.position) / 2;
    }

    // Метод для визначення обертання каменя, залежно від позицій рук
    private Quaternion CalculateSpawnRotation()
    {
        Vector3 direction = (rightHand.position - leftHand.position).normalized;
        return Quaternion.LookRotation(direction);
    }

    // Метод, який викликається під час тертя руками (в анімації)
    public void SpawnStone()
    {
        // Створюємо камінь між руками з обертанням між руками
        Vector3 spawnPoint = CalculateSpawnPoint();
        Quaternion spawnRotation = CalculateSpawnRotation();
        _spawnedStone = Instantiate(stonePrefab, spawnPoint, spawnRotation);
        _isHoldingStone = true;
    }

    private void Update()
    {
        // Якщо камінь створений і його потрібно тримати
        if (_spawnedStone != null && _isHoldingStone)
        {
            // Оновлюємо позицію і обертання каменя між двома руками, поки голем його тре
            _spawnedStone.transform.position = CalculateSpawnPoint();
            _spawnedStone.transform.rotation = CalculateSpawnRotation();
        }
    }

    // Метод, який викликається під час замаху (в анімації)
    public void ThrowStone()
    {
        if (_spawnedStone != null)
        {
            // Знімаємо прапорець, щоб більше не прив'язувати камінь до рук
            _isHoldingStone = false;

            // Орієнтуємо камінь в напрямку, на який дивиться голем
            _spawnedStone.transform.rotation = Quaternion.LookRotation(transform.forward);

            // Отримуємо Rigidbody каменя і додаємо силу для кидка
            Rigidbody rb = _spawnedStone.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
            }
        }
    }
}

