using System.Collections;
using UnityEngine;

public class SpearTrap : MonoBehaviour
{
    // Prefab of the spear to be instantiated
    [SerializeField] private GameObject _spearPrefab;

    // The point where the spear will be spawned
    [SerializeField] private Transform _spawnPoint;

    // Time interval between each spear shot
    [SerializeField] private float _shootInterval = 2f;

    // Speed at which the spear will be shot
    [SerializeField] private float _spearSpeed = 10f;

    // Lifetime of the spear before it gets destroyed
    [SerializeField] private float _spearLifetime = 5f;

    // Audio source to play the shoot sound
    [SerializeField] private AudioSource _shootSound;

    private void Start()
    {
        // Start the continuous shooting cycle
        StartCoroutine(ShootSpearRoutine());
    }

    private IEnumerator ShootSpearRoutine()
    {
        while (true)
        {
            // Instantiate the spear at the spawn point with correct rotation
            GameObject spear = Instantiate(_spearPrefab, _spawnPoint.position, _spawnPoint.rotation);
            Rigidbody rb = spear.GetComponent<Rigidbody>();

            // Set the velocity of the spear if it has a Rigidbody component
            if (rb != null)
            {
                rb.velocity = -_spawnPoint.up * _spearSpeed;
            }

            // Play the shooting sound if the AudioSource is assigned
            if (_shootSound != null)
            {
                _shootSound.Play();
            }

            // Destroy the spear after a set lifetime to avoid clutter
            Destroy(spear, _spearLifetime);

            // Wait for the specified interval before the next shot
            yield return new WaitForSeconds(_shootInterval);
        }
    }
}
