using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMove : MonoBehaviour
{
    // variable to store character animator component
    private Animator _animator;

    // variables to store optimized setter/getter parameter IDs
    private int _isWalkingHash;
    private int _isRunningHash;

    // variable to store the instance of the PlayerInput
    private PlayerInput _playerInput;
    private InputAction _moveAction;

    // variables to store player input values
    private Vector2 _currentMovement;
    private bool _movementPressed;
    private bool _runPressed;

    // Movement speed variables
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _runSpeed = 10f;

    // 
    [SerializeField] private KeysManager _keysManager;
    [SerializeField] private HeartManager _heartManager;

    // Layer names
    private const string _keyLayer = "Key";
    private const string _enemyLayer = "Enemy";

    // 
    private AudioManager _audioManager;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        // set the player input values using listeners
        _playerInput.CharacterControls.Run.performed += ctx => _runPressed = ctx.ReadValueAsButton();

        _moveAction = _playerInput.FindAction("Movement");
    }

    private void Start()
    {
        // set the animator reference
        _animator = GetComponent<Animator>();

        // set the ID reference
        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        handleMovement();
        handleRotation();
    }

    private void handleMovement()
    {
        // get parameter values from animator
        bool isWalking = _animator.GetBool(_isWalkingHash);
        bool isRunning = _animator.GetBool(_isRunningHash);

        // Determine movement speed
        float speed = _runPressed ? _runSpeed : _walkSpeed;

        _currentMovement = _moveAction.ReadValue<Vector2>();
        _movementPressed = _currentMovement.x != 0 || _currentMovement.y != 0;

        // Apply movement in the forward direction (character moves in the direction it faces)
        Vector3 movement = new Vector3(_currentMovement.x, 0, _currentMovement.y).normalized * speed * Time.deltaTime;

        // Move the character based on the movement direction and speed
        transform.Translate(movement, Space.Self);

        // Walking logic
        if (_movementPressed && !isWalking)
        {
            _animator.SetBool(_isWalkingHash, true);
            if (_audioManager.soundMoveSource.clip != _audioManager.walking || !_audioManager.soundMoveSource.isPlaying)
            {
                _audioManager.PlayMoveSource(_audioManager.walking);
            }
        }

        // Stop walking when not moving
        if (!_movementPressed && isWalking)
        {
            _animator.SetBool(_isWalkingHash, false);
            _audioManager.soundMoveSource.Pause();
        }

        // Running logic
        if (_movementPressed && _runPressed && !isRunning)
        {
            _animator.SetBool(_isRunningHash, true);
            if (_audioManager.soundMoveSource.clip != _audioManager.running)
            {
                _audioManager.PlayMoveSource(_audioManager.running);
            }
        }

        // Switch back to walking sound when stop running
        if ((!_movementPressed || !_runPressed) && isRunning)
        {
            _animator.SetBool(_isRunningHash, false);
            if (_movementPressed) // If still moving, switch to walking sound
            {
                if (_audioManager.soundMoveSource.clip != _audioManager.walking)
                {
                    _audioManager.PlayMoveSource(_audioManager.walking);
                }
            }
            else // If completely stopped, pause the sound
            {
                _audioManager.soundMoveSource.Pause();
            }
        }
    }

    private void handleRotation()
    {
        /*// Rotate the character based on movement direction (optional if you want the character to face the direction of movement)
        if (_movementPressed)
        {
            Vector3 forwardDirection = new Vector3(_currentMovement.x, 0, _currentMovement.y).normalized;

            if (forwardDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(forwardDirection), 0.1f);
            }
        }*/
    }

    private void OnEnable()
    {
        // enable the character controls action map
        _playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        // disable the character controls action map
        _playerInput.CharacterControls.Disable();
    }

    private void OnCollisionEnter(Collision collision)
    {
        int layer = collision.gameObject.layer;

        if (LayerMask.LayerToName(layer) == _enemyLayer)
        {
            _heartManager.TakeDamage();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        int layer = other.gameObject.layer;

        if (LayerMask.LayerToName(layer) == _keyLayer)
        {
            _audioManager.PlaySFX(_audioManager.getKey);
            _keysManager.AddKey(1);
            other.gameObject.SetActive(false);
        }
    }
}

