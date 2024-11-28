using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(UserInput))]
[RequireComponent(typeof(DetectorEarth))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private UserInput _userInput;
    private bool _isOnGround;

    public DetectorEarth DetectorEarth { get; private set; }

    private void Awake()
    {
        DetectorEarth = GetComponent<DetectorEarth>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _userInput = GetComponent<UserInput>();
        _isOnGround = true;
    }

    private void OnEnable()
    {
        DetectorEarth.GroundStatusChanged += UpdateIsGround;
    }

    private void Update()
    {
        Move();

        if (Input.GetKeyDown(_userInput.Jump) && _isOnGround)
        {
            Jump();
        }
    }

    private void OnDisable()
    {
        DetectorEarth.GroundStatusChanged -= UpdateIsGround;
    }

    public void UpdateIsGround()
    {
        _isOnGround = DetectorEarth.IsOnGround;
    }

    private void Move()
    {
        float move = Input.GetAxis(_userInput.AxisHorizontal);

        if (move != 0) 
        {
            _rigidbody.velocity = new Vector2(move * _speed, _rigidbody.velocity.y);
            float angle = move > 0 ? 0 : 180;
            Quaternion targetRotation = Quaternion.Euler(new Vector2(0, angle));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _speed);
        }
        
        Setup(Mathf.Abs(move));
    }

    private void Jump() 
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }

    private void Setup(float speed)
    {
        _animator.SetFloat(PlayerAnimatorData.Params.Speed, speed);
        _animator.SetBool(PlayerAnimatorData.Params.IsGrounded, _isOnGround);
    }
}