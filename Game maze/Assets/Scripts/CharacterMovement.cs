using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    // variable to store character animator component
    private Animator _animator;

    // variables to store optimized setter/getter parameter IDs
    private int _isWalkingHash;
    private int _isRunningHash;
    private int _isJumpingHash;

    // variable to store the instance of the PlayerInput
    private PlayerInput _input;

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

    private void Awake()
    {
        _input = new PlayerInput();

        // set the player input values using listeners
        _input.CharacterControls.Movement.performed += ctx => {
            _currentMovement = ctx.ReadValue<Vector2>();
            _movementPressed = _currentMovement.x != 0 || _currentMovement.y != 0;
        };
        _input.CharacterControls.Run.performed += ctx => _runPressed = ctx.ReadValueAsButton();
        _input.CharacterControls.Jump.performed += ctx => _jumpPressed = ctx.ReadValueAsButton();
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
    private void Update()
    {
        handleMovement();
        handleJump();
        handleRototion();
    }

    private void handleMovement()
    {
        // get paramater values from animator
        bool isWalking = _animator.GetBool(_isWalkingHash);
        bool isRunning = _animator.GetBool(_isRunningHash);

        // start walking if movement pressed is true and not already walking
        if(_movementPressed && !isWalking)
        {
            _animator.SetBool(_isWalkingHash, true);
        }

        // spot walking if movement pressed is false and currently walking
        if (!_movementPressed && isWalking)
        {
            _animator.SetBool(_isWalkingHash, false);
        }

        // start running if movement pressed and run pressed is true and not already running
        if ((_movementPressed && _runPressed) && !isRunning)
        {
            _animator.SetBool(_isRunningHash, true);
        }

        // stop running if movement pressed or run pressed is false and currently running
        if ((!_movementPressed || !_runPressed) && isRunning)
        {
            _animator.SetBool(_isRunningHash, false);
        }
    }

    private void handleRototion()
    {
        // Current position of our character
        Vector3 currentPosition = transform.position;

        // the change in position our character point to
        Vector3 newPosition = new Vector3(_currentMovement.x, 0, _currentMovement.y);

        // combine the position to give a position to look at
        Vector3 positionToLook = currentPosition + newPosition;

        // rotate the character to face the positionToLook
        transform.LookAt(positionToLook);
    }

    private void handleJump()
    {
        // Check if the player is grounded
        _isGrounded = (Physics.CheckSphere(groundLeftCheck.position, 0.1f, groundLayer) && 
                      Physics.CheckSphere(groundRightCheck.position, 0.1f, groundLayer));

        // get the jump parameter from animator
        bool isJumping = _animator.GetBool(_isJumpingHash);

        // Start jump if jump is pressed, player is grounded, and not already jumping
        if (_jumpPressed && _isGrounded && !isJumping)
        {
            _animator.SetBool(_isJumpingHash, true);
        }
 
        // Stop jump if the player is grounded again
        if (_isGrounded && isJumping)
        {
            //_animator.SetBool(_isJumpingHash, false);
        }
    }

    private void OnEnable()
    {
        // enable the character controls action map
        _input.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        // disable the character controls action map
        _input.CharacterControls.Disable();
    }
}
