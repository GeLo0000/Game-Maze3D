using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMove : MonoBehaviour
{
    // Reference to the character's animator component
    private Animator _animator;

    // IDs for animator parameters to optimize performance
    private int _isWalkingHash;
    private int _isRunningHash;

    // Reference to the PlayerInput component and movement action
    private PlayerInput _playerInput;
    private InputAction _moveAction;

    // Player input values for movement and running
    private Vector2 _currentMovement;
    private bool _movementPressed;
    private bool _runPressed;

    // Movement speed settings
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _runSpeed = 10f;

    // Reference to the KeysManager and HeartManager
    [SerializeField] private KeysManager _keysManager;
    [SerializeField] private HeartManager _heartManager;

    // Layer names for collision detection
    private const string _keyLayer = "Key";
    private const string _enemyLayer = "Enemy";

    // Reference to the AudioManager
    private AudioManager _audioManager;

    private void Awake()
    {
        // Initialize PlayerInput and AudioManager
        _playerInput = new PlayerInput();
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        // Set up input action listeners
        _playerInput.CharacterControls.Run.performed += ctx => _runPressed = ctx.ReadValueAsButton();
        _moveAction = _playerInput.FindAction("Movement");
    }

    private void Start()
    {
        // Initialize animator and animator parameter IDs
        _animator = GetComponent<Animator>();
        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
    }

    // Update character movement in FixedUpdate for consistent physics calculations
    private void FixedUpdate()
    {
        handleMovement();
    }

    // Handle character movement and animation
    private void handleMovement()
    {
        // Retrieve current animator states
        bool isWalking = _animator.GetBool(_isWalkingHash);
        bool isRunning = _animator.GetBool(_isRunningHash);

        // Determine movement speed based on input
        float speed = _runPressed ? _runSpeed : _walkSpeed;

        // Get current movement input and check if any movement is pressed
        _currentMovement = _moveAction.ReadValue<Vector2>();
        _movementPressed = _currentMovement.x != 0 || _currentMovement.y != 0;

        // Apply movement in the forward direction relative to the character
        Vector3 movement = new Vector3(_currentMovement.x, 0, _currentMovement.y).normalized * speed * Time.deltaTime;
        transform.Translate(movement, Space.Self);

        // Update walking animation and sound
        if (_movementPressed && !isWalking)
        {
            _animator.SetBool(_isWalkingHash, true);
            if (_audioManager.soundMoveSource.clip != _audioManager.walking || !_audioManager.soundMoveSource.isPlaying)
            {
                _audioManager.PlayMoveSource(_audioManager.walking);
            }
        }

        // Stop walking animation and sound if not moving
        if (!_movementPressed && isWalking)
        {
            _animator.SetBool(_isWalkingHash, false);
            _audioManager.soundMoveSource.Pause();
        }

        // Update running animation and sound
        if (_movementPressed && _runPressed && !isRunning)
        {
            _animator.SetBool(_isRunningHash, true);
            if (_audioManager.soundMoveSource.clip != _audioManager.running)
            {
                _audioManager.PlayMoveSource(_audioManager.running);
            }
        }

        // Switch to walking sound or pause if not running
        if ((!_movementPressed || !_runPressed) && isRunning)
        {
            _animator.SetBool(_isRunningHash, false);
            if (_movementPressed)
            {
                if (_audioManager.soundMoveSource.clip != _audioManager.walking)
                {
                    _audioManager.PlayMoveSource(_audioManager.walking);
                }
            }
            else
            {
                _audioManager.soundMoveSource.Pause();
            }
        }
    }

    // Handle character rotation
    /*private void handleRotation()
    {
        
        if (_movementPressed)
        {
            Vector3 forwardDirection = new Vector3(_currentMovement.x, 0, _currentMovement.y).normalized;

            if (forwardDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(forwardDirection), 0.1f);
            }
        }
        
    }
*/

    private void OnEnable()
    {
        // Enable character controls when object is enabled
        _playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        // Disable character controls when object is disabled
        _playerInput.CharacterControls.Disable();
    }

    // Handle collisions with keys or enemies
    private void OnTriggerEnter(Collider other)
    {
        int layer = other.gameObject.layer;

        if (LayerMask.LayerToName(layer) == _keyLayer)
        {
            _audioManager.PlaySFX(_audioManager.getKey);
            _keysManager.AddKey(1);
            other.gameObject.SetActive(false);
        }

        if (LayerMask.LayerToName(layer) == _enemyLayer)
        {
            _heartManager.TakeDamage();
        }
    }
}
