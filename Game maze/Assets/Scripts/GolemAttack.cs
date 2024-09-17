using UnityEngine;

public class GolemAttack : MonoBehaviour
{
    // Позиції рук, між якими буде спавнитися камінь
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;

    // Префаб каменя
    [SerializeField] private GameObject stonePrefab;
    // Сила, з якою кидається камінь
    [SerializeField] private float throwForce = 15f;

    // Змінні для розміру каменя
    [SerializeField] private Vector3 minStoneScale = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField] private Vector3 maxStoneScale = new Vector3(1f, 1f, 1f);
    [SerializeField] private float growthDuration = 1f; // Час, за який камінь досягає максимального розміру

    private GameObject _spawnedStone;
    private Animator _animator;
    private bool _isHoldingStone = false; // Прапорець, щоб відстежувати чи голем тримає камінь
    private bool _holdOnRightHand = false; // Прапорець, щоб фіксувати камінь на правій руці
    private float _growthStartTime;

    [SerializeField] private AudioSource _golemSound;

    [SerializeField] private AudioClip _throwSound;
    [SerializeField] private AudioClip _idleSound;

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
        _holdOnRightHand = false; // Камінь все ще тримається між двома руками

        // Запам'ятовуємо час початку росту
        _growthStartTime = Time.time;

        _golemSound.PlayOneShot(_idleSound);
    }

    // Метод, що фіксує камінь на правій руці
    public void HoldStoneOnRightHand()
    {
        _holdOnRightHand = true; // Фіксуємо камінь на правій руці
        _golemSound.PlayOneShot(_throwSound);
    }

    private void Update()
    {
        // Якщо камінь створений і його потрібно тримати
        if (_spawnedStone != null && _isHoldingStone)
        {
            if (_holdOnRightHand)
            {
                // Якщо прапорець встановлений, фіксуємо камінь на правій руці
                _spawnedStone.transform.position = rightHand.position;
            }
            else
            {
                // Оновлюємо позицію і обертання каменя між двома руками, поки голем його тре
                _spawnedStone.transform.position = CalculateSpawnPoint();
                _spawnedStone.transform.rotation = CalculateSpawnRotation();
            }

            // Розрахунок нового розміру каменя
            float growthProgress = (Time.time - _growthStartTime) / growthDuration;
            if (growthProgress < 1f)
            {
                // Розрахунок розміру на основі часу
                _spawnedStone.transform.localScale = Vector3.Lerp(minStoneScale, maxStoneScale, growthProgress);
            }
            else
            {
                // Розмір досяг максимального значення
                _spawnedStone.transform.localScale = maxStoneScale;
            }
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
            // _spawnedStone.transform.rotation = Quaternion.LookRotation(transform.forward);

            // Отримуємо Rigidbody каменя і додаємо силу для кидка
            Rigidbody rb = _spawnedStone.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(transform.right * throwForce, ForceMode.Impulse);
            }
        }
    }
}
