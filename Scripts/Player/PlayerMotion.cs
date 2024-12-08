using System;
using UnityEngine;

[RequireComponent(typeof(DetectorGround))]
[RequireComponent(typeof(InputScheme))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMotion : MonoBehaviour
{
    private const float AngleRight = 0f;
    private const float AngleLeft = 180f;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private bool _isOnGround;
    private InputScheme _inputScheme;
    private Rigidbody2D _rigidbody;
    private DetectorGround _detectorGround;

    public event Action OnMoved;

    public float CurrentMove { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputScheme = GetComponent<InputScheme>();
        _detectorGround = GetComponent<DetectorGround>();
        _isOnGround = true;
    }

    private void OnEnable()
    {
        _detectorGround.GroundStatusChanged += UpdateIsGround;
    }

    private void Update()
    {
        Move();
        OnMoved?.Invoke();

        if (Input.GetKeyDown(_inputScheme.Jump) && _isOnGround)
        {
            Jump();
        }
    }

    private void OnDisable()
    {
        _detectorGround.GroundStatusChanged -= UpdateIsGround;
    }

    public void UpdateIsGround()
    {
        _isOnGround = _detectorGround.IsOnGround;
    }

    private void Move()
    {
        CurrentMove = Input.GetAxis(_inputScheme.AxisHorizontal);

        if (CurrentMove != 0)
        {
            _rigidbody.velocity = new Vector2(CurrentMove * _speed, _rigidbody.velocity.y);
            float angle = CurrentMove > 0 ? AngleRight : AngleLeft;
            Quaternion targetRotation = Quaternion.Euler(new Vector2(0, angle));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _jumpForce);
        }
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }
}