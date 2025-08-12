using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Mover : MonoBehaviour
{
    [SerializeField] private float _currentSpeed = 5;
    [SerializeField] private float _maxSpeed = 5;
    [SerializeField] private float _jumpForce = 40;

    private Rigidbody2D _rigidbody2d;
    private SpriteRenderer _sprite;
    private AnimationHandler _animationHandler;
    private InputHandler _inputHandler;

    private Vector2 _currentMoveInput;
    private bool _isGrounded;
    private bool _isJumpAllowed;

    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _animationHandler = GetComponent<AnimationHandler>();
        _inputHandler = GetComponent<InputHandler>();
    }

    private void Update()
    {
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        CheckGroundStatus();
        Move();
        Jump();
    }

    private void OnEnable()
    {
        _inputHandler.OnMoveInput += HandleMoveInput;
        _inputHandler.OnJumpInput += HandleJumpInput;
    }

    private void OnDisable()
    {
        _inputHandler.OnMoveInput -= HandleMoveInput;
        _inputHandler.OnJumpInput -= HandleJumpInput;
    }

    private void HandleMoveInput(Vector2 moveInput)
    {
        if (_currentMoveInput == moveInput)
            return;

        _currentMoveInput = moveInput;
        Reflect();
    }

    private void HandleJumpInput()
    {
        _isJumpAllowed = _isGrounded;
    }

    private void Move()
    {
        _rigidbody2d.velocity = new Vector2(_currentMoveInput.x * _currentSpeed, _rigidbody2d.velocity.y);
    }

    private void Reflect()
    {
        if (_currentMoveInput.x > 0)
            _sprite.flipX = false;

        if (_currentMoveInput.x < 0)
            _sprite.flipX = true;
    }

    private void Jump()
    {
        if (_isJumpAllowed == false)
            return;

        _rigidbody2d.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        _isJumpAllowed = false;
    }

    private void UpdateAnimation()
    {
        bool isJumping = !_isGrounded;
        bool isRunning = _currentMoveInput.x != 0;

        _animationHandler.UpdateJumpAnimation(isJumping);
        _animationHandler.UpdateRunAnimation(isRunning);
    }

    private void SetGroundedState(bool state)
    {
        _isGrounded = state;
    }

    private void SetSpeed(bool hasContact)
    {
        if (_isGrounded)
        {
            _currentSpeed = _maxSpeed;
            return;
        }

        float limitSpeed = 1f;

        if (hasContact)
            limitSpeed++;

        _currentSpeed = Mathf.MoveTowards(_currentSpeed, limitSpeed, _maxSpeed * Time.deltaTime);
    }

    private void CheckGroundStatus()
    {
        float distance = 0.2f;
        float radius = 1f;
        Vector2 checkPoint = new Vector2(transform.position.x, transform.position.y) + Vector2.down;

        RaycastHit2D hitDown = Physics2D.Raycast(checkPoint, Vector2.down, distance);
        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, radius);

        SetGroundedState(hitDown.collider != null);
        SetSpeed(hitCollider != null);
    }
}