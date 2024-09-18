using UnityEngine;

public class GolemAttack : MonoBehaviour
{
    // Positions of hands for spawning the stone
    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _rightHand;

    // Stone prefab and throw settings
    [SerializeField] private GameObject _stonePrefab;
    [SerializeField] private float _throwForce = 15f;
    [SerializeField] private Transform _throwDirection;

    // Stone size settings
    [SerializeField] private Vector3 _minStoneScale = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField] private Vector3 _maxStoneScale = new Vector3(1f, 1f, 1f);
    [SerializeField] private float _growthDuration = 1f; // Time for stone to reach max size

    [SerializeField] private AudioSource _golemSound;
    [SerializeField] private AudioClip _throwSound;
    [SerializeField] private AudioClip _idleSound;

    private GameObject _spawnedStone;
    private Animator _animator;

    // Flag to track if the golem is holding the stone
    private bool _isHoldingStone = false;

    // Flag to fix stone on the right hand
    private bool _holdOnRightHand = false;
    private float _growthStartTime;

    // Initialize animator component
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Calculate the midpoint between the hands for stone spawning
    private Vector3 CalculateSpawnPoint()
    {
        return (_leftHand.position + _rightHand.position) / 2;
    }

    // Calculate the rotation of the stone based on hand positions
    private Quaternion CalculateSpawnRotation()
    {
        Vector3 direction = (_rightHand.position - _leftHand.position).normalized;
        return Quaternion.LookRotation(direction);
    }

    // Called during hand rubbing animation to spawn the stone
    public void SpawnStone()
    {
        // Instantiate the stone at the midpoint between hands with appropriate rotation
        Vector3 spawnPoint = CalculateSpawnPoint();
        Quaternion spawnRotation = CalculateSpawnRotation();
        _spawnedStone = Instantiate(_stonePrefab, spawnPoint, spawnRotation);
        _isHoldingStone = true;
        _holdOnRightHand = false; // Stone is still held between both hands

        // Record the start time for growth
        _growthStartTime = Time.time;

        _golemSound.PlayOneShot(_idleSound);
    }

    // Fix the stone to the right hand
    public void HoldStoneOnRightHand()
    {
        _holdOnRightHand = true;
        _golemSound.PlayOneShot(_throwSound);
    }

    private void Update()
    {
        // Update the stone's position and scale if it is being held
        if (_spawnedStone != null && _isHoldingStone)
        {
            if (_holdOnRightHand)
            {
                // Fix the stone's position to the right hand
                _spawnedStone.transform.position = _rightHand.position;
            }
            else
            {
                // Update stone's position and rotation between the hands
                _spawnedStone.transform.position = CalculateSpawnPoint();
                _spawnedStone.transform.rotation = CalculateSpawnRotation();
            }

            // Update stone's size based on time elapsed
            float growthProgress = (Time.time - _growthStartTime) / _growthDuration;
            if (growthProgress < 1f)
            {
                // Interpolate stone size based on growth progress
                _spawnedStone.transform.localScale = Vector3.Lerp(_minStoneScale, _maxStoneScale, growthProgress);
            }
            else
            {
                // Set stone size to maximum value
                _spawnedStone.transform.localScale = _maxStoneScale;
            }
        }
    }

    // Called during the throwing animation to launch the stone
    public void ThrowStone()
    {
        if (_spawnedStone != null)
        {
            // Unfix the stone from the hands
            _isHoldingStone = false;

            // Get the Rigidbody component and apply force for the throw
            Rigidbody rb = _spawnedStone.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero; // Reset velocity
                rb.AddForce(_throwDirection.right * _throwForce, ForceMode.Impulse); // Apply throwing force
            }
        }
    }
}
