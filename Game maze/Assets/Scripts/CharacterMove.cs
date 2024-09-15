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
    private int _isJumpingHash;

    // variable to store the instance of the PlayerInput
    private PlayerInput _playerInput;
    private InputAction _moveAction;

    // variables to store player input values
    private Vector2 _currentMovement;
    private bool _movementPressed;
    private bool _runPressed;
    private bool _jumpPressed;

    // Ground check
    [SerializeField] private Transform groundLeftCheck;
    [SerializeField] private Transform groundRightCheck;
    [SerializeField] private LayerMask groundLayer;
    private bool _isGrounded;

    // Movement speed variables
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;


    private void Awake()
    {
        _playerInput = new PlayerInput();

        // set the player input values using listeners
        
        _playerInput.CharacterControls.Run.performed += ctx => _runPressed = ctx.ReadValueAsButton();
        _playerInput.CharacterControls.Jump.performed += ctx => _jumpPressed = ctx.ReadValueAsButton();

        _moveAction = _playerInput.FindAction("Movement");
    }

    private void Start()
    {
        // set the animator reference
        _animator = GetComponent<Animator>();

        // set the ID reference
        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
        _isJumpingHash = Animator.StringToHash("isJumping");
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
        float speed = _runPressed ? runSpeed : walkSpeed;

        _currentMovement = _moveAction.ReadValue<Vector2>();
        _movementPressed = _currentMovement.x != 0 || _currentMovement.y != 0;

        // Apply movement in the forward direction (character moves in the direction it faces)
        Vector3 movement = new Vector3(_currentMovement.x, 0, _currentMovement.y) * speed * Time.deltaTime;

        // Move the character based on the movement direction and speed
        transform.Translate(movement, Space.Self);

        // start walking if movement pressed is true and not already walking
        if (_movementPressed && !isWalking)
        {
            _animator.SetBool(_isWalkingHash, true);
        }

        // stop walking if movement pressed is false and currently walking
        if (!_movementPressed && isWalking)
        {
            _animator.SetBool(_isWalkingHash, false);
        }

        // start running if movement pressed and run pressed is true and not already running
        if (_movementPressed && _runPressed && !isRunning)
        {
            _animator.SetBool(_isRunningHash, true);
        }

        // stop running if movement pressed or run pressed is false and currently running
        if ((!_movementPressed || !_runPressed) && isRunning)
        {
            _animator.SetBool(_isRunningHash, false);
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
}

